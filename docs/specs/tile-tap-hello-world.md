# Tile Tap Hello World Spec

## Overview
Minimal vertical slice: one tile, tap it, it bounces and beeps. Used to verify the entire toolchain end-to-end.

## Tile GameObject
- **Dimensions:** 2×2 world units (square).
- **Sprite:** Built-in white square (Unity's default sprite or procedural).
- **Sorting Layer:** "Tiles"
- **Order in Layer:** 0
- **Collider:** BoxCollider2D, 2×2 size, is Trigger.

## Interaction
- **Tap Detection:** Touch input detected via BoxCollider2D hit.
- **Bounce Animation:** Scale animates 1.0 → 1.2 → 1.0 over ~0.2 seconds.
- **Audio:** Procedurally generated sine-wave beep (~440 Hz, ~0.1s duration).
- **Tap Target Size:** Minimum 44pt equivalent on iPad viewport.

## Scene
- **Name:** HelloWorld.unity
- **Camera:** Orthographic, no follow logic.
- **Background:** Default gray.
