using Jewelry.Engine.Contracts;
using Jewelry.Engine.Integration;

var runtime = new JewelryEngineRuntime();
var graph = runtime.CreateClassicSolitaire(new GraphId("classic-solitaire-contract-test"));
var result = await runtime.Executor.ExecuteAsync(graph, ExecutionTarget.All);

if (!result.Succeeded) throw new InvalidOperationException("Canonical core graph execution contract smoke test failed.");
if (graph.SchemaVersion != CanonicalEnvironment.GraphSchemaVersion) throw new InvalidOperationException("Graph schema mismatch.");
if (CanonicalEnvironment.ReleasedEngineVersion != "0.1.1") throw new InvalidOperationException("Released engine version changed without authorization.");

Console.WriteLine($"PASS: {graph.Nodes.Count} nodes; released={CanonicalEnvironment.ReleasedEngineVersion}; target={CanonicalEnvironment.TargetEngineVersion}");
