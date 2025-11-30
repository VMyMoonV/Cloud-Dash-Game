using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public GameSettings settings;
    public TMP_Text scoreText;
    public Transform player;

    private int score = 0;
    private float nextHeight;

    void Start()
    {
        nextHeight = player.position.y + settings.scoreInterval;
        UpdateScore();
    }

    void Update()
    {
        if (player.position.y >= nextHeight)
        {
            score++;
            nextHeight += settings.scoreInterval;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public int GetScore() => score;
}
