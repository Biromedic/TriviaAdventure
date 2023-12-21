using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource music; // Assign this in the Inspector

    private bool isMusicPlaying;
    public GameObject MusicButtonSlash;

    void Start()
    {
        isMusicPlaying = true;
    }

    public void MusicCloser()
    {
        if (isMusicPlaying == false)
        {
            music.Play();
            //hide the MusicButtonSlash object  
            //MusicButtonSlash.gameObject.SetActive(false);     
            isMusicPlaying = true;
        }
        else
        {
            music.Stop();
            //show the MusicButtonSlash object
            //MusicButtonSlash.gameObject.SetActive(false);
            isMusicPlaying = false;
        }
    }
}

