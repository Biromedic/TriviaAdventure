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
            // Hide the MusicButtonSlash object
            MusicButtonSlash.SetActive(false);
            isMusicPlaying = true;
        }
        else
        {
            music.Stop();
            // Show the MusicButtonSlash object
            MusicButtonSlash.SetActive(true); // To make it visible
            isMusicPlaying = false;
        }
    }
}
