using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    public static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    public Score scoreScript; // Reference to the Score script

    void Start()
    {
        // Make sure to assign the Score script in the Unity Editor
        if (scoreScript == null)
        {
            Debug.LogError("Score script not found! Assign it in the Unity Editor.");
        }

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = new List<Question>(questions);
        }
        SetCurrentQuestion();
        Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;
    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
            // Add scores and check if the player reached 100 points
            //scoreScript.AddScores(10); // You can adjust the score value as needed
            // go to Level1 scene
            SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.Log("WRONG!");
            // Reset the user's score to zero
            scoreScript.ResetScore();
            // go to MainMenu scene
            SceneManager.LoadScene("Level1");
        }
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!");
            // Add scores and check if the player reached 100 points
            //scoreScript.AddScores(10); // You can adjust the score value as needed
            // go to Level1 scene
            SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.Log("WRONG!");
            // Reset the user's score to zero
            scoreScript.ResetScore();
            // go to MainMenu scene
            SceneManager.LoadScene("Level1");
        }
    }
}
