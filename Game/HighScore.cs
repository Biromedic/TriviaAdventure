using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highScoreText;
    public int highScore;

    // The key to uniquely identify the high score in PlayerPrefs
    private const string HighScoreKey = "HighScore";

    void Start()
    {
        // Load the player's high score from PlayerPrefs
        LoadHighScore();
        UpdateHighScoreText();
    }

    // Method to update the high score text on the UI
    private void UpdateHighScoreText()
    {
        highScoreText.text = highScore.ToString();
    }

    // Method to save the player's high score to PlayerPrefs
    public void SaveHighScore(int newHighScore)
    {
        highScore = newHighScore;
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save(); // This line is optional, as PlayerPrefs auto-saves on application quit
        UpdateHighScoreText();
    }

    // Method to load the player's high score from PlayerPrefs
    public void LoadHighScore()
    {
        // If PlayerPrefs contains a high score key, load the high score
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            highScore = PlayerPrefs.GetInt(HighScoreKey);
            UpdateHighScoreText();
        }
    }
}
