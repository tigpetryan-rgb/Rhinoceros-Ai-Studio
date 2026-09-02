# Source Recovery Note — MID-423 / G1 blocker

The canonical MID-423 Drive handoff contains the MID-381→422 source manifest and architecture/implementation report, but the raw `.cs/.csproj` bytes listed by that manifest were not included in the archived ZIP.

This repository therefore reconstructs the **Core compile surface only** from frozen MID-381→422 decisions as a direct blocker-removal action for G1. It does not reopen architecture, does not claim Rhino bindings, and does not promote G2 or later gates.

The released engine remains v0.1.1. The v0.2 line remains migration-stage until mandatory evidence gates pass.
