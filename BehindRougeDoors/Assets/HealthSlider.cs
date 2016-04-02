using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSlider : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            GetComponent<Slider>().value = player.GetComponentInChildren<Health>().health;
        }
    }
    public void UpdateHealth()
    {
        GetComponent<Slider>().value = player.GetComponent<Health>().health;
    }
}
