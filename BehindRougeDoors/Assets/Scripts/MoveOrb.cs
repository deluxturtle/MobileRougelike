using UnityEngine;
using System.Collections;

public class MoveOrb : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Obstacles")
        //{
        //    Destroy(gameObject);
        //}
        if(other.name == "Ceiling")
        {
            Destroy(gameObject);
        }
    }
}
