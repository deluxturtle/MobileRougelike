using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    
    public bool closed = true;

    public void OpenDoor()
    {
        GetComponent<Animator>().SetBool("open", true);
        closed = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        
    }
}
