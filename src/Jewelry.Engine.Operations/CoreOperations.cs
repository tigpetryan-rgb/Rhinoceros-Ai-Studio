using System.Text.Json;
using Jewelry.Engine.Contracts;
using Jewelry.Engine.Execution;

namespace Jewelry.Engine.Operations;

public static class KnownContracts
{
    public const string Schema = "0.2";
    public static readonly ParameterContractId EmptyParameters = new("params.empty.v1");
    public static readonly OutputPortId Geometry = new("geometry");

    public static readonly OperationTypeId RingSize = new("ring.size");
    public static readonly OperationTypeId RingRail = new("ring.rail");
    public static readonly OperationTypeId Profile = new("ring.profile");
    public static readonly OperationTypeId RingShank = new("ring.shank");
    public static readonly OperationTypeId ClosedSolid = new("qc.closed-solid");
    public static readonly OperationTypeId NakedEdge = new("qc.naked-edge");
    public static readonly OperationTypeId Gem = new("gem.generate");
    public static readonly OperationTypeId GemPlacement = new("gem.place");
    public static readonly OperationTypeId GemSeat = new("setting.seat");
    public static readonly OperationTypeId ProngLayout = new("setting.prong-layout");
    public static readonly OperationTypeId Prongs = new("setting.prongs");
    public static readonly OperationTypeId BooleanDifference = new("boolean.difference");
    public static readonly OperationTypeId BooleanUnion = new("boolean.union");
    public static readonly OperationTypeId FinalQc = new("qc.final");
    public static readonly OperationTypeId Manufacturing = new("manufacturing.validate");
}

public sealed class NoOpKernel : IOperationKernel
{
    public OperationTypeId OperationTypeId { get; }
    public string OperationSchemaVersion => KnownContracts.Schema;
    public ParameterContractId ParameterContractId => KnownContracts.EmptyParameters;
    public NoOpKernel(OperationTypeId operationTypeId) => OperationTypeId = operationTypeId;
    public ValueTask<OperationExecutionResult> ExecuteAsync(OperationExecutionContext context, JsonElement parameters, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(OperationExecutionResult.Success());
    }
}

public static class OperationRegistration
{
    public static void RegisterCanonicalOperations(OperationRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);
        foreach (var type in new[]
        {
            KnownContracts.RingSize, KnownContracts.RingRail, KnownContracts.Profile, KnownContracts.RingShank,
            KnownContracts.ClosedSolid, KnownContracts.NakedEdge, KnownContracts.Gem, KnownContracts.GemPlacement,
            KnownContracts.GemSeat, KnownContracts.ProngLayout, KnownContracts.Prongs, KnownContracts.BooleanDifference,
            KnownContracts.BooleanUnion, KnownContracts.FinalQc, KnownContracts.Manufacturing
        }) registry.Register(new NoOpKernel(type));
    }
}
