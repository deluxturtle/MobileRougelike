using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Interactable entity "Mushroom" drops a health buff if you 
/// run into it.
/// </summary>
public class EntityMushroom : MonoBehaviour {

    void Start()
    {
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        gameObject.isStatic = true;
        //Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D>();
        //rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
    }

    //void OnCollisionEnter2D(Collision other)
    //{
    //    if (other.gameObject != null)
    //        Destroy(gameObject);
    //}
}
