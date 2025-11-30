using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "MyProject/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Camera Settings")]
    public float cameraSmoothSpeed = 10f;

    [Header("Death Settings")]
    public float deathDistanceBelowCamera = 3f;
    public float restartDelay = 0.5f;
    public string gameSceneName = "Game";
    public AudioClip deathSound;
    public bool showDebugInfo = false;

    [Header("Platform Settings")]
    public int startPlatformCount = 15;
    public float minYDistance = 1.5f;
    public float maxYDistance = 2.5f;

    [Header("Player Settings")]
    public float moveSpeed = 5f;

    [Header("Platform Forces")]
    public float jumpForce = 15f;
    public float strongForce = 25f;
    public float weakForce = 5f;

    [Header("Score Settings")]
    public float scoreInterval = 1f;
}
