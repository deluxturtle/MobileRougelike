using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetVolumeValue : MonoBehaviour {

	// Use this for initialization
	void OnEnable ()
    {
        float volume = GameObject.Find("MusicManager").GetComponent<AudioSource>().volume;
        if(GetComponent<Slider>())
            GetComponent<Slider>().value = volume;
	}
}
