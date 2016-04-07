using UnityEngine;
using System.Collections;

public class EnemyOrb : MonoBehaviour {

	void Start()
    {
        //Destroy self after 5 seconds.
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Ceiling")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            other.GetComponent<Health>().Damage(10);
        }
    }
}
