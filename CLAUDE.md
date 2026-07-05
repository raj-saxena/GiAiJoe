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

## Commands
- Test (batchmode, EditMode): `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -runTests -testPlatform editmode -testResults ./TestResults/editmode-results.xml -logFile -`
- Test (batchmode, PlayMode): `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -runTests -testPlatform playmode -testResults ./TestResults/playmode-results.xml -logFile -`
- Build iOS: `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -executeMethod BuildScript.BuildiOS -logFile -`
- Build Android: `/Applications/Unity/Hub/Editor/6000.5.2f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath . -executeMethod BuildScript.BuildAndroid -logFile -`
