using Jewelry.Engine.Contracts;
using Jewelry.Engine.Execution;

namespace Jewelry.Engine.Repair;

public enum RepairKind { Parameter, Topology }
public sealed record RepairPlan(RepairAttemptId AttemptId, RepairKind Kind, IReadOnlyList<string> ChangedSemanticPaths, IReadOnlySet<OperationNodeId> InvalidationRoots);

public sealed class RepairPlanner
{
    private readonly HashSet<string> _registeredCodes = new(StringComparer.Ordinal) { "PRONG_TOO_THIN", "SEAT_TOO_DEEP", "BOOLEAN_NO_RESULT" };
    public RepairPlan? Plan(IReadOnlyList<Diagnostic> diagnostics, IReadOnlySet<OperationNodeId> roots)
    {
        var actionable = diagnostics.FirstOrDefault(d => _registeredCodes.Contains(d.Code));
        if (actionable is null) return null;
        var kind = actionable.Code == "BOOLEAN_NO_RESULT" ? RepairKind.Topology : RepairKind.Parameter;
        return new(RepairAttemptId.New(), kind, new[] { actionable.Code }, roots);
    }
}

public sealed class RepairRevisionCoordinator
{
    private readonly InvalidationEngine _invalidation = new();
    public InvalidatedSubgraphTarget CalculateAffectedNodes(OperationGraph graph, RepairPlan plan) =>
        _invalidation.Calculate(graph, new(InvalidationDomain.Parameter, plan.InvalidationRoots));
}
