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

        // Add your questions here
        questions = new Question[]
        {
            new Question("Tetris is the #1 best-selling video game of all-time.", 0),
            new Question("Linux was first created as an alternative to Windows XP.", 0),
            new Question("Russia passed a law in 2013 which outlaws telling children that homosexuals exist.", 1),
            new Question("In Terraria, you can craft the Cell Phone pre-hardmode.", 1),
            new Question("In the 1988 film \"Akira\", Tetsuo ends up destroying Tokyo.", 1),
            new Question("The 2016 United States Presidential Election is the first time Hillary Clinton has run for President.", 0),
            new Question("In Pokémon, Arbok evolves into Seviper.", 0),
            new Question("In Sonic the Hedgehog universe, Tails' real name is Miles Prower.", 1),
            new Question("Bulls are attracted to the color red.", 0),
            new Question("Donald Trump won the popular vote in the 2016 United States presidential election.", 0),
            new Question("In World War ll, Great Britian used inflatable tanks on the ports of Great Britain to divert Hitler away from Normandy/D-day landing.", 1),
            new Question("In \"Need for Speed: Porsche Unleashed\", the player can only drive cars manufactured by Porsche.", 1),
            new Question("\"Ananas\" is mostly used as the word for Pineapple in other languages.", 1),
            new Question("Time on Computers is measured via the EPOX System.", 0),
            new Question("Minecraft can be played with a virtual reality headset.", 1),
            new Question("The programming language \"Python\" is based off a modified version of \"JavaScript\".", 0),
            new Question("RAM stands for Random Access Memory.", 1),
            new Question("The shotgun appears in every numbered Resident Evil game.", 1),
            new Question("The song \"Megalovania\" by Toby Fox made its third appearence in the 2015 RPG \"Undertale\".", 1),
            new Question("Han Solo's co-pilot and best friend, \"Chewbacca\", is an Ewok.", 0),
            new Question("The Great Wall of China is visible from the moon.", 0),
            new Question("BMW M GmbH is a subsidiary of BMW AG that focuses on car performance.", 1),
            new Question("The names of Tom Nook's cousins in the Animal Crossing franchise are named \"Timmy\" and \"Jimmy\".", 0),
            new Question("Peter Molyneux was the founder of Bullfrog Productions.", 1),
            new Question("Deus Ex (2000) does not feature the World Trade Center because it was destroyed by terrorist attacks according to the game's plot.", 1),
            new Question("\"Resident Evil 7\" is the first first-person Resident Evil game.", 0),
            new Question("Despite being seperated into multiple countries, The Scandinavian countries are unified by all having the same monarch.", 0),
            new Question("Matt Damon played an astronaut stranded on an extraterrestrial planet in both of the movies Interstellar and The Martian.", 1),
            new Question("Rabbits are carnivores.", 0),
            new Question("The Sun rises from the North.", 0),
            new Question("In Chobits, Hideki found Chii in his apartment.", 0),
            new Question("In 1993, Prince changed his name to an unpronounceable symbol because he was unhappy with his contract with Warner Bros.", 1),
            new Question("Actor Tommy Chong served prison time.", 1),
            new Question("Scatman John's real name was John Paul Larkin.", 1),
            new Question("Vatican City is a country.", 1),
            new Question("In the \"To Love-Ru\" series, Golden Darkness is sent to kill Lala Deviluke.", 0),
            new Question("\"27 Club\" is a term used to refer to a list of famous actors, musicians, and artists who died at the age of 27.", 1),
            new Question("Former president Theodore Roosevelt (1900-1908)  ran for another term under the Progressive Party in 1912.", 1),
            new Question("John Williams composed the music for \"Star Wars\".", 1),
            new Question("Ewan McGregor did not know the name of the second prequel film of Star Wars during and after filming.", 1),
            new Question("Rabbits can see what's behind themselves without turning their heads.", 1),
            new Question("The music video to The Buggle's \"Video Killed the Radio Star\" was the first music video to broadcast on MTV.", 1),
            new Question("An equilateral triangle always has every angle measuring 60°.", 1),
            new Question("Adolf Hitler was a german soldier in World War I.", 1),
            new Question("Tupac Shakur died due to complications from being stabbed in 1996.", 0),
            new Question("An Astronomical Unit is the distance between Earth and the Moon.", 0),
            new Question("When you cry in space, your tears stick to your face.", 1),
            new Question("In RuneScape, one must complete the \"Dragon Slayer\" quest before equipping Rune Platelegs.", 0)
        };

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
        if (currentQuestion.IsTrue)
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
        if (!currentQuestion.IsTrue)
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
