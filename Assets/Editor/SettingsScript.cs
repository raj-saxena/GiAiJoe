using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SettingsScript
{
    [MenuItem("Tools/Configure Project Settings")]
    public static void ConfigureProjectSettings()
    {
        ConfigurePlayerSettings();
        ConfigureSortingLayers();
        Debug.Log("Project settings configured successfully!");
    }

    private static void ConfigurePlayerSettings()
    {
        // Set Portrait orientation
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

        // Set IL2CPP backend for iOS and Android
        PlayerSettings.SetScriptingBackend(NamedBuildTarget.iOS, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);

        Debug.Log("Player Settings configured: IL2CPP backend, Portrait orientation");
    }

    private static void ConfigureSortingLayers()
    {
        // Get the TagManager asset
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]
        );

        SerializedProperty sortingLayers = tagManager.FindProperty("m_SortingLayers");

        // Check if "Tiles" sorting layer exists
        bool tilesExists = false;
        for (int i = 0; i < sortingLayers.arraySize; i++)
        {
            SerializedProperty layer = sortingLayers.GetArrayElementAtIndex(i);
            SerializedProperty name = layer.FindPropertyRelative("name");
            if (name.stringValue == "Tiles")
            {
                tilesExists = true;
                break;
            }
        }

        if (!tilesExists)
        {
            // Add new sorting layer
            sortingLayers.InsertArrayElementAtIndex(sortingLayers.arraySize);
            SerializedProperty newLayer = sortingLayers.GetArrayElementAtIndex(sortingLayers.arraySize - 1);
            newLayer.FindPropertyRelative("name").stringValue = "Tiles";
            newLayer.FindPropertyRelative("uniqueID").intValue = 1;
        }

        tagManager.ApplyModifiedProperties();
        Debug.Log("Sorting layer 'Tiles' configured");
    }
}
