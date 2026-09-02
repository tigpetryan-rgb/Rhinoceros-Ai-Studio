# G1 Core Build Evidence

Canonical gate: MID-423 / STEP 1 / G1.

## Verdict

**PASS**

## Evidence

- Date: 2026-09-02
- Repository: `tigpetryan-rgb/Rhinoceros-Ai-Studio`
- PR: #2 — `G1 evidence: compile current Core with .NET 8`
- Evidence source commit: `74fb68c9efa55356228b4feef3e1449469352231`
- GitHub Actions workflow: `G1 Core Build`
- Successful workflow run ID: `33627706511`
- Job ID: `100239280284`
- Runner: Ubuntu 24.04
- SDK pinned by `global.json`: **.NET SDK 8.0.424**
- Target framework: `net8.0`
- Repository policy: `TreatWarningsAsErrors=true`

Commands:

```text
dotnet restore tests/Jewelry.Engine.ContractTests/Jewelry.Engine.ContractTests.csproj
dotnet build tests/Jewelry.Engine.ContractTests/Jewelry.Engine.ContractTests.csproj -c Release --no-restore
dotnet run --project tests/Jewelry.Engine.ContractTests/Jewelry.Engine.ContractTests.csproj -c Release --no-build
```

Build result:

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

Smoke result:

```text
PASS: 15 nodes; released=0.1.1; target=0.2.0
```

## Failure history / fix provenance

The first inspectable run (`33627390904`) failed with one deterministic compiler error:

`CS0121` in `ExecutionTarget.All` because `new(null)` was ambiguous between the nullable-set constructor and record copy constructor.

The only source correction was an explicit nullable constructor cast:

`new((IReadOnlySet<OperationNodeId>?)null)`

No frozen architecture was reopened and no later gate was promoted.

## Gate conclusion

G1 acceptance criteria are satisfied for the recovered Core compile surface: reproducible .NET 8 SDK build, warnings-as-errors, zero warnings, zero errors, and successful Core smoke execution.

This PASS does **not** imply G2, Rhino SDK, Grasshopper, geometry, manufacturing, export, or v0.2 RC readiness.
