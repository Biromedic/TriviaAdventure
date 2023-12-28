using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    public InputField userName;
    public Text highScore; // Changed 'InputField' to 'Text'
    public HighScoreManager highScoreManager;

    //public Text userNameText;
    //public Text highScoreText;


    private string userID;
    private DatabaseReference dbreference;

    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbreference = FirebaseDatabase.DefaultInstance.RootReference;
        highScoreManager.LoadHighScore();

    }

    public void CreateUser()
{
    GameObject userObject = new GameObject("User");
    User newUser = userObject.AddComponent<User>();
    newUser.userName = userName.text;
    newUser.highScore = highScore.text;

    string json = JsonUtility.ToJson(newUser);
    dbreference.Child("users").Child(userID).SetRawJsonValueAsync(json);
}

    /*public IEnumerator GetName(Action<string> onCallBack)
    {
        var DBTask = dbreference.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No data exists");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            string json = snapshot.GetRawJsonValue();

            User loadedUser = JsonUtility.FromJson<User>(json);
            onCallBack(loadedUser.userName);
        }
    }

    public IEnumerator GetHighScore(Action<string> onCallBack)
    {
        var DBTask = dbreference.Child("users").Child(userID).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("No data exists");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            string json = snapshot.GetRawJsonValue();

            User loadedUser = JsonUtility.FromJson<User>(json);
            onCallBack(loadedUser.highScore);
        }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name) => {
            userNameText.text = name; // Changed 'userName' to 'userNameText'
        }));

        StartCoroutine(GetHighScore((string score) => {
            highScoreText.text = score; // Changed 'highScore' to 'highScoreText'
        }));
    }*/

}

