using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public GameSettings settings;

    [Header("References")]
    public Transform player;
    public GameObject gameOverPanel;

    private bool isDead = false;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        if (player == null)
            Debug.LogError("Player is not assigned in GameManager!");
    }

    void Update()
    {
        if (!isDead)
            CheckPlayerDeath();

        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }

    void CheckPlayerDeath()
    {
        if (player == null || Camera.main == null) return;

        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float deathLine = cameraBottom - settings.deathDistanceBelowCamera;

        if (player.position.y < deathLine)
            OnPlayerDeath();
    }

    void OnPlayerDeath()
    {
        if (isDead) return;
        isDead = true;

        if (settings.showDebugInfo)
            Debug.Log("GAME OVER! Player died at Y=" + player.position.y);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (settings.deathSound != null)
            AudioSource.PlayClipAtPoint(settings.deathSound, Camera.main.transform.position);

        SaveHighScore();
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(settings.restartDelay);
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

            if (settings.showDebugInfo)
                Debug.Log($"New High Score: {currentScore}");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(settings.gameSceneName);
    }

    public void GameOver()
    {
        if (!isDead)
            OnPlayerDeath();
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || Camera.main == null) return;

        Gizmos.color = Color.red;
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float deathLine = cameraBottom - settings.deathDistanceBelowCamera;
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

        if (settings.showDebugInfo)
            Debug.Log("High Score reset!");
    }
}
