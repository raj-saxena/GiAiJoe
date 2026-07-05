using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SetupInitialScene
{
    [MenuItem("Tools/Setup Initial Scene")]
    public static void SetupScene()
    {
        // Create a new scene
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Find the main camera and configure it
        Camera camera = Camera.main;
        if (camera != null)
        {
            camera.orthographic = true;
            camera.orthographicSize = 5f;
        }

        // Save the scene
        string scenePath = "Assets/Scenes/Default.unity";
        EditorSceneManager.SaveScene(newScene, scenePath);

        Debug.Log($"Scene saved to {scenePath}");

        // Add the scene to build settings
        var scenes = EditorBuildSettings.scenes;
        System.Collections.Generic.List<EditorBuildSettingsScene> sceneList =
            new System.Collections.Generic.List<EditorBuildSettingsScene>(scenes);

        bool sceneExists = false;
        foreach (var scene in sceneList)
        {
            if (scene.path == scenePath)
            {
                sceneExists = true;
                break;
            }
        }

        if (!sceneExists)
        {
            sceneList.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = sceneList.ToArray();
            Debug.Log("Scene added to build settings");
        }
    }
}
