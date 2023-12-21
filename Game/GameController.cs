using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomQuestionsController : MonoBehaviour
{
    private int currentScore = 0;
    private int wantedScore = 100; // Set your desired wanted score

    // Other game-related variables and methods...

    // Call this method when checking if the player lost
    private void CheckIfPlayerLost()
    {
        if (currentScore < wantedScore)
        {
            // Player failed to reach the wanted score, transition to Level1 scene
            SaveScore(); // Save the current score
            LoadLevel1Scene();
        }
        /*else
        {
            // Continue the game
            // ... (other logic)
        }*/
    }

    // Call this method when the player answers a question correctly
    public void AnswerCorrectly(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        // Check if the player has reached the wanted score
        CheckIfPlayerLost();
    }

    // Save the current score (you might want to use PlayerPrefs, a GameManager, or other methods)
    private void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();
    }

    // Load the "Level1" scene
    private void LoadLevel1Scene()
    {
        SceneManager.LoadScene("Level1");
    }
}
