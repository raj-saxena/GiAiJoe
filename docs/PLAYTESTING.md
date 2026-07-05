# Playtesting Guide (for humans)

No technical background needed. This checks the "hello world" tile-tap works the way a 4-year-old would experience it.

## How to try it

**Option A — on an iPad/Android device (preferred):**
1. Ask the developer for the latest build, or build it yourself with `task build` (see repo root).
2. Install it on the device and open the app.

**Option B — on a computer, via Unity:**
1. Open the project in Unity Hub.
2. Open the scene `Assets/Scenes/HelloWorld.unity`.
3. Press the Play button at the top of the editor.

## What to check

- [ ] **Tap the tile.** It should bounce and play a short beep, right away — no delay.
- [ ] **Tap it again and again.** Every tap should respond the same way. Nothing should ever "break," freeze, or show an error/red screen.
- [ ] **Nothing punishes a "wrong" tap.** There's no wrong tap — tapping anywhere on or off the tile should never show a fail message, timer, or game-over screen. If you see one, that's a bug.
- [ ] **The tile is easy to hit.** Try tapping quickly with a fingertip near the edge of the tile, not just dead-center. It should still register. A 4-year-old's aim is imprecise — this needs to be forgiving.
- [ ] **It's pleasant, not overwhelming.** No jarring sounds, no flashing, no sudden loud audio.

## If something looks wrong

Note what you tapped, what you expected, and what actually happened (a screen recording helps a lot), then pass it to the developer. Don't worry about diagnosing *why* — just describe what you saw.
