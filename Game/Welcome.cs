using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Start butonuna basýldýðýnda yeni bir sahneye geçiþ yap.
        SceneManager.LoadScene("Level1");
    }
}