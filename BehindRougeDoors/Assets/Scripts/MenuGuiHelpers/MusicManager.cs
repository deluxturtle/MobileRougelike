using UnityEngine;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Singleton music manager.
/// </summary>
public class MusicManager : MonoBehaviour {

    static bool created = false;

    AudioSource source;
    public AudioClip mainMenuSong;
    public AudioClip caveSong;
    public AudioClip gameOver;


	// Use this for initialization
	void Start ()
    {

        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        source = GetComponent<AudioSource>();

        PlayMenuMusic();
	}

    public void UpdateVolume(float value)
    {
        source.volume = value;
    }
	
	public void PlayMenuMusic()
    {
        source.clip = mainMenuSong;
        source.Play();
    }

    public void PlayCaveMusic()
    {
        source.clip = caveSong;
        source.Play();
    }

    public void PlayGameOverMusic()
    {
        source.clip = gameOver;
        source.Play();
    }
}
