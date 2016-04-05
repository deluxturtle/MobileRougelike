using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBooper : MonoBehaviour {


    public AudioClip boop;
    AudioSource source;

    void Start()
    {
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        source.clip = boop;

        foreach(Button button in GameObject.FindObjectsOfType<Button>())
        {
            button.onClick.AddListener(PlayBoop);
        }
    }

    void PlayBoop()
    {
        source.Play();
    }
}
