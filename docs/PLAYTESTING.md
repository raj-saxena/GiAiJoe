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

## Animal Habitat Match Level 1

Test the drag-and-drop animal matching game. This level has 3 animals (Lion, Fish, Cow) that must be dragged to their matching habitats (Savanna, Ocean, Farm).

**Option A — on an iPad/Android device (preferred):**
1. Ask the developer for the latest build, or build it yourself with `task build`.
2. Install it on the device and open the app.

**Option B — on a computer, via Unity:**
1. Open the project in Unity Hub.
2. Open the scene `Assets/Scenes/AnimalHabitatLevel1.unity`.
3. Press the Play button at the top of the editor.

### What to check

- [ ] **Each animal is easy to grab.** Try dragging each of the three animals (Lion, Fish, Cow) from their starting positions. The animal should follow your touch/mouse with no lag.
- [ ] **Correct drops lock in place.** Drag the Lion onto the Savanna, Fish onto the Ocean, and Cow onto the Farm. Each animal should snap to the habitat and stay there. They should not be draggable again.
- [ ] **Wrong drops bounce back gently.** Try dragging Lion onto the Ocean or Fish onto the Farm (wrong matches). A short "boing" sound plays and the animal returns to where it started. (Note: this currently snaps back instantly rather than sliding — a smoother wobble/glide animation is a planned follow-up, not a bug.) No error message, no buzzer-like sound — just a gentle redirect.
- [ ] **Empty-space drops are safe.** Try dragging an animal and releasing it in empty space (not on any habitat). It should quietly return to its starting position. No punishment, no message.
- [ ] **All three correct matches trigger a fanfare.** Once all three animals are in their correct habitats, a triumphant chime should play. (Note: a full visual celebration — animals bouncing, habitats glowing — is a planned follow-up, not implemented yet; the audio cue is the main signal for now.)
- [ ] **Level restarts automatically.** About 1.5 seconds after the fanfare, the level should reload itself from the start (since there's only one level right now, this is a loop, not real progression).
- [ ] **Animals are big and easy to tap.** Try dragging quickly with a fingertip, aiming near the edge of each animal sprite, not dead-center. It should still grab. A 4-year-old's aim is rough — this must be forgiving.
- [ ] **No failures, no frustration.** Nothing should ever say "wrong," "fail," "try again," or show a red/angry screen. Wrong drops feel playful, not punishing. A child should never feel stuck or lost.
- [ ] **Sounds are pleasant.** The correct chime should be bright and happy. The wrong-drop "boing" should be whimsical, not scary. The level-complete fanfare should feel triumphant and fun, not overwhelming.

### If something looks wrong

Note what you dragged, where you dropped it, what you expected, and what actually happened (a screen recording helps a lot), then pass it to the developer. Don't worry about diagnosing *why* — just describe what you saw.
