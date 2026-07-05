# Roadmap

Milestones, tasks, and assignees. Agent names refer to `.claude/agents/*.md`.

## M0 — Bootstrap (done)

- [x] Unity project scaffolding (Assets/, ProjectSettings/, Packages/) — `bootstrap-platform-engineer`
- [x] Hello-world tile-tap vertical slice (bounce + beep) — `game-developer`
- [x] EditMode/PlayMode test scaffolding, 18/18 passing — `game-developer`
- [x] First iOS + Android builds — `bootstrap-platform-engineer`

## M1 — Hardening (pending)

- [ ] CI: GitHub Actions workflow running `task test` and validating `task build` on every PR — `bootstrap-platform-engineer`
- [ ] iOS codesigning/provisioning for release builds (currently dev-build only, see TODO in `Assets/Editor/BuildScript.cs`) — `bootstrap-platform-engineer`
- [ ] Expand `README.md` beyond the placeholder heading — human
- [ ] Apple Kids Category / COPPA compliance pass before any external testing — `compliance-agent`

## Backlog (unscheduled)

- [ ] First real level beyond the hello-world spike — `game-level-designer` to spec, `game-developer` to implement
