using UnityEngine;

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

        //if (created && GameObject.Find("MusicManager"))
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        Debug.Log("Start");
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
}
