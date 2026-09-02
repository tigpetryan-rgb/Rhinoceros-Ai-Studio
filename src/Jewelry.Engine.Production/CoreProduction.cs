using Jewelry.Engine.Contracts;

namespace Jewelry.Engine.Production;

public sealed class ProductionCandidateFactory
{
    public ProductionCandidate Create(GraphRevisionId graphRevisionId, GraphExecutionResult execution, GeometryQcReport geometryQc, ManufacturingReport manufacturing, bool repairAccepted) =>
        new(graphRevisionId, execution, geometryQc, manufacturing, repairAccepted);
}

public sealed class ProductionReadinessGate
{
    public IReadOnlyList<Diagnostic> Evaluate(ProductionCandidate candidate, string? manufacturingOverrideReason = null)
    {
        var diagnostics = new List<Diagnostic>();
        if (!candidate.Execution.Succeeded) diagnostics.Add(new("PRODUCTION_EXECUTION_FAILED", DiagnosticSeverity.Error, "Graph execution did not succeed."));
        if (!candidate.GeometryQc.Passed) diagnostics.Add(new("PRODUCTION_QC_FAILED", DiagnosticSeverity.Error, "Final Geometry QC did not pass."));
        if (!candidate.Manufacturing.Passed && string.IsNullOrWhiteSpace(manufacturingOverrideReason)) diagnostics.Add(new("PRODUCTION_MANUFACTURING_FAILED", DiagnosticSeverity.Error, "Manufacturing validation did not pass and no reasoned override exists."));
        if (!candidate.RepairAccepted) diagnostics.Add(new("PRODUCTION_REPAIR_NOT_ACCEPTED", DiagnosticSeverity.Error, "Repair state is not accepted."));
        return diagnostics;
    }
}

public sealed class ProductionCommitCoordinator
{
    private readonly ProductionReadinessGate _gate = new();
    public ProductionRevision Commit(ProductionCandidate candidate, GeometrySnapshot snapshot, string? manufacturingOverrideReason = null)
    {
        var diagnostics = _gate.Evaluate(candidate, manufacturingOverrideReason);
        if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
            throw new InvalidOperationException(string.Join("; ", diagnostics.Select(d => d.Code)));
        return new(ProductionRevisionId.New(), CanonicalEnvironment.ReleasedEngineVersion, candidate.GraphRevisionId, snapshot, DateTimeOffset.UtcNow);
    }
}

public interface IProductionExporter
{
    string Format { get; }
    ValueTask ExportAsync(ProductionRevision revision, Stream destination, CancellationToken cancellationToken);
}

public sealed record PhysicalProperties(double VolumeMm3, double DensityGPerMm3, double WeightGrams);
public sealed record CommercialCost(decimal MaterialCost, decimal ProductionCost, decimal TotalCost);
