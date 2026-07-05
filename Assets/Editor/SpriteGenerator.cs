using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public static class SpriteGenerator
{
    /// <summary>
    /// Generates a simple white square sprite and saves it as an asset.
    /// Returns the sprite for use in scenes.
    /// </summary>
    public static Sprite GenerateWhiteSquareSprite()
    {
        string assetPath = "Assets/Resources/Sprites/WhiteSquare.png";

        // Create directories if they don't exist
        string directory = Path.GetDirectoryName(assetPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Check if sprite already exists
        Sprite existingSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        if (existingSprite != null)
        {
            return existingSprite;
        }

        // Create a simple 128x128 white texture
        Texture2D texture = new Texture2D(128, 128, TextureFormat.RGBA32, false);
        Color[] colors = new Color[128 * 128];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.white;
        }
        texture.SetPixels(colors);
        texture.Apply();

        // Save the texture as PNG
        byte[] pngData = texture.EncodeToPNG();
        File.WriteAllBytes(assetPath, pngData);
        Object.DestroyImmediate(texture);

        // Import and configure the texture
        AssetDatabase.ImportAsset(assetPath);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(assetPath);
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();

        // Load and return the sprite
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
        return sprite;
    }
}
