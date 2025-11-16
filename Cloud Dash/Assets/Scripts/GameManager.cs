using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject gameOverPanel;
    
    [Header("Death Settings")]
    [Tooltip("How far below the camera should the player fall")]
    public float deathDistanceBelowCamera = 3f;
    [Tooltip("Time for restart (seconds)")]
    public float restartDelay = 0.5f;
    
    [Header("Scene Settings")]
    [Tooltip("Name scene restart")]
    public string gameSceneName = "Game";
    
    [Header("Audio (Optional)")]
    public AudioClip deathSound;
    
    [Header("Debug")]
    public bool showDebugInfo = false;
    
    private bool isDead = false;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        
        if (player == null)
        {
            Debug.LogError("Player is not assigned in GameManager!");
        }
    }

    void Update()
    {
        if (!isDead)
        {
            CheckPlayerDeath();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (showDebugInfo)
                Debug.Log("Manual restart by R key");
            RestartGame();
        }
    }

    void CheckPlayerDeath()
    {
        if (player == null || Camera.main == null) return;

        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float deathLine = cameraBottom - deathDistanceBelowCamera;
        
        if (player.position.y < deathLine)
        {
            OnPlayerDeath();
        }
    }

    void OnPlayerDeath()
    {
        isDead = true;
        
        if (showDebugInfo)
            Debug.Log($"GAME OVER! Player Y: {player.position.y:F2}");
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
        
        SaveHighScore();
        
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        if (showDebugInfo)
            Debug.Log($"Restarting in {restartDelay} seconds...");
        
        yield return new WaitForSeconds(restartDelay);
        
        RestartGame();
    }

    void SaveHighScore()
    {
        if (scoreManager == null) return;
        
        int currentScore = scoreManager.GetScore();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
            
            if (showDebugInfo)
                Debug.Log($"New High Score: {currentScore}!");
        }
    }

    public void RestartGame()
    {
        if (showDebugInfo)
            Debug.Log($"Loading scene: {gameSceneName}");
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void GameOver()
    {
        if (!isDead)
        {
            OnPlayerDeath();
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || Camera.main == null) return;
        
        Gizmos.color = Color.red;
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float deathLine = cameraBottom - deathDistanceBelowCamera;
        
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        
        Gizmos.DrawLine(new Vector3(screenLeft, deathLine, 0), new Vector3(screenRight, deathLine, 0));
        
        #if UNITY_EDITOR
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.Label(new Vector3(0, deathLine - 0.5f, 0), $"DEATH LINE (Y={deathLine:F1})");
        #endif
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        
        if (showDebugInfo)
            Debug.Log("High Score reset!");
    }
}