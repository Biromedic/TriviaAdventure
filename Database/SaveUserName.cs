using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveUserName : MonoBehaviour
{
    public GameObject userName;
    public void SaveName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        /*if(name == "")
        {
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        else
        {*/
            //After 1 second, load the main scene
            Invoke("LoadMainScene", 1f);
        //}
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
