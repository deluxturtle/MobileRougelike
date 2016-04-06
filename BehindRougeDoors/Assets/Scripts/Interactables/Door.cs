using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    
    public bool closed = true;
    [HideInInspector]
    public GameObject topPart;

    void Start()
    {
        foreach(GameObject tile in GetComponent<Tile>().northTiles)
        {
            if (tile.GetComponent<Tile>().topDoor == true)
            {
                topPart = tile;
            }
        }
    }

    public void OpenDoor()
    {
        GetComponent<Tile>().LightTile();
        if (topPart != null)
        {
            topPart.GetComponent<Tile>().blocksLight = false;
            topPart.GetComponent<Tile>().wall = false;
            topPart.SetActive(false);
        }
        closed = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<Tile>().blocksLight = false;
        GetComponent<Tile>().wall = false;
        GetComponent<Animator>().SetBool("open", true);
        GetComponent<AudioSource>().Play();

    }
}
