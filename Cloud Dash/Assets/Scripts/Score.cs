using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text scoreText;
    
    [Header("Settings")]
    public Transform player;
    public float scoreInterval = 1f;
    
    private int score = 0;
    private float nextScoreHeight;
    private float startHeight;

    void Start()
    {
        startHeight = player.position.y;
        score = 0;
        nextScoreHeight = startHeight + scoreInterval;
        UpdateScore();
    }

    void Update()
    {
        if (player.position.y >= nextScoreHeight)
        {
            score++;
            nextScoreHeight += scoreInterval;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}