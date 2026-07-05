using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Collections.Generic;

public static class HelloWorldSceneSetup
{
    [MenuItem("Tools/Setup HelloWorld Scene")]
    public static void SetupHelloWorldScene()
    {
        // Create a new scene
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Configure camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 5f;
            mainCamera.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1f); // Default gray
        }

        // Create the tile GameObject
        GameObject tileGo = new GameObject("Tile");
        tileGo.transform.position = Vector3.zero;

        // Add SpriteRenderer
        SpriteRenderer spriteRenderer = tileGo.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = SpriteGenerator.GenerateWhiteSquareSprite();
        spriteRenderer.sortingLayerName = "Tiles";
        spriteRenderer.sortingOrder = 0;

        // Add BoxCollider2D (2x2 size, trigger)
        BoxCollider2D collider = tileGo.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(2f, 2f);
        collider.isTrigger = true;

        // Add game components via reflection (they're in GiAiJoe.Scripts assembly)
        AddComponentByTypeName(tileGo, "GiAiJoe.Gameplay.TileTapHandler");
        AddComponentByTypeName(tileGo, "GiAiJoe.Gameplay.TileAnimator");
        AddComponentByTypeName(tileGo, "GiAiJoe.Gameplay.TileAudioSource");
        AddComponentByTypeName(tileGo, "GiAiJoe.Gameplay.TileController");

        // Save the scene
        string scenePath = "Assets/Scenes/HelloWorld.unity";
        EditorSceneManager.SaveScene(newScene, scenePath);
        Debug.Log($"Scene saved to {scenePath}");

        // Add the scene to build settings
        var scenes = EditorBuildSettings.scenes;
        List<EditorBuildSettingsScene> sceneList = new List<EditorBuildSettingsScene>(scenes);

        bool helloWorldExists = false;
        foreach (var scene in sceneList)
        {
            if (scene.path == scenePath)
            {
                helloWorldExists = true;
                break;
            }
        }

        if (!helloWorldExists)
        {
            sceneList.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = sceneList.ToArray();
            Debug.Log("HelloWorld scene added to build settings");
        }
    }

    private static void AddComponentByTypeName(GameObject go, string typeName)
    {
        // Type.GetType(string) only searches mscorlib and the calling assembly
        // (this Editor script's assembly), not the separate GiAiJoe.Scripts
        // assembly the gameplay types live in - so it must be found by scanning
        // all loaded assemblies instead.
        Type componentType = null;
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            componentType = assembly.GetType(typeName);
            if (componentType != null)
            {
                break;
            }
        }

        if (componentType != null)
        {
            go.AddComponent(componentType);
        }
        else
        {
            Debug.LogError($"Could not find type: {typeName}");
        }
    }
}
