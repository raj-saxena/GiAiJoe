# Animal Habitat Matching Level 1 Spec

## Overview
First real gameplay level: drag-and-drop animal sprites onto matching habitat zones. Toddler-friendly (4+ years) with no fail states, only playful redirects on wrong drops. Theme: animals to habitats. Exactly 3 animal-habitat pairs.

## Animal-Habitat Pairs (Chosen)

### Pair 1: Lion → Savanna
- **Visual Distinctness:** Large orange cat with mane vs. golden/tan grassland with scattered rocks.
- **Toddler Appeal:** Lion is instantly recognizable as "big yellow cat," strong thematic match to wide open grass.
- **Naming:** Simple one-word names ("Lion," "Savanna").

### Pair 2: Fish → Ocean
- **Visual Distinctness:** Colorful small aquatic creature vs. deep blue/cyan water with waves and bubbles.
- **Toddler Appeal:** Fish is a universal first-animal concept, water is obviously "blue and splashy."
- **Naming:** Simple one-word names ("Fish," "Ocean").

### Pair 3: Cow → Farm
- **Visual Distinctness:** Black-and-white or brown bovine with distinct spots vs. green grass with a barn/fence element.
- **Toddler Appeal:** Cows are highly recognizable from books/songs ("moo"), farm is a warm, enclosed safe space.
- **Naming:** Simple one-word names ("Cow," "Farm").

**Why This Set:** Each pair is visually and thematically distinct (savanna vs. aquatic vs. pastoral). Names are single syllables or clear words. Colors don't overlap (orange/gold, blue, brown/white). Three pairs give clear gameplay loop without overwhelming.

---

## Isometric Grid Layout

### Grid Dimensions
- **Canvas Resolution Target:** iPad landscape (1024×768 logical, 2048×1536 physical at 2x DPI).
- **Isometric Tile Base:** 64×32 pixels per tile (standard isometric proportions: width 2× height).
- **Playable Area:** Roughly 10 tiles wide × 8 tiles tall in isometric space (not a hard grid; layout is suggestive).

### Habitat Placements (Isometric Grid Coordinates)
Use (x, y) notation where x increases right, y increases downward in a top-down grid view:

```
Isometric Grid Layout (text diagram):

        [Ocean]              [Savanna]
                                          [Farm]

     [Fish]        [Cow]              [Lion]
     (start)       (start)            (start)
```

**Concrete Positions (Tile Grid):**

| Element | Grid Position | Purpose |
|---------|---------------|---------|
| Ocean Habitat | (2, 1) | Deep center-left zone, 3×3 tiles |
| Savanna Habitat | (6, 1) | Center-right zone, 3×3 tiles |
| Farm Habitat | (10, 1) | Right zone, 3×3 tiles |
| Fish (Start) | (1, 6) | Bottom-left, clearly separate from habitats |
| Cow (Start) | (4, 6) | Bottom-center, clearly separate |
| Lion (Start) | (8, 6) | Bottom-right, clearly separate |

**Background Decoration:**
- Isometric checkerboard or simple gradient background (tan/beige base).
- No interactive tiles; background is visual only.
- Optional subtle tile grid visualization to reinforce isometric space (do not interfere with animals).

---

## Tap Target Sizing

### Animal Sprites (Drag Sources)
- **Minimum Bounding Box:** 64×64 pixels (logical) = 128×128 physical (2x). This exceeds 44pt minimum.
- **Actual Sprite Dimensions:** Plan for 56×56 pixels (logical, accounts for ~8px padding from collider).
- **Hitbox:** BoxCollider2D matching sprite bounds, **not** a trigger initially—becomes trigger during drag to allow hover detection.
- **Rationale:** 64×64 logical is well above 44pt (~44 logical pixels ≈ 88 physical), easy for small fingers.

### Habitat Drop Zones
- **Minimum Bounding Box per Habitat:** 3 tiles wide × 3 tiles tall = 192×96 pixels (logical) = 384×192 physical (2x).
- **Actual Drop Zone Collider:** BoxCollider2D, **is Trigger**, 180×80 pixels (logical, allows some margin for visual effects).
- **Rationale:** Far exceeds 44pt minimum; large enough for toddler imprecision in dragging.

---

## Sorting Layer & Order-in-Layer Plan

This project uses **Sorting Layers exclusively** for depth—never Z-position. Layers are rendered in order; Order in Layer breaks ties within a layer.

### Sorting Layer Definitions

| Layer Name | Purpose | Order in Layer Range |
|-------------|---------|---------------------|
| Background | Static tilemap/gradient, checkerboard | 0–10 |
| Habitats | Habitat sprites (Ocean, Savanna, Farm) | 20–30 |
| Animals | Animal sprites at rest or being dragged | 40–50 |
| UI | Labels, optional UI overlays | 60–70 |
| AnimalsInTransit | Animal during drag or redirect animation | 100–110 |

### Specific Layer Assignments

**Background Layer (renders first):**
- Isometric tile checkerboard or gradient: Order 0.
- Decorative background elements (clouds, sun outline): Order 5–10.

**Habitats Layer:**
- Ocean habitat sprite: Order 20.
- Savanna habitat sprite: Order 22.
- Farm habitat sprite: Order 24.
- (Stagger to avoid aliasing in isometric perspective; not critical but good practice.)

**Animals Layer (at rest):**
- Fish sprite at start position: Order 40.
- Cow sprite at start position: Order 40.
- Lion sprite at start position: Order 40.
- (When idle, all at same Order. During drag, move to AnimalsInTransit layer.)

**AnimalsInTransit Layer (while dragging or redirecting):**
- Order 100–110 (all dragging/animating animals render above everything else).
- When drag ends and animal locks to habitat: move back to Animals Layer, update position.

### Implementation Rule
When an animal is **picked up for dragging**, immediately set its Sorting Layer to "AnimalsInTransit" with Order 100. On **drop (correct or redirect)**, during the animation (lock or wobble-back), keep at AnimalsInTransit. Once animation completes and animal is at rest, set Sorting Layer back to "Animals" with Order 40.

---

## Interaction & UX: Timing & Feel

### A. Picking Up an Animal (Drag Start)
- **Input:** Touch/drag on animal sprite (BoxCollider2D hit).
- **Visual Feedback:**
  - Animal sprite scales up 1.0 → 1.1 over 0.1s (slight growth = "picked up").
  - Shadow/outline intensifies slightly (optional, helps readability).
  - Immediately move Sorting Layer to AnimalsInTransit (Order 100) so it floats above habitats.
- **Audio:** None (or optional short "pop" sound, ~200ms, friendly tone).
- **Latency Requirement:** Touch-to-visual must be <50ms; frame lag should not prevent responsive feel.

### B. Dragging (In Transit)
- **Visual:**
  - Animal follows finger/touch point in real-time (no lerp lag; immediate position tracking).
  - Animal remains slightly enlarged (1.1 scale).
  - Optional faint glow or color tint.
- **Habitat Feedback on Hover:**
  - When animal touches habitat's drop zone (triggers BoxCollider2D overlap), habitat pulses or brightens slightly (0.2s ease-out pulse).
  - This signals to toddler "this is a place to drop."
- **Audio:** None; optional subtle loop (1–2 second ambient loop if implemented, but not required).

### C. Correct Drop (Match Found)
- **Drop Logic:** Animal's center must overlap with correct habitat's drop zone collider for >0.1s to register as "correct."
- **Visual Sequence (timing):**
  1. **Lock Animation (0.3s):** Animal sprite snaps to habitat center, scales 1.1 → 1.0, fades/locks in place.
  2. **Habitat Celebrates (0.2s):** Habitat pulses outward (1.0 → 1.15 scale) once, glow intensifies.
  3. **Resting State:** Animal and habitat stay in place, both at rest (Animals Layer, Order 40–50).
- **Audio:**
  - Correct-placement **chime:** Bright, ascending pitch (e.g., C–E–G progression in pentatonic scale), ~0.4s total, plays at lock moment.
  - Tone: celebratory, warm, not jarring.
- **Sorting:** During lock animation, stay at AnimalsInTransit. Once at rest, move to Animals Layer.

### D. Wrong Drop (Mismatch → Playful Redirect)
- **Drop Logic:** Animal's center overlaps a habitat's drop zone for >0.1s **but is the wrong habitat**.
- **Visual Sequence (timing):**
  1. **Wobble (0.4s):** Animal stays briefly at drop location, then shakes side-to-side (amplitude ~16 pixels, 6–8 oscillations over 0.3s). This reads as "oops, not here!" without being frustrating.
  2. **Glide Back (0.5s):** Animal smoothly eases back to its original start position (easing curve: ease-out-cubic). Scale remains normal (1.0) during glide.
  3. **Land Gently (0.1s):** Slight bounce at start position (scale 1.0 → 1.05 → 1.0) as animal returns.
- **Audio:**
  - Wrong-drop **reaction cue:** Short, descending pitch slide (e.g., E–C) or playful "boing" sound, ~0.3s total, plays at wobble moment.
  - Tone: whimsical, not punitive (like a toy surprise, not a buzzer).
- **Sorting:** Stay at AnimalsInTransit throughout wobble and glide (Order 100). After land animation, move back to Animals Layer.

### E. Level Complete (All 3 Pairs Matched)
- **Trigger:** All 3 animals locked to correct habitats and at rest for >1.0s.
- **Visual Sequence (timing):**
  1. **Background Pulse (0.6s):** Screen flashes gently (1–2 white/yellow overlay at low opacity, fades out) or background color brightens.
  2. **Animal Celebration (1.0s):** All 3 animals animate upward (translate +40 pixels over 1.0s with ease-out, slightly bouncy), scale pulses 1.0 → 1.2 → 1.0 over 1.0s.
  3. **Habitat Sparkle (0.8s):** All habitat zones glow/sparkle with 2–3 twinkling points (optional particle effects, low intensity).
  4. **Auto-Advance Delay (1.5s):** Level holds celebration for 1.5s, then auto-advances (see "future levels" note below).
- **Audio:**
  - Level-complete **fanfare:** Uplifting, multi-note chime or trumpet-like progression (e.g., C–E–G–C in major scale), ~1.0s duration, plays at start of celebration phase.
  - Ambient loop (if present during gameplay) fades out over 0.5s as fanfare plays.
  - Tone: triumphant, joyful, rewards success without being overwhelming.

### F. Background Ambient Loop (Optional, Plays During Level)
- **Duration:** 4–8 seconds, looping seamlessly.
- **Tone:** Gentle, uplifting, thematic (e.g., soft strings, light percussion, nature sounds).
- **Volume:** Low enough not to interfere with SFX cues (correct/wrong/fanfare).
- **Requirement:** Not mandatory for MVP; note in implementation if deferred.

---

## Scene Structure

### Scene Name
**AnimalHabitat1.unity** (or similar; clear naming convention)

### GameObject Hierarchy (Suggestive)
```
AnimalHabitat1
├── Camera (Orthographic, 1024×768 logical viewport)
├── Background
│   ├── Tilemap/Checkerboard (Sprite Renderer, Sorting Layer "Background" Order 0)
│   └── Decorations (clouds, sun) (Sorting Layer "Background" Order 5–10)
├── Habitats
│   ├── OceanHabitat (Sprite + BoxCollider2D Trigger, Sorting Layer "Habitats" Order 20)
│   ├── SavannaHabitat (Sprite + BoxCollider2D Trigger, Sorting Layer "Habitats" Order 22)
│   └── FarmHabitat (Sprite + BoxCollider2D Trigger, Sorting Layer "Habitats" Order 24)
├── Animals
│   ├── Fish (Sprite + BoxCollider2D non-Trigger, Sorting Layer "Animals" Order 40)
│   ├── Cow (Sprite + BoxCollider2D non-Trigger, Sorting Layer "Animals" Order 40)
│   └── Lion (Sprite + BoxCollider2D non-Trigger, Sorting Layer "Animals" Order 40)
├── GameManager
│   ├── LevelController (monobehavior, handles drag logic, matching, completion)
│   ├── AudioManager (plays cues)
│   └── AnimationCoordinator (handles all sprite/UI animations)
└── Canvas (UI Layer, Sorting Layer "UI" Order 60)
    └── Optional labels or HUD (deferred if not needed for MVP)
```

---

## Sprite Asset Requirements

Each sprite should be sourced from a free CC0/Kenney-style asset pack or equivalent. **Do not assume specific existing files**—these are requirements for sourcing.

### Animal Sprites
| Animal | Dimensions (Logical) | Aspect Ratio | Notes |
|--------|----------------------|--------------|-------|
| Lion | 56×56 px | 1:1 | Large orange/gold, mane visible, simple profile or front-facing |
| Fish | 56×56 px | 1:1 | Bright colors (blue, yellow, red), simple side-facing profile, recognizable as aquatic |
| Cow | 56×56 px | 1:1 | Brown or black-and-white, spots visible, simple side-facing profile |

### Habitat Sprites
| Habitat | Dimensions (Logical) | Aspect Ratio | Notes |
|---------|----------------------|--------------|-------|
| Ocean | 180×80 px | ~2.25:1 (isometric-friendly) | Deep blue background, waves/bubbles, maybe a seaweed element |
| Savanna | 180×80 px | ~2.25:1 (isometric-friendly) | Golden/tan grass, scattered rocks, maybe a sun or distant tree |
| Farm | 180×80 px | ~2.25:1 (isometric-friendly) | Green grass, fence or barn silhouette, maybe a silo |

### Sprite Framing (Isometric Consideration)
- Habitats should be framed as if viewed from above-right (45° angle typical isometric), with ground plane clearly visible.
- Animals are small overlays; can be simpler front/side views (not strict isometric).
- All sprites should have transparent backgrounds (PNG format).

---

## Audio Asset Requirements

Four distinct audio cues required (exact audio files sourced separately; spec defines **feel/tone only**).

| Cue | Duration | Tone / Pitch | Notes |
|-----|----------|-----------|-------|
| **Correct-Placement Chime** | ~0.4s | Bright, ascending (pentatonic or major scale), mid-to-high pitch (C4–G4 range) | Celebratory, warm, no harshness |
| **Wrong-Drop Reaction** | ~0.3s | Descending or playful "boing" (low-to-mid pitch), whimsical | Not punitive; reads as toy-like surprise |
| **Level-Complete Fanfare** | ~1.0s | Ascending multi-note progression (C–E–G–C or similar major scale), mid-to-high pitch | Triumphant, uplifting, joyful |
| **Ambient Background Loop** | 4–8s | Gentle, thematic (soft strings, light percussion, nature sounds, or combination) | Low volume, unobtrusive, loops seamlessly |

**Implementation Note:** Audio can be procedurally generated (sine waves) or sourced from free audio packs. Ensure all audio is CC0 or properly licensed for commercial use.

---

## Level Completion & Auto-Advance Hook

### Current Behavior (MVP)
- Once all 3 animals are matched to correct habitats and celebration animation completes, the level auto-advances.
- **For now:** Auto-advance simply reloads the same level (AnimalHabitat1).
- **Mechanism:** Call a static method `LevelManager.AdvanceLevel()` or load scene by name.

### Future Levels Hook
A dedicated `LevelManager` or `GameController` singleton should track:
- Current level index or level name.
- A list/array of available levels (loaded from a ScriptableObject `LevelDefinition` asset or similar data structure).
- On completion, fetch the next level and load it; if no next level, loop back to first.

**Outline (not implemented now, but note architecture):**
```csharp
public class LevelManager : MonoBehaviour
{
    public static void AdvanceLevel()
    {
        // Load next level from LevelDefinition array
        // If no next, loop to first
    }
}
```

---

## Future Levels Spec Notes

### What Varies Between Levels (Parameterization)
1. **Pair Count:** This level has 3 pairs. Future levels might have 2, 4, or 5.
2. **Animal Sprites:** Different animals (e.g., Dog, Cat, Bird, Elephant).
3. **Habitat Sprites:** Different habitats (e.g., Forest, Desert, Mountain, House).
4. **Layout:** Animal and habitat grid positions can differ (e.g., 2×2 vs. 1×3 arrangement).
5. **Difficulty Nuance:** (Deferred to backlog) Pair hints (color-matching, name labels), spacing (to reduce accidental correct drops), animation difficulty.

### Architecture for Extensibility
Define a `LevelDefinition` ScriptableObject structure:

```csharp
[System.Serializable]
public class AnimalHabitatPair
{
    public Sprite animalSprite;
    public Sprite habitatSprite;
    public Vector2 animalStartPosition;
    public Vector2 habitatPosition;
}

public class LevelDefinition : ScriptableObject
{
    public string levelName;
    public int levelIndex;
    public List<AnimalHabitatPair> pairs;
    public Sprite backgroundSprite;
    // Future: difficulty modifiers, hints, etc.
}
```

Each new level creates a new `LevelDefinition` asset with its own pair list and sprite assignments. The `LevelController` reads the definition and dynamically instantiates animals/habitats from it.

---

## Implementation Checklist (For Game Developer)

- [ ] Create AnimalHabitat1.unity scene
- [ ] Define 4 Sorting Layers: Background, Habitats, Animals, AnimalsInTransit, UI
- [ ] Place 3 habitat sprites at grid positions per layout; attach BoxCollider2D (Trigger) with proper size
- [ ] Place 3 animal sprites at start positions; attach BoxCollider2D (non-Trigger) for drag input
- [ ] Implement LevelController monobehavior:
  - [ ] Drag input detection (OnMouseDown/OnMouseDrag/OnMouseUp or Touch API)
  - [ ] Correct/wrong drop detection (collider overlap, identity check)
  - [ ] Correct-drop animation: lock, scale, habitat pulse
  - [ ] Wrong-drop animation: wobble, glide back
  - [ ] All 3 matched detection → celebration animation
  - [ ] Auto-advance on celebration end
- [ ] Implement AnimationCoordinator monobehavior:
  - [ ] Scale, position, color tweens (use DOTween or Coroutines)
  - [ ] Sorting Layer transitions for drag state
- [ ] Implement AudioManager monobehavior:
  - [ ] Play correct cue, wrong cue, fanfare, ambient loop
- [ ] Source/place sprite assets (Lion, Fish, Cow, Ocean, Savanna, Farm)
- [ ] Source/place audio assets (correct chime, wrong reaction, fanfare, ambient)
- [ ] Write PlayMode tests:
  - [ ] Correct drop triggers match and locks animal
  - [ ] Wrong drop triggers wobble-back to start position
  - [ ] All 3 matched triggers celebration animation
  - [ ] Sorting Layers update correctly during drag/drop
  - [ ] Audio cues play at correct moments
- [ ] Verify all tap targets meet 44pt minimum on iPad and Android breakpoints

---

## Design Notes & Rationale

### Why No Fail States?
Wrong drops (wobble + redirect) are playful redirects, not punishments. A 4-year-old should never feel "stuck" or "wrong"—only guided back gently. The wobble and whimsical sound make the redirect fun, not frustrating.

### Why Sorting Layers, Not Z-Position?
This project uses URP 2D Renderer, which does not support Z-based depth in the same way 3D does. Sorting Layers are the canonical way to order 2D sprites in isometric perspective. This avoids depth-fighting, camera clipping, or inconsistent rendering between platforms.

### Why 3 Pairs?
Cognitive load for a 4-year-old is limited. 3 pairs = manageable loop (pick, drag, drop, repeat), clear completion moment. 2 pairs feel too short; 5+ pairs risk fatigue. Once the engine is proven, future levels can experiment with pair count.

### Why These 3 Animals/Habitats?
- **Lion + Savanna:** Iconic, high visual contrast (orange vs. golden tan), strong thematic bond.
- **Fish + Ocean:** Universal aquatic concept, distinct blue color, easy to animate (swimming motion optional).
- **Cow + Farm:** Recognizable from toddler media, pastoral warmth, clear safe-space vibe.
All three have simple, single-syllable or clear names, and sprite representations are widely available in free asset packs.

### Why 64×64 Tap Targets?
iPad standard touch precision is ~44pt, but larger is safer for toddlers. 64×64 (logical) is still compact enough to fit 3 animals on screen without overlap, and leaves room for landing feedback animations without covering other UI.

---

## References
- Project Constraints: `/docs/design.md` (no fail states, Sorting Layers, 44pt tap targets)
- Rendering Setup: CLAUDE.md (URP 2D Renderer)
- Prior Spec Example: `/docs/specs/tile-tap-hello-world.md` (format reference)
