using Jewelry.Engine.Contracts;
using Jewelry.Engine.Execution;
using Jewelry.Engine.Operations;
using Jewelry.Engine.Planning;

namespace Jewelry.Engine.Integration;

public sealed class JewelryEngineRuntime
{
    public OperationRegistry Registry { get; }
    public Executor Executor { get; }

    public JewelryEngineRuntime()
    {
        Registry = new();
        OperationRegistration.RegisterCanonicalOperations(Registry);
        Executor = new(Registry);
    }

    public OperationGraph CreateClassicSolitaire(GraphId? graphId = null) => GraphFactory.CreateClassicSolitaire(graphId ?? GraphId.New());
}
