using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    
    public bool closed = true;
    public GameObject topPart;

    public void OpenDoor()
    {
        GetComponent<Animator>().SetBool("open", true);
        closed = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<Tile>().blocksLight = false;
        GetComponent<Tile>().wall = false;
        
        if(topPart != null)
        {
            topPart.GetComponent<Tile>().blocksLight = false;
            topPart.GetComponent<Tile>().wall = false;
        }
    }
}
