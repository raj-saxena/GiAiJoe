using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public static class BuildScript
{
    // TODO: Add iOS codesigning and provisioning profiles when configuring TestFlight integration.
    // Development build only for this spike.

    [MenuItem("Build/Build iOS")]
    public static void BuildiOS()
    {
        PerformBuild(BuildTarget.iOS, "Build/iOS");
    }

    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        PerformBuild(BuildTarget.Android, "Build/Android");
    }

    private static void PerformBuild(BuildTarget target, string outputPath)
    {
        // Create output directory if it doesn't exist
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        // Get all scenes in the build settings
        string[] scenes = GetScenesFromBuildSettings();

        if (scenes.Length == 0)
        {
            Debug.LogError("No scenes found in Build Settings. Please add scenes to Build Settings before building.");
            EditorApplication.Exit(1);
            return;
        }

        // Build options
        BuildOptions buildOptions = BuildOptions.Development;

        // Prepare build report
        BuildReport report = BuildPipeline.BuildPlayer(
            scenes,
            GetBuildOutputPath(target, outputPath),
            target,
            buildOptions
        );

        // Check if build succeeded
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded: {target}");
            Debug.Log($"Build output location: {GetBuildOutputPath(target, outputPath)}");
        }
        else
        {
            Debug.LogError($"Build failed: {target}");
            foreach (var step in report.steps)
            {
                foreach (var message in step.messages)
                {
                    if (message.type == LogType.Error)
                    {
                        Debug.LogError(message.content);
                    }
                }
            }
            EditorApplication.Exit(1);
        }
    }

    private static string GetBuildOutputPath(BuildTarget target, string basePath)
    {
        switch (target)
        {
            case BuildTarget.iOS:
                return Path.Combine(basePath, "iOS");
            case BuildTarget.Android:
                return Path.Combine(basePath, "app.aab"); // Android App Bundle
            default:
                return Path.Combine(basePath, target.ToString());
        }
    }

    private static string[] GetScenesFromBuildSettings()
    {
        return EditorBuildSettings.scenes.Length > 0
            ? EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes)
            : new string[] { };
    }
}
