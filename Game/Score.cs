using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int currentScores;

    void Start()
    {
        currentScores = 0;
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameEvent.AddScores += AddScores;
    }

    private void OnDisable()
    {
        GameEvent.AddScores -= AddScores;
    }

    private void AddScores(int scores)
    {
        currentScores += scores;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScores.ToString();
    }
}
