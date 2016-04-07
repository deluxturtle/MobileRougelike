using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Handels orb collisions.
/// </summary>
public class MoveOrb : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Ceiling")
        {
            Destroy(gameObject);
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().Damage(100);
            Destroy(gameObject);
        }
    }


}
