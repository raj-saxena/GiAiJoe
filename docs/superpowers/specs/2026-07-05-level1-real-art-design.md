# Level 1 Real Art Sourcing — Design

## Goal
Replace the 6 procedurally-generated placeholder sprites in `Assets/Art/AnimalHabitat/` with real, licensed artwork. Scope is Level 1 (Animal Habitat Match) only — `Assets/Resources/Sprites/WhiteSquare.png` stays untouched; it backs the toolchain-verification Hello World scene per `docs/specs/tile-tap-hello-world.md`, which deliberately specifies a procedural/default sprite.

## Assets & Sources

| File | Source | License | Attribution needed |
|------|--------|---------|---------------------|
| Cow.png | Kenney Animal Pack Redux (`opengameart.org/sites/default/files/kenney_animalPackRedux.zip`) | CC0 | No |
| Fish.png | Kenney Fish Pack (`opengameart.org/sites/default/files/kenney_fish-pack_2.0.zip`) | CC0 | No |
| Lion.png | LPC Animals 2022 (`opengameart.org/sites/default/files/lpc_animals_2022_v1.1.zip`) — Kenney has no lion | CC-BY 4.0 | Yes |
| Ocean.png, Savanna.png, Farm.png | Kenney Nature Kit / Background Elements (CC0), cropped/composited to fit each habitat's palette | CC0 | No |

Requirement carried over from the existing placeholder README: transparent PNG, animals 56×56 (1:1), habitats 180×80 (~2.25:1), toddler-friendly/non-scary style.

## Pipeline
1. Download the three zips to a scratch dir under `/tmp` (not committed), extract.
2. Pick source images: one static cow icon, one static fish icon, one cropped frame from the lion walk-cycle spritesheet (64×64/frame), and three habitat background crops.
3. Resize/crop with Pillow (or `sips` if simpler) to exact target dimensions, preserving transparency.
4. Overwrite the 6 files in place at their existing paths — same filenames, so existing `.meta` GUIDs stay valid and no `.unity`/`.prefab`/C# reference needs updating.
5. Update `Assets/Art/AnimalHabitat/README.md`: flip status from PLACEHOLDER to real art, note sources.
6. Update `Assets/Art/THIRD_PARTY_LICENSES.md`: replace the placeholder template with real entries per the format already in the file — CC0 packs get a short entry, LPC lion gets full attribution text.

## Verification
- `task validate:scenes` (cheap, catches nothing new here but is the standing pre-commit gate for anything touching `Assets/`).
- Re-run EditMode/PlayMode tests — sprite content changes shouldn't affect logic, but same-file overwrite must not break sprite loading (Unity re-imports on file change; existing `.meta` import settings — Sprite (2D and UI), Pixels Per Unit 100, pivot center — are reused).
- Open the scene (or screenshot via headless render if available) to visually sanity-check the new art doesn't look worse than the placeholders — non-scary, legible at target size.

## Risk
Asset-only change, same filenames/GUIDs — no code or scene-graph changes. Main risk is a source pack disappearing/license drift later; mitigated by recording exact download URLs in THIRD_PARTY_LICENSES.md now.
