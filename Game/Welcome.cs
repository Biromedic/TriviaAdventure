using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Start butonuna bas�ld���nda yeni bir sahneye ge�i� yap.
        SceneManager.LoadScene("Level1");
    }
}