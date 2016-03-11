using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Generic Tile Property. Holds light up data.
/// </summary>
public class Tile : MonoBehaviour {

    public LayerMask mask = 1;
    public int xIndex;
    public int yIndex;
    public bool blocksLight = false;
    public bool wall = false;
    public bool ceiling = false;
    public int tempRange = 0;
    public List<GameObject> neighbors = new List<GameObject>();
    public List<GameObject> northTiles = new List<GameObject>();
    public List<GameObject> southTiles = new List<GameObject>();
    public List<GameObject> eastTiles = new List<GameObject>();
    public List<GameObject> westTiles = new List<GameObject>();
    public List<GameObject> topTiles = new List<GameObject>();
    bool lit = false;
    
    LoadTiles mapProperties;
    
    

    //Generates the neighbors list
    public void MakeNeighbors()
    {
        mapProperties = GameObject.Find("LevelLoad").GetComponent<LoadTiles>();
        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (tile.GetComponent<Tile>().xIndex == (xIndex - 1) &&
                tile.GetComponent<Tile>().yIndex == yIndex)
            {
                if (tile != gameObject && !neighbors.Contains(tile))
                {
                    eastTiles.Add(tile);
                    neighbors.Add(tile);
                }
            }
            else if (tile.GetComponent<Tile>().xIndex == (xIndex + 1) &&
                tile.GetComponent<Tile>().yIndex == yIndex)
            {
                if (tile != gameObject && !neighbors.Contains(tile))
                {
                    westTiles.Add(tile);
                    neighbors.Add(tile);
                }
            }

            else if (tile.GetComponent<Tile>().yIndex == (yIndex + 1) &&
                tile.GetComponent<Tile>().xIndex == xIndex)
            {
                if (tile != gameObject && !neighbors.Contains(tile))
                {
                    northTiles.Add(tile);
                    neighbors.Add(tile);
                }
            }
            else if (tile.GetComponent<Tile>().yIndex == (yIndex - 1) &&
                tile.GetComponent<Tile>().xIndex == xIndex)
            {

                if(tile != gameObject && !neighbors.Contains(tile))
                {
                    southTiles.Add(tile);
                    neighbors.Add(tile);
                }
            }
            else if(tile.GetComponent<Tile>().yIndex == yIndex &&
                tile.GetComponent<Tile>().xIndex == xIndex)
            {
                //then its on top of this.
                topTiles.Add(tile);
            }

        }
    }

    public void LightTile()
    {
        if (!lit)
        {
            lit = true;
            GetComponent<SpriteRenderer>().enabled = true;

            //Light path for walls
            foreach(GameObject wall in northTiles)
            {
                if (wall.GetComponent<Tile>().wall)
                {
                    wall.GetComponent<Tile>().LightTile();
                    foreach (GameObject ceiling in wall.GetComponent<Tile>().northTiles)
                    {
                        if (ceiling.GetComponent<Tile>().ceiling && ceiling.GetComponent<Tile>().tempRange <= 2)
                            ceiling.GetComponent<Tile>().LightTile();
                    }

                }
            }

            //Light path for ceilings
            foreach(GameObject tile in westTiles)
            {
                if (tile.GetComponent<Tile>().ceiling && !ceiling)
                {
                    tile.GetComponent<Tile>().lit = true;
                    tile.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            foreach (GameObject tile in eastTiles)
            {
                if (tile.GetComponent<Tile>().ceiling && !ceiling)
                {
                    tile.GetComponent<Tile>().lit = true;
                    tile.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

    }
}
