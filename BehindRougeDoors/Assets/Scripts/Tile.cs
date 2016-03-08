using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Generic Tile Property. Holds light up data.
/// </summary>
public class Tile : MonoBehaviour {

    public int xIndex;
    public int yIndex;

    bool lit = false;
    public List<GameObject> neighbors = new List<GameObject>();
    LoadTiles mapProperties;

    //Generates the neighbors list
    public void MakeNeighbors()
    {
        mapProperties = GameObject.Find("LevelLoad").GetComponent<LoadTiles>();
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (tile.GetComponent<Tile>().xIndex == xIndex - 1 ||
                tile.GetComponent<Tile>().xIndex == xIndex + 1 ||
                tile.GetComponent<Tile>().yIndex == yIndex - 1 ||
                tile.GetComponent<Tile>().yIndex == yIndex + 1)
            {

                if(tile != gameObject && !neighbors.Contains(tile))
                {
                    neighbors.Add(tile);
                }
            }

        }
    }

    public void LightTile()
    {
        if (!lit)
        {
            lit = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }

    }
}
