# JEWELRY ENGINE — CANONICAL MASTER EXECUTION PLAN

**Plan ID:** JE-MASTER-PLAN  
**Plan Revision:** 1.1  
**Effective date:** 2026-09-02  
**Canonical released engine:** Jewelry Engine v0.1.1  
**Target milestone:** Jewelry Engine v0.2.0  
**Canonical decisions closed:** MID-001 → MID-422  
**Current exact MID:** MID-423  
**Current state:** v0.2 MIGRATION / RC-HARDENING — NOT RELEASE CANDIDATE  
**Current active gate:** STEP 2 — BIND RELEASED RING v0.1.1 IMPLEMENTATION

---

## 0. NORMATIVE AUTHORITY

This file is the mandatory execution authority for every Jewelry Engine / Rhinoceros AI Studio chat, agent, GitHub change, Drive handoff, implementation, refactor, and test session.

Authority order:
1. Highest revision of this plan.
2. Canonical Master Decisions MID-001→MID-422, unless objective build/test/Rhino evidence proves a contradiction.
3. Released v0.1.1 source/evidence.
4. Historical handoffs and module documents as evidence only, never as competing roadmaps.

If GitHub and Drive copies differ, STOP implementation and reconcile them. Do not operate with split-brain governance.

The project owner may change direction, but an architecture/order change must first be recorded in a new Plan Revision and, when architecture-affecting, a new MID decision.

---

## 1. STRICT STATUS LANGUAGE

- ✅ **VERIFIED COMPLETE** — required implementation and declared acceptance evidence passed.
- ⚠️ **IMPLEMENTED BUT NOT FINAL** — code/contracts exist but required build, Rhino, integration, regression, persistence, or production evidence is incomplete.
- ❌ **NOT COMPLETE** — required work/evidence is missing.
- 🗑️ **REJECTED / NOT MAIN DIRECTION** — superseded or forbidden by canonical architecture.

A task becomes ✅ only after evidence is recorded. “Code exists”, “architecture approved”, or “looks correct” is never enough.

---

## 2. MANDATORY AGENT / CHAT STARTUP PROTOCOL

Before touching code/docs/issues/PRs/tests:
1. Read this entire file.
2. Internally identify Plan Revision, released version, target version, current MID, and the single active gate.
3. Work only on the active gate and direct blockers.
4. Do not begin later phases for convenience or parallelism.
5. Do not reopen MID-001→MID-422 without concrete failing evidence.
6. Do not invent competing Result/Diagnostic/Geometry/Identity contracts.
7. Preserve released v0.1.1 algorithms unless an evidence-backed adapter/migration boundary requires change.
8. At the end of a meaningful unit, record evidence/status here and mirror the plan to GitHub + Drive before advancing.

---

## 3. ONE-ACTIVE-GATE RULE

Exactly one primary gate is active at a time.

Failure handling is narrow:
- compile failure → compile/integration fix only;
- contract failure → deterministic contract fix only;
- Rhino binding failure → Rhino adapter/binding fix only;
- geometry fixture failure → responsible geometry/policy fix only.

A failure is not permission to redesign unrelated architecture.

---

## 4. VERIFIED RELEASE BASELINE

### ✅ Jewelry Engine v0.1.1 released algorithms
- ✅ RingSizeConverter
- ✅ RingRailGenerator
- ✅ ProfileGenerator
- ✅ RingShankGenerator
- ✅ ClosedSolidValidator
- ✅ NakedEdgeValidator

v0.2 must adapt/migrate this baseline; do not casually rewrite it.

### ✅ Canonical integration history
- ✅ A–L architecture reviews completed.
- ✅ Cross-module contracts MID-001→MID-330.
- ✅ OperationGraph + Executor v0.2 final schema freeze MID-331→MID-380.
- ✅ v0.2 source/contract migration decisions MID-381→MID-422.
- ✅ G0 static source/project-reference/delimiter audit.
- ✅ G1 Core .NET 8 warnings-as-errors build closure.

### Release truth
- ✅ Released engine remains **v0.1.1**.
- ❌ v0.2 is not RC.
- ❌ No version bump is authorized.

---

## 5. SOURCE RECOVERY PROVENANCE

The MID-423 Drive archive preserved the MID-381→422 source manifest and canonical implementation/decision reports, but the archived ZIP did not contain the raw `.cs/.csproj` bytes described by that manifest.

To unblock G1, the repository Core compile surface was reconstructed strictly from frozen MID-381→422 decisions. This reconstruction:
- is a G1 blocker-removal action;
- does not claim byte identity with the missing archive source;
- does not substitute for released v0.1.1 Rhino algorithms;
- does not promote G2 or later gates;
- must be reconciled with recovered released/source artifacts at explicit adapter boundaries.

Repository evidence file: `docs/SOURCE_RECOVERY.md`.

---

## 6. CURRENT MODULE STATUS

| Module / subsystem | Status | Canonical meaning |
|---|---|---|
| A — Gem Engine | ⚠️ | v0.2 contract surface exists; real approved Rhino module binding/tests remain. |
| B — Gem Seat + Prongs | ⚠️ | Typed contracts/layout direction exists; real Rhino assembly verification remains. |
| C — Boolean + Geometry QC | ⚠️ | Typed outcome/QC direction exists; real Rhino pathological fixtures remain. |
| D — DesignIntent | ⚠️ | Integration bridge/domain work exists; final validator/default/normalization/capability work remains post-RC unless a blocker. |
| E — Manufacturing Validator | ⚠️ | Validation/readiness boundary exists; concrete production analyzers/rules + Rhino evidence remain. |
| F — Weight + Cost | ⚠️ | ProductionRevision boundary exists; concrete calculators/material catalog/tests remain. |
| G — Export + Production Package | ⚠️ | ProductionRevision-only authority defined; 3DM/STEP/STL writers/re-import/package tests remain. |
| H — AI Jewelry Analyzer | ❌ | Architecture approved; executable implementation is post-v0.2-RC. |
| I — Construction Recognition | ❌ | Architecture approved; executable implementation is post-v0.2-RC. |
| J — OperationGraph Planner | ⚠️ | v0.2 graph factory/migration surface exists; full runtime/contract verification remains. |
| Executor / GeometryStore / Cache / Invalidation / Journal | ⚠️ | Core compile surface builds; complete G2 contract harness evidence remains. |
| K — Grasshopper | ⚠️ | Adapter architecture approved; real GH_Component/GUID/preview/persistence host tests remain. |
| L — Autonomous Repair | ⚠️ | Deterministic rule boundary exists; real module/Rhino repair fixtures remain. |
| ProductionReadinessGate / ProductionRevision | ⚠️ | Core boundary builds; real commit/persistence/export fixtures remain. |
| Persistent production stores | ❌ | Not complete. |
| Full Rhino deterministic regression | ❌ | Not run. |
| v0.2 RC verdict | ❌ | Blocked by remaining mandatory gates. |

---

## 7. MANDATORY TEST-GATE STATE

| Gate | Scope | Status | Required evidence |
|---|---|---|---|
| G0 | Static source/project-reference/delimiter audit | ✅ PASS | MID-381→422 evidence |
| G1 | .NET 8 Core compile, warnings-as-errors | ✅ PASS | GitHub Actions run `33627706511`: SDK 8.0.424, net8.0, 0 warnings, 0 errors; final PR head rerun `33627808748` also PASS |
| G2 | Full contract harness: graph/executor/store/cache/invalidation/repair/production | ⚠️ NOT CLOSED | Complete deterministic harness pass; current 15-node smoke is not G2 closure |
| G3 | Jewelry.Engine.Rhino compile against Rhino 8 RhinoCommon | ❌ NOT RUN | Successful Rhino SDK build log |
| G4 | Real Rhino Classic Solitaire E2E | ❌ NOT RUN | Deterministic successful fixture + QC evidence |
| G5 | Boolean NoResult/Ambiguous/EngineFailure corpus | ❌ NOT RUN | Expected outcome matrix pass |
| G6 | Incremental execution + real geometry fingerprints | ❌ NOT RUN | Cache/invalidation/preservation fixtures pass |
| G7 | Parameter/topology repair + rollback with real modules | ❌ NOT RUN | Repair + last-good rollback fixtures pass |
| G8 | Production snapshot + 3DM/STEP/STL integration | ⚠️ PARTIAL | Export + re-import/hash/manifest tests pass |
| G9 | Real Grasshopper GH_Component solve/preview/persistence | ⚠️ ADAPTER ONLY | Host integration tests pass |
| G10 | Rhino 8 determinism/tolerance regression | ❌ NOT RUN | Repeated-session tolerance-aware equivalence pass |

No RC decision is allowed until required gates close.

---

## 8. STRICT EXECUTION ORDER — MID-423

### STEP 1 — G1 Core build closure — ✅ COMPLETE
Evidence:
- repo `tigpetryan-rgb/Rhinoceros-Ai-Studio`;
- workflow `G1 Core Build`;
- SDK pinned with `global.json` to **8.0.424**;
- target `net8.0`;
- `TreatWarningsAsErrors=true`;
- build: **0 warnings / 0 errors**;
- smoke: `PASS: 15 nodes; released=0.1.1; target=0.2.0`;
- successful evidence run `33627706511`;
- final evidence-head run `33627808748` PASS;
- merged G1 commit: `35b78ceaf47acb630ebcfe23f514fad508a8158b`.

A first run (`33627390904`) found one deterministic CS0121 ambiguity in `ExecutionTarget.All`; it was fixed only by explicitly typing the nullable constructor argument. No architecture change.

### STEP 2 — Bind released Ring v0.1.1 implementation — **ACTIVE NOW**
Status: ⚠️

Required actions:
1. Recover/ground the released v0.1.1 Ring source artifacts from canonical Drive handoffs.
2. Preserve RingSizeConverter, RingRailGenerator, ProfileGenerator, RingShankGenerator, ClosedSolidValidator, NakedEdgeValidator behavior.
3. Bind them to the canonical v0.2 Rhino adapter seam / `IRingGeometryModuleV011`-equivalent interface without duplicating algorithms.
4. Keep mm-domain and Rhino unit conversion boundary intact.
5. Add adapter compile tests and baseline fixture-equivalence evidence.

Acceptance:
- released Ring algorithms are present or referenced through an explicit adapter boundary;
- adapter compiles cleanly;
- baseline v0.1.1 ring fixture equivalence passes;
- no silent algorithm rewrite.

**Nothing later becomes primary until STEP 2 is ✅.**

### STEP 3 — Bind A/B/C/E concrete Rhino modules
Gem, Seat, Prong layout/generation, Boolean, Geometry QC, Manufacturing validation. No ComponentId→Brep magic lookup; no geometry algorithm duplication in Executor/Grasshopper.

### STEP 4 — Rhino geometry detached clone + content fingerprint
Fingerprint stability across sessions and clone independence.

### STEP 5 — G2 full contract harness closure
Graph/executor/store/cache/invalidation/repair/production deterministic cases. The current smoke test is insufficient.

### STEP 6 — G3 Rhino SDK compile
Compile `Jewelry.Engine.Rhino` against Rhino 8 RhinoCommon.

### STEP 7 — G4 Classic Solitaire real Rhino E2E
Ring → Round Brilliant Gem → placement → Seat → 4/6 Prongs → Boolean → Geometry QC → Manufacturing candidate.

### STEP 8 — G5 Boolean failure corpus
Preserve NoResult / AmbiguousResult / EngineFailure / Cancelled distinctions and deterministic selection policies. No random retries.

### STEP 9 — G6 incremental invalidation/cache/preservation
Verify branch preservation, declared consumer invalidation, material separation, and pricing never regenerating CAD.

### STEP 10 — G7 Autonomous Repair real fixtures
Initial deterministic rules: `PRONG_TOO_THIN`, `SEAT_TOO_DEEP`, `BOOLEAN_NO_RESULT`; parameter/topology rerun, acceptance/rejection, rollback.

### STEP 11 — ProductionReadinessGate + ProductionRevision
Accepted candidate → final QC → manufacturing/approved override → accepted repair state → snapshot → readiness gate → append-only ProductionRevision.

### STEP 12 — G8 3DM / STEP / STL + Production Package
Export only from ProductionRevision + GeometrySnapshot; include DesignIntent/QC/manufacturing/material/stone/weight/version/hash manifest; test re-import/integrity/failures.

### STEP 13 — Physical properties + commercial costing
ProductionRevision → volume → configurable density → weight → material/production cost. Geometry never knows price.

### STEP 14 — G9 Grasshopper real host integration
GH_Component wrappers/GUIDs/preview/persistence only. Grasshopper is never a second Planner/Executor/geometry authority.

### STEP 15 — G10 determinism/tolerance regression
Repeat Rhino 8 fixtures across runs/sessions with canonical tolerances.

### STEP 16 — RC readiness verdict
Explicit YES/NO with all gate evidence, blockers, A–L status, new MID decisions beginning MID-423 when warranted. No automatic release bump.

---

## 9. POST-v0.2-RC AUTONOMOUS AI PATH — DO NOT PULL FORWARD

Until v0.2 RC closure, these are not primary work:
- ❌ executable AI Jewelry Analyzer;
- ❌ executable Construction Recognition;
- ⚠️ final DesignIntent validator/default resolver/normalizer/capability validator;
- ❌ full Reference → Analyzer → Construction → DesignIntent → Planner → Rhino → QC → Manufacturing → Repair → ProductionRevision → Cost → Export autonomous loop.

---

## 10. DEFERRED FUTURE WORK

Do not pull into current RC-hardening path without Plan Revision:
Halo, Pavé, Channel, Bezel, expanded gallery, additional gem shapes, pendants/earrings/other families, Trend Intelligence, Style DNA, collection generation, natural-language editing UX, full render system, multi-image reconstruction.

---

## 11. 🗑️ REJECTED / SUPERSEDED DIRECTIONS

Do not reintroduce:
- AI → arbitrary Rhino scripts/raw NURBS as primary runtime path;
- Grasshopper as second CAD business-logic authority;
- OperationGraph v0.1 as current graph architecture;
- silent v0.1.1→v0.2 reinterpretation;
- ComponentId→live Brep/GeometryReference magic lookup;
- runtime GeometryReference as durable semantic identity;
- random Boolean/repair parameter search;
- competing module-local Result/Diagnostic families where shared canonical contracts exist;
- Gem historical `v0.1.2` label as released engine version;
- MatrixGold/Peacock proprietary code/runtime dependency;
- independent module roadmaps advancing canonical state without Master-plan review.

---

## 12. REPOSITORY CONTRADICTION POLICY

1. Active code/docs must conform to this plan and MID-001→MID-422.
2. Conflicting active instructions must be corrected or removed.
3. Historical evidence may remain only when clearly archived/non-authoritative.
4. Never delete released v0.1.1 baseline merely because v0.2 exists.
5. Never rewrite working geometry solely for stylistic conformity; adapt at explicit boundaries.
6. README/AGENTS/Copilot instructions must point to this plan and current gate.
7. Any mismatch in version/current gate is a blocking governance defect.

---

## 13. REQUIRED GITHUB GOVERNANCE FILES

Repository root:
- `/MASTER_EXECUTION_PLAN.md`
- `/AGENTS.md`
- `/.github/copilot-instructions.md`
- `/README.md` top START-HERE banner

CI evidence:
- `/.github/workflows/g1-core.yml`
- `/docs/G1_BUILD_EVIDENCE.md`
- `/docs/SOURCE_RECOVERY.md`

---

## 14. EVIDENCE / STATUS UPDATE PROTOCOL

Whenever status changes:
1. record date, Plan Revision, gate/task;
2. record commit SHA, build/test command, SDK/host, fixture, result, hashes/artifacts where relevant;
3. promote status only after evidence;
4. create MID decision only when canonical behavior changes;
5. set exactly one next active gate;
6. mirror this plan to GitHub + Drive before next primary implementation.

### Evidence log

- 2026-09-02 — Revision 1.0 created; baseline v0.1.1; MID-423; G0 PASS; G1 active.
- 2026-09-02 — G1 first inspectable run `33627390904` FAIL: CS0121 ambiguous `ExecutionTarget.All`; 0 warnings / 1 error.
- 2026-09-02 — Compile-only fix applied: explicitly typed nullable `IReadOnlySet<OperationNodeId>?` argument; architecture unchanged.
- 2026-09-02 — G1 run `33627504547` PASS after compile fix, but SDK pin tightened for unambiguous evidence.
- 2026-09-02 — G1 run `33627706511` PASS using .NET SDK 8.0.424; build 0 warnings / 0 errors; 15-node smoke PASS.
- 2026-09-02 — Final PR head run `33627808748` PASS; G1 merged as `35b78ceaf47acb630ebcfe23f514fad508a8158b`.
- 2026-09-02 — Revision 1.1: G1 promoted to ✅; STEP 2 released Ring v0.1.1 binding becomes the single active gate.

---

## 15. END-OF-SESSION CHECKLIST

Before declaring work complete:
- worked only on active gate/direct blocker;
- no frozen architecture reopened without evidence;
- v0.1.1 released behavior preserved;
- required acceptance evidence actually run;
- no ⚠️ promoted to ✅ without proof;
- exact failures/fixes/tests recorded;
- current gate/MID changed only when justified;
- plan mirrored to Drive + GitHub;
- exactly one next action remains unambiguous.

If any answer is no, the handoff is incomplete.
