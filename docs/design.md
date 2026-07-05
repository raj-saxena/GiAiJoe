# Design Principles

## No Fail States
All interactions redirect only. There are no failure states, loss conditions, or blocking errors in gameplay. When user input is invalid or a system fails, redirect gracefully to a valid game state.

## Depth via Sorting Layers, Never Z-Position
Isometric depth is managed exclusively via **Sorting Layers** and **Order in Layer** values in the Sprite Renderer. **Never use `transform.position.z` or depth-via-Z for gameplay depth.** This keeps the 2.5D isometric rendering consistent project-wide.

## Tap Targets
All interactive UI and gameplay elements must have tap targets of minimum 44pt equivalent (hardware independent).

## Target Audience
4-year-old: large, colorful, responsive, no time pressure, no failure.
