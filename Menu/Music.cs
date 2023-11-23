using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource music; // Assign this in the Inspector

    private bool isMusicPlaying = false;

    public void MusicCloser()
    {
        isMusicPlaying = !isMusicPlaying;

        if (isMusicPlaying)
        {
            music.Play();
        }
        else
        {
            music.Pause();
        }
    }
}
