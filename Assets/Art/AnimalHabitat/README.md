# Animal Habitat Level 1 Sprites

## Status: REAL ART (Sourced 2026-07-05)

This directory contains licensed sprite assets for the Animal Habitat Level 1 gameplay scene, sourced to replace the original programmatic placeholders. See `/Assets/Art/THIRD_PARTY_LICENSES.md` for source/license/attribution detail per file.

## What's Here

### Animal Sprites (56×56 px, 1:1 aspect ratio)

- **Lion.png**: Standing side-profile lion (LPC Animals pack)
- **Fish.png**: Blue fish icon (Kenney Fish Pack)
- **Cow.png**: Round flat-style cow icon (Kenney Animal Pack Redux)

### Habitat Sprites (180×80 px, ~2.25:1 aspect ratio)

- **Ocean.png**: Tiled teal water texture (Kenney RPG Pack)
- **Savanna.png**: Tiled tan sand texture (Kenney RPG Pack)
- **Farm.png**: Tiled green grass texture (Kenney RPG Pack)

## Import Settings

All sprites use consistent Unity import settings:

| Setting | Value | Reason |
|---------|-------|--------|
| Texture Type | Sprite (2D and UI) | Required for 2D sprite rendering in URP |
| Sprite Mode | Single | Each file is one sprite |
| Pixels Per Unit | 100 | Consistent with other project sprites (e.g., WhiteSquare) |
| Filter Mode | Bilinear | Standard for 2D graphics |
| Compression | Default | Automatic per-platform optimization |
| Transparency | Enabled (Alpha Is Transparency) | All sprites use transparent PNG |
| Pivot | Center (0.5, 0.5) | Standard for interactive sprites |
| Mipmaps | Disabled | Not needed for UI/sprite graphics at fixed scale |

## Provenance

Each PNG was downloaded from its source pack (see `THIRD_PARTY_LICENSES.md`), then cropped/resized to the exact target dimensions with transparency preserved. Filenames and `.meta` GUIDs were kept identical to the placeholders they replaced, so no scene or script references needed updating.

## Testing Notes

- Sprite dimensions and collider sizes still match spec requirements (56×56 animals, 180×80 habitats)
- No performance penalties versus the placeholders — same format, same import settings

## File Structure

```
Assets/Art/AnimalHabitat/
├── Lion.png
├── Lion.png.meta
├── Fish.png
├── Fish.png.meta
├── Cow.png
├── Cow.png.meta
├── Ocean.png
├── Ocean.png.meta
├── Savanna.png
├── Savanna.png.meta
├── Farm.png
├── Farm.png.meta
├── README.md (this file)
```

All .meta files contain the standardized import settings; no manual re-configuration should be needed.
