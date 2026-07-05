# Animal Habitat Level 1 Sprites

## Status: PLACEHOLDER SPRITES (Ready for Testing, Not for Production)

This directory contains temporary placeholder sprite assets for the Animal Habitat Level 1 gameplay scene. These are functional placeholders generated programmatically to enable end-to-end testing of the level's drag-and-drop mechanics and visual feedback systems.

## What's Here

### Animal Sprites (56×56 px, 1:1 aspect ratio)

- **Lion.png**: Orange circle placeholder (represents the Lion)
- **Fish.png**: Blue circle placeholder (represents the Fish)
- **Cow.png**: Brown circle placeholder (represents the Cow)

### Habitat Sprites (180×80 px, ~2.25:1 aspect ratio)

- **Ocean.png**: Deep blue rectangle (represents the Ocean habitat)
- **Savanna.png**: Golden tan rectangle (represents the Savanna habitat)
- **Farm.png**: Green rectangle (represents the Farm habitat)

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

## Next Steps: Sourcing Real Art

Before shipping, replace these placeholders with real artwork from a CC0 or commercial-use-licensed pack:

1. **Recommended Sources**:
   - **Kenney.nl** (https://kenney.nl/) — CC0, excellent quality, specifically for games
   - **Open Game Art** (https://opengameart.org/) — Community assets, check licenses
   - **Itch.io** (https://itch.io/) — Search for free/CC0 animal and habitat packs

2. **Requirements for Replacement Assets**:
   - License must allow commercial use (CC0 or equivalent)
   - Format: Transparent PNG (RGBA)
   - Animals: 56×56 px (1:1 ratio) or proportional
   - Habitats: 180×80 px (~2.25:1 ratio) or proportional
   - Style: Toddler-friendly, recognizable, non-scary

3. **Update License Documentation**:
   - Once real art is sourced, update `/Assets/Art/THIRD_PARTY_LICENSES.md` with pack name, source URL, license type, and attribution text
   - Ensure attribution requirements are met before shipping

4. **Re-Import Settings**:
   - When new PNGs are imported, verify they use the same import settings above
   - Use the included .meta files as a template for any new sprites

## Testing Notes

- These placeholders are sufficient for all PlayMode and EditMode tests
- Sprite dimensions and collider sizes match spec requirements
- Visual appearance (colors and labels) help developers identify animals/habitats during testing
- No performance penalties from placeholders

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
