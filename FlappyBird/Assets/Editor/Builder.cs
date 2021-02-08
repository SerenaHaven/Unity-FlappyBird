﻿#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public partial class Builder
{
    public static readonly string BuildRoot = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "Build");
    public static readonly string[] Scenes = new string[] { "Assets/_Scene/Main.unity" };

    private static void Log(string message)
    {
        Debug.Log("========================================\n" + message + "\n========================================");
    }

    private static bool Build(
        BuildTarget buildTarget,
        string path,
        BuildOptions options = BuildOptions.None)
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.target = buildTarget;
        buildPlayerOptions.scenes = Scenes;
        buildPlayerOptions.locationPathName = path;
        buildPlayerOptions.options = options;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            Log("Build succeeded: " + summary.totalSize + " bytes");
            return true;
        }
        else
        {
            // Debug.LogError("Build failed" + summary);
            Log("Build failed: " + summary);
            return false;
        }
    }

    [MenuItem("Tools/Build Standalone Windows 64")]
    private static bool BuildStandaloneWindows64()
    {
        Log("Build Standalone Windows 64");
        return Build(BuildTarget.StandaloneWindows64, Path.Combine(BuildRoot, "StandaloneWindows64", Application.productName + ".exe"));
    }

    [MenuItem("Tools/Build Android")]
    private static bool BuildAndroid()
    {
        Log("Build Android");
        return Build(BuildTarget.Android, Path.Combine(BuildRoot, "Android", Application.productName + ".apk"));
    }
}
#endif