using System.Text.Json;
using Jewelry.Engine.Contracts;
using Jewelry.Engine.Operations;

namespace Jewelry.Engine.Planning;

public static class GraphFactory
{
    private static JsonElement EmptyParameters() => JsonSerializer.SerializeToElement(new { });

    public static OperationGraph CreateClassicSolitaire(GraphId graphId)
    {
        var nodes = new List<OperationNode>();
        OperationNode Add(string role, OperationTypeId type, params OperationNodeId[] dependencies)
        {
            var id = OperationNodeId.FromRole(graphId, role);
            var node = new OperationNode(id, type, KnownContracts.Schema, KnownContracts.EmptyParameters, EmptyParameters(), role, dependencies, Array.Empty<InputBinding>(), Array.Empty<OutputPortDefinition>());
            nodes.Add(node);
            return node;
        }

        var size = Add("ring-size", KnownContracts.RingSize);
        var rail = Add("ring-rail", KnownContracts.RingRail, size.Id);
        var profile = Add("profile", KnownContracts.Profile);
        var shank = Add("ring-shank", KnownContracts.RingShank, rail.Id, profile.Id);
        var closed = Add("closed-solid", KnownContracts.ClosedSolid, shank.Id);
        var naked = Add("naked-edge", KnownContracts.NakedEdge, closed.Id);
        var gem = Add("gem", KnownContracts.Gem);
        var placement = Add("gem-placement", KnownContracts.GemPlacement, gem.Id);
        var seat = Add("gem-seat", KnownContracts.GemSeat, placement.Id, naked.Id);
        var layout = Add("prong-layout", KnownContracts.ProngLayout, placement.Id);
        var prongs = Add("prongs", KnownContracts.Prongs, layout.Id, placement.Id);
        var difference = Add("boolean-difference", KnownContracts.BooleanDifference, naked.Id, seat.Id);
        var union = Add("boolean-union", KnownContracts.BooleanUnion, difference.Id, prongs.Id);
        var finalQc = Add("final-qc", KnownContracts.FinalQc, union.Id);
        Add("manufacturing", KnownContracts.Manufacturing, finalQc.Id);
        return new(graphId, GraphRevisionId.New(), CanonicalEnvironment.GraphSchemaVersion, nodes);
    }
}

public static class LegacyV011Migration
{
    public const string ReleasedSchemaTag = "0.1.1";
    public static OperationGraph MigrateKnownRingBaseline(string releasedSchemaTag, GraphId graphId)
    {
        if (!string.Equals(releasedSchemaTag, ReleasedSchemaTag, StringComparison.Ordinal))
            throw new InvalidOperationException("Only the released v0.1.1 baseline may use this explicit migrator.");
        return GraphFactory.CreateClassicSolitaire(graphId);
    }
}
