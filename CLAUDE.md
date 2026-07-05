# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project
Unity 2.5D isometric game (Sorting Layers, not true 3D) for a 4-year-old.
Targets: iPad primary, Android optional.
Language: C#.

## Conventions
- All gameplay logic gets a Unity Test Framework (UTF) EditMode or PlayMode test before merge.
- No fail states — redirects only, per /docs/design.md.
- Tap targets minimum 44pt equivalent.
- Isometric depth via Sorting Layers/Order in Layer, not Z-position — keep this consistent project-wide.
- Test runs overwrite the canonical `TestResults/editmode-results.xml` / `playmode-results.xml` — no ad-hoc copies (`-verify`, `-final-check`, etc.).
- Rendering: Universal Render Pipeline (URP) with 2D Renderer — no Built-in pipeline features.
- Any `.unity`/`.prefab` file touched by hand or by an agent (not just through the Unity Editor) must pass `task validate:scenes` before commit — catches corrupted Transform hierarchies (duplicate/self-referential Transforms, parent cycles) that hang the Editor on scene open and that UTF tests can't see. Runs automatically as a pre-commit hook and as the first step of `task test`.

## Commands
- Validate scene hierarchies (fast, no Unity launch): `task validate:scenes`
- Test (batchmode, EditMode): `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -runTests -testPlatform editmode -testResults ./TestResults/editmode-results.xml -logFile -`
- Test (batchmode, PlayMode): `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -runTests -testPlatform playmode -testResults ./TestResults/playmode-results.xml -logFile -`
- Build iOS: `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -executeMethod BuildScript.BuildiOS -logFile -`
- Build Android: `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -executeMethod BuildScript.BuildAndroid -logFile -`
