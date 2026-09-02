using System.Text.Json;

namespace Jewelry.Engine.Contracts;

public readonly record struct GraphId(string Value) { public static GraphId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }
public readonly record struct GraphRevisionId(string Value) { public static GraphRevisionId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }
public readonly record struct OperationNodeId(string Value) { public static OperationNodeId FromRole(GraphId graphId, string role) => new($"{graphId.Value}:{role}"); public override string ToString() => Value; }
public readonly record struct OperationTypeId(string Value) { public override string ToString() => Value; }
public readonly record struct InputPortId(string Value) { public override string ToString() => Value; }
public readonly record struct OutputPortId(string Value) { public override string ToString() => Value; }
public readonly record struct ParameterContractId(string Value) { public override string ToString() => Value; }
public readonly record struct ExecutionId(string Value) { public static ExecutionId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }
public readonly record struct GeometryReference(ExecutionId ExecutionId, long Sequence) { public override string ToString() => $"{ExecutionId.Value}:{Sequence}"; }
public readonly record struct RepairAttemptId(string Value) { public static RepairAttemptId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }
public readonly record struct ProductionRevisionId(string Value) { public static ProductionRevisionId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }
public readonly record struct GeometrySnapshotId(string Value) { public static GeometrySnapshotId New() => new(Guid.NewGuid().ToString("N")); public override string ToString() => Value; }

public static class CanonicalEnvironment
{
    public const string ReleasedEngineVersion = "0.1.1";
    public const string TargetEngineVersion = "0.2.0";
    public const string GraphSchemaVersion = "0.2";
    public const string CanonicalUnits = "mm";
}

public enum DiagnosticSeverity { Info, Warning, Error }
public sealed record Diagnostic(string Code, DiagnosticSeverity Severity, string Message, OperationNodeId? NodeId = null);

public interface IGeometryPayload
{
    string ContentFingerprint { get; }
    IGeometryPayload CloneDetached();
}

public sealed record PublishedGeometry(OperationNodeId NodeId, OutputPortId PortId, GeometryReference Reference, string ContentFingerprint);

public abstract record InputBinding(InputPortId PortId);
public sealed record NodeOutputInputBinding(InputPortId PortId, OperationNodeId SourceNodeId, OutputPortId SourcePortId) : InputBinding(PortId);
public sealed record ComponentIdInputBinding(InputPortId PortId, string ComponentId) : InputBinding(PortId);

public sealed record OutputPortDefinition(OutputPortId Id, string ContractId);

public sealed record OperationNode(
    OperationNodeId Id,
    OperationTypeId OperationTypeId,
    string OperationSchemaVersion,
    ParameterContractId ParameterContractId,
    JsonElement Parameters,
    string SemanticRole,
    IReadOnlyList<OperationNodeId> Dependencies,
    IReadOnlyList<InputBinding> InputBindings,
    IReadOnlyList<OutputPortDefinition> Outputs,
    string? OutputValidationCheckpoint = null);

public sealed record OperationGraph(GraphId Id, GraphRevisionId RevisionId, string SchemaVersion, IReadOnlyList<OperationNode> Nodes);

public sealed record ExecutionPolicy(bool StopOnGlobalCancellation = true);
public sealed record ValidationPolicy(string Fingerprint);

public enum NodeExecutionStatus { Pending, Succeeded, Failed, Blocked, Cancelled }
public sealed record NodeExecutionRecord(OperationNodeId NodeId, NodeExecutionStatus Status, IReadOnlyList<Diagnostic> Diagnostics);

public sealed record ExecutionTarget(IReadOnlySet<OperationNodeId>? TargetNodes)
{
    public static ExecutionTarget All { get; } = new((IReadOnlySet<OperationNodeId>?)null);
    public static ExecutionTarget Nodes(params OperationNodeId[] nodeIds) => new(new HashSet<OperationNodeId>(nodeIds));
}

public sealed record OperationExecutionContext(
    ExecutionId ExecutionId,
    OperationGraph Graph,
    OperationNode Node,
    IReadOnlyDictionary<(OperationNodeId NodeId, OutputPortId PortId), PublishedGeometry> PublishedOutputs,
    IGeometryWorkspace GeometryWorkspace);

public sealed record OperationExecutionResult(
    bool Succeeded,
    IReadOnlyDictionary<OutputPortId, IGeometryPayload> GeometryOutputs,
    IReadOnlyList<Diagnostic> Diagnostics)
{
    public static OperationExecutionResult Success(IReadOnlyDictionary<OutputPortId, IGeometryPayload>? outputs = null) =>
        new(true, outputs ?? new Dictionary<OutputPortId, IGeometryPayload>(), Array.Empty<Diagnostic>());

    public static OperationExecutionResult Failure(params Diagnostic[] diagnostics) =>
        new(false, new Dictionary<OutputPortId, IGeometryPayload>(), diagnostics);
}

public interface IOperationKernel
{
    OperationTypeId OperationTypeId { get; }
    string OperationSchemaVersion { get; }
    ParameterContractId ParameterContractId { get; }
    ValueTask<OperationExecutionResult> ExecuteAsync(OperationExecutionContext context, JsonElement parameters, CancellationToken cancellationToken);
}

public interface IGeometryWorkspace
{
    GeometryReference Stage(IGeometryPayload payload);
    IGeometryPayload ReadDetached(GeometryReference reference);
    void Publish(GeometryReference reference);
    void Discard(GeometryReference reference);
    bool IsPublished(GeometryReference reference);
}

public sealed record GraphExecutionResult(
    ExecutionId ExecutionId,
    bool Succeeded,
    bool Cancelled,
    IReadOnlyList<NodeExecutionRecord> Nodes,
    IReadOnlyDictionary<(OperationNodeId NodeId, OutputPortId PortId), PublishedGeometry> PublishedOutputs,
    IReadOnlyList<Diagnostic> Diagnostics);

public sealed record GeometryQcReport(bool Passed, IReadOnlyList<Diagnostic> Diagnostics);
public sealed record ManufacturingReport(bool Passed, IReadOnlyList<Diagnostic> Diagnostics, string RulesFingerprint);
public sealed record ProductionCandidate(GraphRevisionId GraphRevisionId, GraphExecutionResult Execution, GeometryQcReport GeometryQc, ManufacturingReport Manufacturing, bool RepairAccepted);
public sealed record GeometrySnapshot(GeometrySnapshotId Id, IReadOnlyDictionary<string, IGeometryPayload> Components);
public sealed record ProductionRevision(ProductionRevisionId Id, string ReleasedEngineVersion, GraphRevisionId GraphRevisionId, GeometrySnapshot Snapshot, DateTimeOffset CreatedAt);

public static class SerializationGuard
{
    public static void ThrowIfRuntimeGeometryReference(Type type)
    {
        if (type == typeof(GeometryReference))
            throw new InvalidOperationException("GeometryReference is execution-scoped runtime state and must not be serialized as durable semantic identity.");
    }
}
