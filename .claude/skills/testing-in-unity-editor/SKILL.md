---
name: testing-in-unity-editor
description: Use when verifying a gameplay change actually feels right before merge — launching the Unity Editor and playing the scene by hand, as a complement to batchmode EditMode/PlayMode test runs which only prove code correctness, not tap-feel for a 4-year-old.
---

# Testing in Unity Editor

## Overview
Batchmode tests (`task test`) prove logic is correct. They don't prove tile-tap
*feels* right, tap targets are big enough, or a wrong tap redirects instead of
failing. Only an actual Editor Play session shows that.

## When to Use
- Before merging any change to `Assets/Scripts/Gameplay/*`.
- After changing Sorting Layer / Order in Layer values (depth bugs only show visually).
- When batchmode tests pass but you're not confident the interaction is right.

Not a substitute for `task test:editmode` / `task test:playmode` — run both.

## Steps
1. Open project in Unity Editor (`6000.5.2f1`), not Unity Hub only.
2. Open the scene under test — for tile-tap, `Assets/Scenes/HelloWorld.unity`.
3. Press Play.
4. Tap/click a tile. Check against `/docs/design.md` conventions:
   - Correct tap → expected response (`TileTapHandler.cs`, `TileAnimator.cs`, `TileAudioSource.cs`).
   - Wrong or missed tap → redirects to a hint/retry, never a fail/error state.
   - Tap target reads as ≥44pt equivalent at the Editor's Game view scale.
   - Depth/overlap looks right — Sorting Layer order, not Z-position.
5. Stop Play. Note anything off in code, not just visually (fix in `TileController.cs` etc., then re-run batchmode tests).

## Quick Reference

| Question | Answer |
|---|---|
| Does logic work? | `task test:editmode` / `task test:playmode` |
| Does it feel right for a 4yo? | This skill — Editor Play, by hand |
| Both needed before merge? | Yes — batchmode catches regressions, Editor catches feel |

## Common Mistakes
- Trusting green batchmode tests as proof the interaction is fun/clear — it isn't, it only proves no exceptions/assertions fired.
- Testing in Game view at a zoomed-in scale that hides real tap-target size — check at the target device aspect ratio (iPad).
