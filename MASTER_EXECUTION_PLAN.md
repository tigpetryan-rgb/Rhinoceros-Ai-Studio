# JEWELRY ENGINE — CANONICAL MASTER EXECUTION PLAN

**Plan ID:** JE-MASTER-PLAN  
**Plan Revision:** 1.0  
**Effective date:** 2026-09-02  
**Canonical released engine:** Jewelry Engine v0.1.1  
**Target milestone:** Jewelry Engine v0.2.0  
**Canonical decisions closed:** MID-001 → MID-422  
**Current exact MID:** MID-423  
**Current state:** v0.2 MIGRATION-STAGE — NOT RELEASE CANDIDATE  
**Current active objective:** BUILD + RHINO SDK BINDING + TEST-GATE CLOSURE / RC HARDENING

---

## 0. NORMATIVE AUTHORITY

This document is the mandatory execution authority for every Jewelry Engine / Rhinoceros AI Studio work session.

It is not a suggestion, backlog brainstorm, or historical summary. It defines:

- what is already complete,
- what is partially implemented,
- what remains,
- the exact order of execution,
- what is explicitly rejected,
- what is deferred,
- the evidence required before advancing.

### Authority precedence

1. This plan, at the highest `Plan Revision`, is the execution authority.
2. Canonical Master Decisions MID-001→MID-422 remain frozen unless objective compile/test/Rhino evidence proves a contradiction.
3. Historical chat handoffs and module documents are evidence, not competing execution authorities.
4. Isolated module plans never override this file.
5. If Drive and GitHub copies differ, STOP. Reconcile them before implementation. No silent split-brain plan is allowed.

### User override

The project owner may explicitly change this plan. A requested direction change must first be recorded as a new Plan Revision (and, when architecture-affecting, a new MID decision) before implementation starts. Do not silently deviate.

---

## 1. STATUS LANGUAGE — STRICT

Use only these project-state marks:

- ✅ **VERIFIED COMPLETE** — implementation/contract is finished for its declared scope and required evidence has passed.
- ⚠️ **IMPLEMENTED BUT NOT FINAL** — code/contracts exist, but required compile, Rhino, integration, persistence, regression, or production evidence is incomplete.
- ❌ **NOT COMPLETE** — required implementation or verification has not been delivered.
- 🗑️ **NOT MAIN DIRECTION / REJECTED** — explicitly superseded, forbidden, or no longer part of the canonical path.

### Promotion rule

A task may become ✅ only when its acceptance evidence is recorded. “Code exists”, “architecture approved”, “skeleton written”, or “looks correct” is not sufficient.

If later evidence disproves a ✅ claim, downgrade it immediately and record why.

---

## 2. AGENT / CHAT STARTUP PROTOCOL — MANDATORY

Before touching code, docs, GitHub issues/PRs, Drive artifacts, or architecture, every new chat/agent must:

1. Read this entire plan.
2. State the current `Plan Revision`, released engine version, target version, current exact MID, and current active gate internally before acting.
3. Read only the canonical source/evidence required for the active gate.
4. Work on the **single current active gate** and its direct blockers only.
5. Do not start later phases because they are interesting, easy, or parallelizable.
6. Do not reopen MID-001→MID-422 architecture without concrete failing build/test/Rhino evidence.
7. Do not invent a new competing result/diagnostic/geometry/identity contract.
8. At the end of a meaningful work unit, update this plan’s evidence/status before moving forward.

If a request arrives for later-scope work, record it as deferred unless the user explicitly orders a Plan Revision.

---

## 3. ONE-ACTIVE-GATE RULE

Only one primary execution gate may be active at a time.

A later gate may be prepared only when preparation is strictly necessary to close the current gate. Parallel feature development is forbidden until the current gate’s acceptance criteria pass.

### Failure behavior

- Compile failure → fix compile/integration contradictions only.
- Contract test failure → fix the deterministic contract defect only.
- Rhino binding failure → fix Rhino adapter/module binding only.
- Geometry fixture failure → fix the responsible geometry algorithm/policy only.
- Do not use a failure as permission to redesign unrelated architecture.

---

## 4. CURRENT VERIFIED BASELINE

### ✅ Released v0.1.1 baseline

- ✅ RingSizeConverter
- ✅ RingRailGenerator
- ✅ ProfileGenerator
- ✅ RingShankGenerator
- ✅ ClosedSolidValidator
- ✅ NakedEdgeValidator

These released algorithms are preserved. v0.2 adapts/migrates them; it does not rewrite them without evidence.

### ✅ Canonical architecture / integration decisions

- ✅ A–L architecture reviews completed.
- ✅ Cross-module contracts MID-001→MID-330 completed.
- ✅ OperationGraph + Executor v0.2 final schema freeze MID-331→MID-380 completed.
- ✅ v0.2 source/contract migration MID-381→MID-422 completed.
- ✅ Mandatory static source audit gate G0 passed.

### Release truth

- ✅ Released version remains **Jewelry Engine v0.1.1**.
- ❌ v0.2 is not RC.
- ❌ v0.2 release bump is not authorized.

---

## 5. CURRENT MODULE STATUS AFTER MID-422

| Module / subsystem | Status | Canonical meaning |
|---|---|---|
| A — Gem Engine | ⚠️ | Migrated to v0.2 operation contracts; concrete approved Gem module still needs Rhino binding/compile/tests. |
| B — Gem Seat + Prongs | ⚠️ | Typed Seat/ProngLayout/Prong graph migration exists; concrete Rhino assembly verification remains. |
| C — Boolean + Geometry QC | ⚠️ | Typed outcomes/QC/publication contracts exist; real Rhino pathological fixtures remain. |
| D — DesignIntent | ⚠️ | Integration bridge is present; upstream validator/default/normalization/capability completion remains post-RC work unless required by an active gate. |
| E — Manufacturing Validator | ⚠️ | Read-only validation integration/readiness surface exists; concrete production analyzers/rules and Rhino evidence remain. |
| F — Weight + Cost | ⚠️ | Post-ProductionRevision boundary exists; concrete calculators/material catalog/tests remain. |
| G — Export + Production Package | ⚠️ | ProductionRevision-only authority exists; concrete 3DM/STEP/STL writers/re-import/package tests remain. |
| H — AI Jewelry Analyzer | ❌ | Architecture approved; executable analyzer implementation is not on current RC-critical path. |
| I — Construction Recognition | ❌ | Architecture approved; executable recognizer is not on current RC-critical path. |
| J — OperationGraph Planner | ⚠️ | v0.2 factories, stable node IDs, legacy migrator, graph diff exist; full runtime verification remains. |
| Executor / GeometryStore / Cache / Invalidation / Journal | ⚠️ | Compile-oriented implementation exists; build + contract + Rhino integration evidence remains. |
| K — Grasshopper | ⚠️ | Canonical adapter exists; real GH_Component/GUID/preview/persistence host tests remain. |
| L — Autonomous Repair | ⚠️ | Rule-based revision/invalidation/rollback integration exists; real module/Rhino repair fixtures remain. |
| ProductionReadinessGate | ⚠️ | Contract/source exists; real end-to-end gate evidence remains. |
| GeometrySnapshot / ProductionRevision | ⚠️ | Canonical authority model exists; real commit/persistence/export fixtures remain. |
| Persistent cache/journal/production stores | ❌ | Production backends not complete. |
| Full Rhino deterministic regression | ❌ | Not run. |
| v0.2 RC verdict | ❌ | Blocked by mandatory test gates. |

---

## 6. MANDATORY TEST-GATE STATE

| Gate | Scope | Current status | Promotion evidence |
|---|---|---|---|
| G0 | Static source/project-reference/delimiter audit | ✅ PASS | Existing MID-381→422 evidence |
| G1 | .NET 8 Core compile, warnings-as-errors | ❌ NOT RUN | Successful reproducible build log |
| G2 | Contract harness: graph/executor/store/cache/invalidation/repair/production | ⚠️ IMPLEMENTED, NOT RUN | Full harness pass log |
| G3 | Jewelry.Engine.Rhino compile against Rhino 8 RhinoCommon | ❌ NOT RUN | Successful Rhino SDK build log |
| G4 | Real Rhino Classic Solitaire E2E | ❌ NOT RUN | Deterministic successful fixture + QC evidence |
| G5 | Boolean NoResult/Ambiguous/EngineFailure fixtures | ❌ NOT RUN | Expected outcome matrix passes |
| G6 | Incremental execution + real geometry fingerprints | ❌ NOT RUN | Cache/invalidation/preservation fixtures pass |
| G7 | Parameter/topology repair + rollback with real modules | ❌ NOT RUN | Repair and last-good rollback fixtures pass |
| G8 | Production snapshot + 3DM/STEP/STL integration | ⚠️ CONTRACT PARTIAL / NOT RUN | Export + re-import/hash/manifest tests pass |
| G9 | Real Grasshopper GH_Component solve/preview/persistence | ⚠️ ADAPTER ONLY | Host integration tests pass, no durable runtime GeometryReference |
| G10 | Rhino 8 determinism/tolerance regression | ❌ NOT RUN | Repeated-session tolerance-aware equivalence passes |

No RC decision is allowed until required gates are closed.

---

## 7. STRICT EXECUTION ORDER — CURRENT MID-423

The following sequence is mandatory. Do not reorder without a Plan Revision.

### STEP 1 — G1 Core build closure — **ACTIVE NOW**

Status: ❌

Actions:

1. Compile all Core .NET 8 projects with warnings-as-errors.
2. Capture exact compiler diagnostics.
3. Fix only real compile/integration contradictions.
4. Do not reopen frozen architecture without evidence.
5. Re-run until clean.

Acceptance:

- clean reproducible build,
- zero warnings under warnings-as-errors,
- evidence recorded here / linked commit-log.

**Nothing later becomes the primary task until STEP 1 is ✅.**

### STEP 2 — Bind released Ring v0.1.1 implementation

Status: ⚠️

Bind the released Ring implementation to `IRingGeometryModuleV011` / canonical Rhino adapter seams. Preserve released algorithms and explicit migration boundaries.

Acceptance: adapter compile + baseline ring fixture equivalence.

### STEP 3 — Bind A/B/C/E concrete Rhino modules

Status: ⚠️

Bind concrete approved implementations for:

- Gem,
- Gem Seat,
- Prong layout/generation,
- Boolean,
- Geometry QC,
- Manufacturing validation.

No ComponentId→Brep magic lookup. No duplicate geometry algorithms in Executor or Grasshopper.

Acceptance: all bindings compile and use frozen typed contracts.

### STEP 4 — Rhino geometry clone + content fingerprint

Status: ❌

Implement/verify production deterministic detached cloning and geometry content fingerprinting for supported geometry types.

Acceptance: fingerprint stability across sessions and clone independence tests.

### STEP 5 — G2 contract harness closure

Status: ⚠️

Run all implemented graph/executor/store/cache/invalidation/repair/production cases. Fix deterministic failures only.

Acceptance: full contract harness pass.

### STEP 6 — G3 Rhino SDK compile

Status: ❌

Compile `Jewelry.Engine.Rhino` against the installed Rhino 8 RhinoCommon SDK.

Acceptance: clean Rhino adapter build with warnings-as-errors where applicable.

### STEP 7 — G4 Classic Solitaire real Rhino E2E

Status: ❌

Canonical first production model only:

Ring → Round Brilliant Gem → placement → Seat → 4/6 Prongs → Boolean assembly → Geometry QC → Manufacturing validation candidate.

Acceptance: deterministic successful real Rhino fixture with correct staged/published geometry and structured reports.

### STEP 8 — G5 Boolean failure corpus

Status: ❌

Test and preserve semantic distinction among:

- NoResult,
- AmbiguousResult,
- EngineFailure,
- Cancelled,
- deterministic explicit selection policy when allowed.

Include tangent/near-coincident/sliver/micro-feature pathological cases.

Acceptance: expected outcome matrix passes with no random retry.

### STEP 9 — G6 incremental invalidation/cache/preservation

Status: ❌

Verify real geometry fingerprint-driven reuse and invalidation:

- shank-only changes preserve independent gem branch,
- gem dimension changes invalidate seat/prongs/Boolean downstream,
- manufacturing-rule change starts at manufacturing validation,
- material only invalidates declared consumers,
- pricing never regenerates CAD geometry.

Acceptance: deterministic fixture matrix passes.

### STEP 10 — G7 Autonomous Repair real fixtures

Status: ❌

Initial deterministic repair policies:

- `PRONG_TOO_THIN` → explicit diameter increase rule,
- `SEAT_TOO_DEEP` → explicit depth reduction rule,
- `BOOLEAN_NO_RESULT` → explicit deterministic intersection/fallback policy.

Pipeline:

Diagnostic → RepairPlanner → RepairPlan → RepairAttemptId → derived revision → invalidation → targeted rerun → QC → Manufacturing → accept/reject → rollback.

Topology repair must rerun Planner. Failed repair must never overwrite last-good production state.

Acceptance: parameter repair, topology repair, rejection, and rollback fixtures pass.

### STEP 11 — ProductionReadinessGate + ProductionRevision

Status: ⚠️

Required authority:

accepted candidate → final QC → manufacturing validation / reasoned approved override → accepted repair state → geometry commit → GeometrySnapshot → ProductionReadinessGate → ProductionRevision.

Acceptance: success/failure/override/audit fixtures pass; ProductionRevision remains append-only and detached.

### STEP 12 — G8 3DM / STEP / STL + Production Package

Status: ⚠️

Bind concrete exporters consuming only `ProductionRevision + GeometrySnapshot`.

Required outputs:

- 3DM CAD master,
- STEP manufacturing interchange,
- STL mesh manufacturing,
- DesignIntent snapshot,
- QC report,
- manufacturing report,
- material/stone/weight metadata,
- engine/revision IDs,
- hashes/manifest.

Acceptance: export, re-import, integrity, manifest, and failure-handling tests pass.

### STEP 13 — Physical properties + commercial costing

Status: ⚠️

ProductionRevision → Volume → configurable alloy density → Weight → Material/Production Cost.

Invariant: geometry never knows price; pricing does not invalidate CAD geometry.

Acceptance: calculator/catalog/unit tests + production snapshot integration pass.

### STEP 14 — G9 Grasshopper real host integration

Status: ⚠️

Wire actual `GH_Component` wrappers/GUIDs/persistence to canonical adapters:

JE Ring Size, Ring Rail, Profile, Ring Shank, Gem, Seat, Prongs, Boolean, Validate, Weight, OperationGraph.

Invariant: Grasshopper is UI/adapter/wrapper only. It never becomes a second Planner/Executor/geometry authority.

Acceptance: solve/preview/persistence/recompute tests pass; no durable runtime GeometryReference serialization.

### STEP 15 — G10 determinism/tolerance regression

Status: ❌

Run repeatable Rhino 8 fixtures across sessions/runs with canonical tolerances.

Acceptance: equivalent deterministic inputs yield semantically equivalent geometry and reports within tolerance.

### STEP 16 — RC readiness verdict

Status: ❌

Produce explicit RC YES/NO verdict with:

- all gate evidence,
- remaining blockers,
- A–L status,
- exact new MID decisions beginning MID-423,
- released version still v0.1.1 unless explicit Master release acceptance.

No automatic version bump.

---

## 8. POST-v0.2-RC AUTONOMOUS AI PATH — DO NOT PULL FORWARD

These remain part of the project but are not current RC-critical execution work.

1. ❌ AI Jewelry Analyzer executable implementation
   - photos/sketch/CAD screenshots/dimensions/prompt
   - observations + confidence + provenance + uncertainties
2. ❌ Construction Recognition executable implementation
   - observations → construction hypotheses → resolved construction graph
3. ⚠️ DesignIntent finalization
   - validator
   - default resolver
   - normalizer
   - capability validator
   - ResolvedDesignIntent
4. ❌ Full autonomous loop
   - Reference → Analyzer → Construction Recognition → DesignIntent → Planner → OperationGraph → Executor → Rhino CAD → QC → Manufacturing → Repair → ProductionRevision → Weight/Cost → Export

Only after v0.2 RC closure may these become the primary line unless the plan is explicitly revised.

---

## 9. DEFERRED — VALID FUTURE WORK, NOT CURRENT PATH

Do not implement during MID-423 RC hardening unless explicitly promoted by a Plan Revision:

- Halo
- Pavé
- Channel setting
- Bezel setting
- Gallery/undergallery expansion
- Oval/Pear/Emerald/Princess/etc. gem shapes
- Pendants/Earrings/other jewelry families
- Trend Intelligence
- Style DNA
- Collection generation
- natural-language editing UX
- full render system
- multi-image reconstruction

Deferred does not mean rejected.

---

## 10. 🗑️ REJECTED / SUPERSEDED DIRECTIONS

The following must not be reintroduced as active architecture:

- 🗑️ AI generating arbitrary Rhino C# / raw NURBS control logic as the main runtime path.
- 🗑️ Grasshopper as a second CAD business-logic implementation or execution authority.
- 🗑️ OperationGraph v0.1 as the continuing primary graph architecture.
- 🗑️ Silent v0.1.1→v0.2 graph reinterpretation; migration must be explicit/versioned.
- 🗑️ ComponentId→live Brep/GeometryReference magic lookup.
- 🗑️ Runtime GeometryReference persisted as durable semantic asset identity.
- 🗑️ Random Boolean/repair parameter retry/search.
- 🗑️ Competing module-local Result/Diagnostic families where canonical shared contracts exist.
- 🗑️ Treating Gem module’s historical `v0.1.2` label as the released engine version.
- 🗑️ MatrixGold/Peacock proprietary code/runtime dependency. They are workflow/architecture benchmarks only.
- 🗑️ Parallel independent module roadmaps that can advance canonical state without Master-plan review.

---

## 11. REPOSITORY CONTRADICTION POLICY

When this plan is installed in GitHub:

1. Active code and active docs must conform to this plan and MID-001→MID-422.
2. A conflicting active instruction/document must be edited to point to this plan or removed if it has no historical value.
3. Historical handoffs/releases/snapshots should normally be preserved as evidence, but must be clearly labeled `HISTORICAL / NON-AUTHORITATIVE` or placed under an archive path.
4. Never delete the released v0.1.1 baseline merely because v0.2 exists.
5. Never rewrite working geometry algorithms solely to make code stylistically match new architecture; adapt at explicit boundaries.
6. If code contradicts a frozen contract, fix the contradiction only after reproducing it through build/test evidence.
7. README, agent instructions, issue templates, and developer docs must not advertise a later phase as current work.

---

## 12. REQUIRED GITHUB GOVERNANCE FILES

Repository root must contain:

- `/AGENTS.md` — mandatory startup instructions pointing to this plan.
- `/MASTER_EXECUTION_PLAN.md` — byte-equivalent canonical plan mirror.
- `/.github/copilot-instructions.md` — GitHub Copilot/agent enforcement pointer.
- README top banner — “Read MASTER_EXECUTION_PLAN.md before work; current active MID-423 / G1”.

If any one of these claims a different active phase/version, the mismatch is a blocking governance defect.

---

## 13. EVIDENCE / STATUS UPDATE PROTOCOL

Whenever work changes a status:

1. Record date/time and Plan Revision.
2. Record exact gate/task.
3. Record evidence:
   - commit SHA,
   - build/test command,
   - Rhino version/SDK,
   - fixture name,
   - result summary,
   - artifact/hash where relevant.
4. Change status only after evidence exists.
5. Record new MID decision(s) when canonical behavior changes.
6. Set the next exact active gate.
7. Mirror the updated plan to Drive and GitHub before starting the next primary gate.

### Evidence log — initial state

- 2026-09-02 — Plan Revision 1.0 created from latest Drive canonical Master + MID-423 handoff.
- Canonical release: v0.1.1.
- Canonical decisions through MID-422.
- G0 = PASS.
- G1 = NOT RUN.
- G2 = implemented/not run.
- G3–G7/G10 = not run.
- G8/G9 = partial adapter/contract only.
- Current primary active gate = STEP 1 / G1 Core .NET 8 warnings-as-errors build closure.

---

## 14. END-OF-SESSION CHECKLIST

Before a chat/agent declares its work complete:

- Did I work only on the active gate or a direct blocker?
- Did I avoid reopening frozen architecture without evidence?
- Did I preserve v0.1.1 released behavior where required?
- Did I run the required acceptance evidence?
- Did I avoid marking ⚠️ work as ✅ without proof?
- Did I record exact failures/fixes/tests?
- Did I update current MID/gate only if evidence justifies it?
- Did I mirror the plan state to Drive and GitHub?
- Did I leave one unambiguous next action for the next chat?

If any answer is “no”, the handoff is incomplete.
