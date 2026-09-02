using Jewelry.Engine.Contracts;

namespace Jewelry.Engine.Execution;

public sealed class OperationRegistry
{
    private readonly Dictionary<(OperationTypeId TypeId, string Schema), IOperationKernel> _kernels = new();

    public void Register(IOperationKernel kernel)
    {
        var key = (kernel.OperationTypeId, kernel.OperationSchemaVersion);
        if (!_kernels.TryAdd(key, kernel))
            throw new InvalidOperationException($"Duplicate operation registration: {key.OperationTypeId}/{key.OperationSchemaVersion}");
    }

    public bool TryResolve(OperationTypeId typeId, string schemaVersion, out IOperationKernel? kernel) =>
        _kernels.TryGetValue((typeId, schemaVersion), out kernel);
}

public sealed class GraphValidator
{
    private readonly OperationRegistry _registry;
    public GraphValidator(OperationRegistry registry) => _registry = registry;

    public IReadOnlyList<Diagnostic> Validate(OperationGraph graph)
    {
        var diagnostics = new List<Diagnostic>();
        if (!string.Equals(graph.SchemaVersion, CanonicalEnvironment.GraphSchemaVersion, StringComparison.Ordinal))
            diagnostics.Add(new("GRAPH_SCHEMA_UNSUPPORTED", DiagnosticSeverity.Error, $"Expected graph schema {CanonicalEnvironment.GraphSchemaVersion}."));

        var nodeIds = new HashSet<OperationNodeId>();
        foreach (var node in graph.Nodes)
        {
            if (!nodeIds.Add(node.Id)) diagnostics.Add(new("DUPLICATE_NODE_ID", DiagnosticSeverity.Error, node.Id.Value, node.Id));
            if (!_registry.TryResolve(node.OperationTypeId, node.OperationSchemaVersion, out var kernel) || kernel is null)
                diagnostics.Add(new("OPERATION_NOT_REGISTERED", DiagnosticSeverity.Error, $"{node.OperationTypeId}/{node.OperationSchemaVersion}", node.Id));
            else if (kernel.ParameterContractId != node.ParameterContractId)
                diagnostics.Add(new("PARAMETER_CONTRACT_MISMATCH", DiagnosticSeverity.Error, node.ParameterContractId.Value, node.Id));
        }

        var allIds = graph.Nodes.Select(n => n.Id).ToHashSet();
        foreach (var node in graph.Nodes)
            foreach (var dependency in node.Dependencies)
                if (!allIds.Contains(dependency)) diagnostics.Add(new("MISSING_DEPENDENCY", DiagnosticSeverity.Error, dependency.Value, node.Id));

        if (HasCycle(graph)) diagnostics.Add(new("GRAPH_CYCLE", DiagnosticSeverity.Error, "OperationGraph contains a dependency cycle."));
        return diagnostics;
    }

    private static bool HasCycle(OperationGraph graph)
    {
        var byId = graph.Nodes.ToDictionary(n => n.Id);
        var state = new Dictionary<OperationNodeId, int>();
        bool Visit(OperationNodeId id)
        {
            if (state.TryGetValue(id, out var current)) return current == 1;
            state[id] = 1;
            if (byId.TryGetValue(id, out var node))
                foreach (var dependency in node.Dependencies)
                    if (Visit(dependency)) return true;
            state[id] = 2;
            return false;
        }
        return graph.Nodes.Any(node => Visit(node.Id));
    }
}

public sealed class GeometryStore : IGeometryWorkspace
{
    private readonly ExecutionId _executionId;
    private readonly Dictionary<long, (IGeometryPayload Payload, bool Published)> _items = new();
    private long _sequence;
    public GeometryStore(ExecutionId executionId) => _executionId = executionId;

    public GeometryReference Stage(IGeometryPayload payload)
    {
        ArgumentNullException.ThrowIfNull(payload);
        var sequence = Interlocked.Increment(ref _sequence);
        _items.Add(sequence, (payload.CloneDetached(), false));
        return new(_executionId, sequence);
    }

    public IGeometryPayload ReadDetached(GeometryReference reference)
    {
        EnsureScope(reference);
        if (!_items.TryGetValue(reference.Sequence, out var item)) throw new KeyNotFoundException(reference.ToString());
        return item.Payload.CloneDetached();
    }

    public void Publish(GeometryReference reference)
    {
        EnsureScope(reference);
        var item = _items[reference.Sequence];
        _items[reference.Sequence] = (item.Payload, true);
    }

    public void Discard(GeometryReference reference)
    {
        EnsureScope(reference);
        _items.Remove(reference.Sequence);
    }

    public bool IsPublished(GeometryReference reference)
    {
        EnsureScope(reference);
        return _items.TryGetValue(reference.Sequence, out var item) && item.Published;
    }

    private void EnsureScope(GeometryReference reference)
    {
        if (reference.ExecutionId != _executionId) throw new InvalidOperationException("Stale or foreign GeometryReference.");
    }
}

public static class Fingerprints
{
    public static string NodeCompatibility(OperationNode node, ValidationPolicy validationPolicy, string environmentFingerprint)
    {
        var text = $"{node.OperationTypeId.Value}|{node.OperationSchemaVersion}|{node.ParameterContractId.Value}|{node.Parameters.GetRawText()}|{validationPolicy.Fingerprint}|{environmentFingerprint}";
        return Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(text)));
    }
}

public enum InvalidationDomain { Semantic, Parameter, Output, ManufacturingRules, Material, Pricing, Topology, Unknown }
public sealed record InvalidationRequest(InvalidationDomain Domain, IReadOnlySet<OperationNodeId> Roots);
public sealed record InvalidatedSubgraphTarget(IReadOnlySet<OperationNodeId> Nodes);

public sealed class InvalidationEngine
{
    public InvalidatedSubgraphTarget Calculate(OperationGraph graph, InvalidationRequest request)
    {
        if (request.Domain == InvalidationDomain.Pricing) return new(new HashSet<OperationNodeId>());
        var invalid = new HashSet<OperationNodeId>(request.Roots);
        var changed = true;
        while (changed)
        {
            changed = false;
            foreach (var node in graph.Nodes)
                if (!invalid.Contains(node.Id) && node.Dependencies.Any(invalid.Contains))
                    changed |= invalid.Add(node.Id);
        }
        return new(invalid);
    }
}

public sealed class Executor
{
    private readonly OperationRegistry _registry;
    private readonly GraphValidator _validator;
    public Executor(OperationRegistry registry) { _registry = registry; _validator = new(registry); }

    public ValueTask<GraphExecutionResult> ExecuteAsync(OperationGraph graph, ExecutionTarget target, CancellationToken cancellationToken = default)
    {
        var validation = _validator.Validate(graph);
        if (validation.Any(d => d.Severity == DiagnosticSeverity.Error))
            return ValueTask.FromResult(new GraphExecutionResult(ExecutionId.New(), false, false, Array.Empty<NodeExecutionRecord>(), new Dictionary<(OperationNodeId, OutputPortId), PublishedGeometry>(), validation));

        return ExecuteValidatedAsync(graph, target, cancellationToken);
    }

    private async ValueTask<GraphExecutionResult> ExecuteValidatedAsync(OperationGraph graph, ExecutionTarget target, CancellationToken cancellationToken)
    {
        var executionId = ExecutionId.New();
        var store = new GeometryStore(executionId);
        var published = new Dictionary<(OperationNodeId, OutputPortId), PublishedGeometry>();
        var records = new List<NodeExecutionRecord>();
        var diagnostics = new List<Diagnostic>();
        var required = RequiredClosure(graph, target);
        var ordered = TopologicalOrder(graph).Where(node => required.Contains(node.Id));
        var status = new Dictionary<OperationNodeId, NodeExecutionStatus>();
        var cancelled = false;

        foreach (var node in ordered)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancelled = true;
                status[node.Id] = NodeExecutionStatus.Cancelled;
                records.Add(new(node.Id, NodeExecutionStatus.Cancelled, Array.Empty<Diagnostic>()));
                continue;
            }
            if (node.Dependencies.Any(d => status.TryGetValue(d, out var s) && s is NodeExecutionStatus.Failed or NodeExecutionStatus.Blocked or NodeExecutionStatus.Cancelled))
            {
                status[node.Id] = NodeExecutionStatus.Blocked;
                records.Add(new(node.Id, NodeExecutionStatus.Blocked, Array.Empty<Diagnostic>()));
                continue;
            }

            if (!_registry.TryResolve(node.OperationTypeId, node.OperationSchemaVersion, out var kernel) || kernel is null)
                throw new InvalidOperationException("GraphValidator allowed an unregistered operation.");

            var context = new OperationExecutionContext(executionId, graph, node, published, store);
            var result = await kernel.ExecuteAsync(context, node.Parameters, cancellationToken).ConfigureAwait(false);
            diagnostics.AddRange(result.Diagnostics);
            if (!result.Succeeded)
            {
                status[node.Id] = NodeExecutionStatus.Failed;
                records.Add(new(node.Id, NodeExecutionStatus.Failed, result.Diagnostics));
                continue;
            }

            foreach (var output in result.GeometryOutputs)
            {
                var reference = store.Stage(output.Value);
                store.Publish(reference);
                published[(node.Id, output.Key)] = new(node.Id, output.Key, reference, output.Value.ContentFingerprint);
            }
            status[node.Id] = NodeExecutionStatus.Succeeded;
            records.Add(new(node.Id, NodeExecutionStatus.Succeeded, result.Diagnostics));
        }

        var succeeded = !cancelled && records.All(r => r.Status is NodeExecutionStatus.Succeeded);
        return new(executionId, succeeded, cancelled, records, published, diagnostics);
    }

    private static HashSet<OperationNodeId> RequiredClosure(OperationGraph graph, ExecutionTarget target)
    {
        if (target.TargetNodes is null) return graph.Nodes.Select(n => n.Id).ToHashSet();
        var byId = graph.Nodes.ToDictionary(n => n.Id);
        var required = new HashSet<OperationNodeId>();
        void Add(OperationNodeId id)
        {
            if (!required.Add(id) || !byId.TryGetValue(id, out var node)) return;
            foreach (var dependency in node.Dependencies) Add(dependency);
        }
        foreach (var id in target.TargetNodes) Add(id);
        return required;
    }

    private static IReadOnlyList<OperationNode> TopologicalOrder(OperationGraph graph)
    {
        var byId = graph.Nodes.ToDictionary(n => n.Id);
        var indegree = graph.Nodes.ToDictionary(n => n.Id, n => n.Dependencies.Count);
        var dependents = graph.Nodes.ToDictionary(n => n.Id, _ => new List<OperationNodeId>());
        foreach (var node in graph.Nodes)
            foreach (var dependency in node.Dependencies)
                if (dependents.TryGetValue(dependency, out var list)) list.Add(node.Id);

        var ready = new SortedSet<string>(indegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key.Value), StringComparer.Ordinal);
        var result = new List<OperationNode>();
        while (ready.Count > 0)
        {
            var key = ready.Min!;
            ready.Remove(key);
            var id = indegree.Keys.Single(x => x.Value == key);
            result.Add(byId[id]);
            foreach (var dependent in dependents[id])
            {
                indegree[dependent]--;
                if (indegree[dependent] == 0) ready.Add(dependent.Value);
            }
        }
        return result;
    }
}
