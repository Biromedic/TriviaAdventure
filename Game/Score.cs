using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int currentScore;

    // The key to uniquely identify the score in PlayerPrefs
    private const string ScoreKey = "PlayerScore";

    void Start()
    {
        // Load the player's score from PlayerPrefs
        LoadScore();
        UpdateScoreText();
    }

    private void OnEnable()
    {
        // Subscribe to the event for adding scores
        GameEvent.AddScores += AddScores;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event when the script is disabled or destroyed
        GameEvent.AddScores -= AddScores;
    }

    // Method to add scores
    public void AddScores(int scores)
    {
        currentScore += scores;
        UpdateScoreText();

        // Save the updated score to PlayerPrefs
        SaveScore();

        // Check if the player has reached 100 points
        /*if (currentScore >= 100)
        {
            Congratulations(); // Call the Congratulations method
        }*/
    }

    // Method to update the score text on the UI
    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    // Method to save the player's score to PlayerPrefs
    private void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, currentScore);
        PlayerPrefs.Save(); // This line is optional, as PlayerPrefs auto-saves on application quit
    }

    // Method to load the player's score from PlayerPrefs
    private void LoadScore()
    {
        // If PlayerPrefs contains a score key, load the score
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            currentScore = PlayerPrefs.GetInt(ScoreKey);
            UpdateScoreText();
        }
    }

    // Method to continue with the last score
    public void ContinueWithLastScore()
    {
        SceneManager.LoadScene("RandomQuestions"); // Load the next scene
    }

    // Method to reset the score to zero
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
        SaveScore();
        SceneManager.LoadScene("RandomQuestions"); // Load the next scene
    }

    // Method to handle the Congratulations scene
    private void Congratulations()
    {
        SceneManager.LoadScene("Congratulations"); // Load the Congratulations scene
    }
}