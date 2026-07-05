# Roadmap

Milestones, tasks, and assignees. Agent names refer to `.claude/agents/*.md`.

## M0 — Bootstrap (done)

- [x] Unity project scaffolding (Assets/, ProjectSettings/, Packages/) — `bootstrap-platform-engineer`
- [x] Hello-world tile-tap vertical slice (bounce + beep) — `game-developer`
- [x] EditMode/PlayMode test scaffolding, 18/18 passing — `game-developer`
- [x] First iOS + Android builds — `bootstrap-platform-engineer`
- [x] Migrate Built-in RP to URP (2D Renderer) — `bootstrap-platform-engineer`

## M1 — First Real Level (done)

- [x] Animal Habitat Match Level 1 spec (`docs/specs/animal-habitat-level-1.md`) — `game-level-designer`
- [x] Reusable drag-match components (`Draggable`, `DropZone`, `LevelDefinition`, `LevelController`) — `game-developer`
- [x] `AnimalHabitatLevel1.unity` scene: 3 pairs (Lion/Savanna, Fish/Ocean, Cow/Farm), placeholder art, procedural placeholder audio — `game-developer`
- [x] EditMode + PlayMode tests, 25/25 + 19/19 passing — `game-developer`
- [x] Independent QA verification (tests, tap targets, no-fail-state audit, sorting layers, scene sanity) — `qa-verification-agent`
- [x] Playtesting checklist added to `docs/PLAYTESTING.md`

**Known follow-ups (not blocking, tracked below):** wrong-drop is an instant snap-back rather than the spec's wobble/glide animation; level completion plays a fanfare but no visual celebration yet; art and audio are procedural placeholders pending a real CC0 asset pack (see `Assets/Art/THIRD_PARTY_LICENSES.md`).

## M2 — Hardening (pending)

- [ ] CI: GitHub Actions workflow running `task test` and validating `task build` on every PR — `bootstrap-platform-engineer`
- [ ] iOS codesigning/provisioning for release builds (currently dev-build only, see TODO in `Assets/Editor/BuildScript.cs`) — `bootstrap-platform-engineer`
- [ ] Expand `README.md` beyond the placeholder heading — human
- [ ] Apple Kids Category / COPPA compliance pass before any external testing — `compliance-agent`

## Backlog (unscheduled)

- [ ] Source real CC0/licensed art pack to replace Level 1 placeholder sprites — `game-developer`
- [ ] Source or design real audio cues to replace Level 1 procedural placeholder tones — `game-developer`
- [ ] Wrong-drop wobble + glide-back animation (currently instant snap) — `game-developer`
- [ ] Visual level-complete celebration (animals bounce, habitats glow) per spec — `game-developer`
- [ ] Additional levels with growing difficulty (4, 5 pairs; new animal/habitat themes) — `game-level-designer` to spec, `game-developer` to implement
