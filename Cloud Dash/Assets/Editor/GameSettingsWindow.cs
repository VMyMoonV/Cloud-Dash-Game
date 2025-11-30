using UnityEditor;
using UnityEngine;

public class GameSettingsWindow : EditorWindow
{
    private GameSettings settings;
    private Vector2 scrollPos;

    [MenuItem("Tools/Game Settings")]
    public static void OpenWindow()
    {
        GetWindow<GameSettingsWindow>("Game Settings");
    }

    void OnEnable()
    {
        string[] guids = AssetDatabase.FindAssets("t:GameSettings");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            settings = AssetDatabase.LoadAssetAtPath<GameSettings>(path);
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Game Settings Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        settings = (GameSettings)EditorGUILayout.ObjectField("Settings Asset", settings, typeof(GameSettings), false);

        if (settings == null)
        {
            EditorGUILayout.HelpBox("No GameSettings asset found. Create one using Create → MyProject → Game Settings", MessageType.Warning);
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.Space();

        DrawCameraSettings();
        DrawDeathSettings();
        DrawPlatformSettings();
        DrawPlayerSettings();
        DrawJumpPlatformSettings();
        DrawStrongPlatformSettings();
        DrawWeakPlatformSettings();
        DrawScoreSettings();

        EditorGUILayout.EndScrollView();

        if (GUI.changed)
            EditorUtility.SetDirty(settings);
    }

    void DrawCameraSettings()
    {
        GUILayout.Label("Camera Settings", EditorStyles.boldLabel);
        settings.cameraSmoothSpeed = EditorGUILayout.FloatField("Camera Smooth Speed", settings.cameraSmoothSpeed);
        EditorGUILayout.Space();
    }

    void DrawDeathSettings()
    {
        GUILayout.Label("Death Settings", EditorStyles.boldLabel);
        settings.deathDistanceBelowCamera = EditorGUILayout.FloatField("Death Distance Below Camera", settings.deathDistanceBelowCamera);
        settings.restartDelay = EditorGUILayout.FloatField("Restart Delay", settings.restartDelay);
        settings.gameSceneName = EditorGUILayout.TextField("Scene Name", settings.gameSceneName);
        settings.deathSound = (AudioClip)EditorGUILayout.ObjectField("Death Sound", settings.deathSound, typeof(AudioClip), false);
        settings.showDebugInfo = EditorGUILayout.Toggle("Show Debug Info", settings.showDebugInfo);
        EditorGUILayout.Space();
    }

    void DrawPlatformSettings()
    {
        GUILayout.Label("Platform Settings", EditorStyles.boldLabel);
        settings.startPlatformCount = EditorGUILayout.IntField("Start Platform Count", settings.startPlatformCount);
        settings.minYDistance = EditorGUILayout.FloatField("Min Y Distance", settings.minYDistance);
        settings.maxYDistance = EditorGUILayout.FloatField("Max Y Distance", settings.maxYDistance);
        EditorGUILayout.Space();
    }

    void DrawPlayerSettings()
    {
        GUILayout.Label("Player Settings", EditorStyles.boldLabel);
        settings.moveSpeed = EditorGUILayout.FloatField("Move Speed", settings.moveSpeed);
        EditorGUILayout.Space();
    }

    void DrawJumpPlatformSettings()
    {
        GUILayout.Label("Standard Platform", EditorStyles.boldLabel);
        settings.jumpForce = EditorGUILayout.FloatField("Jump Force", settings.jumpForce);
        EditorGUILayout.Space();
    }

    void DrawStrongPlatformSettings()
    {
        GUILayout.Label("Strong Platform", EditorStyles.boldLabel);
        settings.strongForce = EditorGUILayout.FloatField("Up Force", settings.strongForce);
        EditorGUILayout.Space();
    }

    void DrawWeakPlatformSettings()
    {
        GUILayout.Label("Weak Platform", EditorStyles.boldLabel);
        settings.weakForce = EditorGUILayout.FloatField("Down Force", settings.weakForce);
        EditorGUILayout.Space();
    }

    void DrawScoreSettings()
    {
        GUILayout.Label("Score Settings", EditorStyles.boldLabel);
        settings.scoreInterval = EditorGUILayout.FloatField("Score Interval", settings.scoreInterval);
        EditorGUILayout.Space();
    }
}
