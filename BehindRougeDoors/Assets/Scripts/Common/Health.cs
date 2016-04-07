using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health = 100;
    public AudioClip hurtSound;
    public AudioClip dieSound;

    private Animator animator;
    AudioSource source;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (!GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();


        source = GetComponent<AudioSource>();

    }

    public void Damage(int amount)
    {
        health -= amount;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            animator.SetBool("dead", true);
            if(dieSound != null)
            {
                source.clip = dieSound;
                source.Play();
            }

            if(gameObject.tag == "Player")
            {
                Debug.Log("Game Over");


            }

            gameObject.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            //play hurt sound.
            if(hurtSound != null)
            {
                source.clip = hurtSound;
                source.Play();

            }
        }

        if (gameObject.tag == "Player")
        {
            GameObject.Find("SliderHealth").GetComponent<HealthSlider>().UpdateHealth();
        }
    }
}
