using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Generic Tile Property. Holds light up data.
/// </summary>
public class Tile : MonoBehaviour {
    
    public int xIndex;
    public int yIndex;
    public bool blocksLight = false;
    public bool wall = false;
    public bool ceiling = false;
    public bool topDoor = false;
    public int tempRange = 0;
    public List<GameObject> neighbors = new List<GameObject>();
    public List<GameObject> northTiles = new List<GameObject>();
    public List<GameObject> southTiles = new List<GameObject>();
    public List<GameObject> eastTiles = new List<GameObject>();
    public List<GameObject> westTiles = new List<GameObject>();
    public List<GameObject> topTiles = new List<GameObject>();
    bool lit = false;
    
    
    

    //Generates the neighbors list
    public void MakeNeighbors()
    {
        //MakeNewLists();
        neighbors = new List<GameObject>();
        northTiles = new List<GameObject>();
        southTiles = new List<GameObject>();
        eastTiles = new List<GameObject>();
        westTiles = new List<GameObject>();
        topTiles = new List<GameObject>();

        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            if (tile.xIndex == (xIndex - 1) &&
                tile.yIndex == yIndex)
            {
                if (tile.gameObject != gameObject && !neighbors.Contains(tile.gameObject))
                {
                    eastTiles.Add(tile.gameObject);
                    neighbors.Add(tile.gameObject);
                }
            }
            else if (tile.xIndex == (xIndex + 1) &&
                tile.yIndex == yIndex)
            {
                if (tile.gameObject != gameObject && !neighbors.Contains(tile.gameObject))
                {
                    westTiles.Add(tile.gameObject);
                    neighbors.Add(tile.gameObject);
                }
            }

            else if (tile.yIndex == (yIndex + 1) &&
                tile.xIndex == xIndex)
            {
                if (tile.gameObject != gameObject && !neighbors.Contains(tile.gameObject))
                {
                    northTiles.Add(tile.gameObject);
                    neighbors.Add(tile.gameObject);
                }
            }
            else if (tile.yIndex == (yIndex - 1) &&
                tile.xIndex == xIndex)
            {

                if(tile.gameObject != gameObject && !neighbors.Contains(tile.gameObject))
                {
                    southTiles.Add(tile.gameObject);
                    neighbors.Add(tile.gameObject);
                }
            }
            else if(tile.yIndex == yIndex &&
                tile.xIndex == xIndex)
            {
                //then its on top of this.
                if (tile.gameObject != gameObject && !neighbors.Contains(tile.gameObject))
                    topTiles.Add(tile.gameObject);
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

            foreach(GameObject tile in southTiles)
            {
                if (tile.GetComponent<Tile>().ceiling && tile.GetComponent<Tile>().tempRange >= 1)
                {
                    tile.GetComponent<Tile>().LightTile();
                }
            }

            foreach(GameObject tile in topTiles)
            {
                tile.GetComponent<Tile>().LightTile();
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

    /// <summary>
    /// Initializes all the lists.
    /// </summary>
    void MakeNewLists()
    {
        neighbors.Clear();
        
        northTiles.Clear();
        southTiles.Clear();
        eastTiles.Clear();
        westTiles.Clear();
        topTiles.Clear();

    }  
}
