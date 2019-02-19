using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int totalScore;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = string.Format("Score: {0}", totalScore);
    }
}
