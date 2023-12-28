using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string userName; // Changed 'GameObject' to 'string'
    public string highScore = "100";

    public User(string userName, string highScore) // Changed 'GameObject' to 'string'
    {
        this.userName = userName;
        this.highScore = highScore;
    }
}
