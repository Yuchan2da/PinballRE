using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public Pinball.GameState gameState;

    private void Start()
    {
        gameState.score.OnChange += UpdateScore;
        gameState.maxScore.OnChange += UpdateMaxScore;

        // 초기 표시
        UpdateScore(gameState.score.value);
        UpdateMaxScore(gameState.maxScore.value);
    }

    private void UpdateScore(int value)
    {
        scoreText.text = $"Score: {value}";
    }

    private void UpdateMaxScore(int value)
    {
        maxScoreText.text = $"Max Score: {value}";
    }
}
