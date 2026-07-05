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
- [ ] **Wrong drops bounce back gently.** Try dragging Lion onto the Ocean or Fish onto the Farm (wrong matches). The animal should shake briefly at the drop location (like a friendly "oops!"), then smoothly slide back to where it started. No error message, no buzzer — just a gentle redirect.
- [ ] **Empty-space drops are safe.** Try dragging an animal and releasing it in empty space (not on any habitat). It should quietly return to its starting position. No punishment, no message.
- [ ] **All three correct matches trigger celebration.** Once all three animals are in their correct habitats, the whole screen should light up with a joyful animation (animals might bounce up, habitats might glow). A happy chime sound should play.
- [ ] **Level repeats or advances.** After the celebration ends, the level should automatically advance (or restart, if there's only this level).
- [ ] **Animals are big and easy to tap.** Try dragging quickly with a fingertip, aiming near the edge of each animal sprite, not dead-center. It should still grab. A 4-year-old's aim is rough — this must be forgiving.
- [ ] **No failures, no frustration.** Nothing should ever say "wrong," "fail," "try again," or show a red/angry screen. Wrong drops feel playful, not punishing. A child should never feel stuck or lost.
- [ ] **Sounds are pleasant.** The correct chime should be bright and happy. The wrong-drop "boing" should be whimsical, not scary. The level-complete fanfare should feel triumphant and fun, not overwhelming.

### If something looks wrong

Note what you dragged, where you dropped it, what you expected, and what actually happened (a screen recording helps a lot), then pass it to the developer. Don't worry about diagnosing *why* — just describe what you saw.
