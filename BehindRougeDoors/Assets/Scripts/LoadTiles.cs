using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System;

/// <summary>
/// @Author: Andrew Seba
/// @Description: 
/// </summary>
public class LoadTiles : MonoBehaviour {

    //holds the .xml file.
    [Header("Map Info")]
    public TextAsset[] mapInformation;
    public int layerWidth;
    public int layerHeight;
    [Header("Interactive Props")]
    public GameObject doorObject;

    [Header("Enemies")]
    public GameObject skeleton;
    
    [Header("Other")]
    public GameObject player;


    //private variables
    GameObject tileParent;
    private Sprite[] spritesLevelOne;
    private Sprite[] spritesLevelTwo;

    //BackgroundLyaer
    GameObject[] tileIndex;

    //Sprite Index for interactable entities.
    //Closed Door
    const int CLOSEDDOORONETOP = 331;
    const int CLOSEDDOORONEBOTTOM = 361;
    //Open Door
    const int OPENDOORONETOP = 338;
    const int OPENDOORONEMIDDLE = 368;
    const int OPENDOORONEBOTTOM = 397;
    //Spawn Point Guy
    const int SPAWNDUDETOP = 781;
    const int SPAWNDUDEBOTTOM = 811;
    //Skeletons
    const int SKELETONTOP = 883;
    const int SKELETONBOTTOM = 913;


    void Awake()
    {
        tileParent = new GameObject();
        LoadMap(UnityEngine.Random.Range(0, mapInformation.Length));

    }

	public void LoadMap (int mapNum)
    {
        //Load the tileset into the sprites array
        spritesLevelOne = Resources.LoadAll<Sprite>("dungeon_tileset_calciumtrice");
        spritesLevelTwo = Resources.LoadAll<Sprite>("snow-expansion");

        XmlDocument xmlDoc = new XmlDocument();

        switch (mapNum)
        {
            case 0:
                xmlDoc.LoadXml(mapInformation[0].text);
                break;
            case 1:
                xmlDoc.LoadXml(mapInformation[1].text);
                break;
            case 2:
                xmlDoc.LoadXml(mapInformation[2].text);
                break;
            default:
                Debug.Log("Map number not found.");
                break;
        }

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");


        //foreach(XmlNode node in layerNames)
        //{
        //    //Debug.Log(node.Attributes[0].InnerText);
        //    //Debug.Log(node.Attributes.GetNamedItem("name").InnerText);
        //    Debug.Log(node.Attributes["name"].InnerText);
        //}

        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");
        float tileWidth = float.Parse(tilesetInfo.Attributes["tilewidth"].Value) / (float)16;
        float tileHeight = float.Parse(tilesetInfo.Attributes["tileheight"].Value) / (float)16;
        
        //For each layer that exists
        foreach(XmlNode layerInfo in layerNames)
        {
            layerWidth = int.Parse(layerInfo.Attributes["width"].Value);
            layerHeight = int.Parse(layerInfo.Attributes["height"].Value);

            //Debug.Log(layerInfo.Attributes["name"].Value);

            ////In the background layer in the xml
            //if(layerInfo.Attributes["name"].Value == "Background")
            //{
                //Pull out of the data node
            XmlNode tempNode = layerInfo.SelectSingleNode("data");

            int verticalIndex = layerHeight - 1;
            int horizontalIndex = 0;

            //for each tile inside the map
            foreach(XmlNode tile in tempNode.SelectNodes("tile"))
            {
                int spriteValue = int.Parse(tile.Attributes["gid"].Value);


                //if sprite is not empty
                if(spriteValue > 0)
                {
                    Sprite[] currentSpriteSheet = spritesLevelOne;
                    if (spriteValue > 1050)
                    {
                        currentSpriteSheet = spritesLevelTwo;
                        spriteValue -= 1050;
                    }
                    else
                    {
                        currentSpriteSheet = spritesLevelOne;
                    }
                    //create a sprite
                    GameObject tempSprite = new GameObject(layerInfo.Attributes["name"].Value + " <" + horizontalIndex + ", " + verticalIndex + ">");
                    //Add the tile script to it
                    Tile tempTile = tempSprite.AddComponent<Tile>();
                    tempTile.xIndex = horizontalIndex;
                    tempTile.yIndex = verticalIndex;

                    //Make a sprite renderer.
                    SpriteRenderer spriteRenderer = tempSprite.AddComponent<SpriteRenderer>();
                    //Get sprite from sheet.
                    //Debug.Log(spriteValue);
                    spriteRenderer.sprite = currentSpriteSheet[spriteValue - 1];
                    //Set position
                    tempSprite.transform.position = new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex));
                    //Set sorting layer.
                    spriteRenderer.sortingLayerName = layerInfo.Attributes["name"].Value;

                    //Set parent
                    tempSprite.transform.parent = GameObject.Find(layerInfo.Attributes["name"].Value + "Layer").transform;
                    tempSprite.tag = "Tile";
                    //Instantiate(tempCube, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);

                    //Add rigidbody and box collider to the walls.
                    if (layerInfo.Attributes["name"].Value == "Obstacles")
                    {
                        tempSprite.AddComponent<BoxCollider2D>();
                        tempSprite.AddComponent<Rigidbody2D>();
                        tempSprite.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                        if(spriteValue >= 31 && spriteValue <= 86)
                        {
                            tempSprite.name = "Ceiling";
                            tempTile.blocksLight = true;
                            tempTile.ceiling = true;
                            tempTile.mask = 1 << 8;
                        }
                        if(spriteValue >= 121 && spriteValue <= 158)
                        {
                            tempSprite.name = "Wall";
                            tempTile.wall = true;
                            tempTile.blocksLight = true;
                            tempTile.mask = 1 << 8;
                        }
                        //121 158
                    }

                    if(layerInfo.Attributes["name"].Value == "Interactive")
                    {
                        switch (spriteValue)
                        {
                            case CLOSEDDOORONETOP:
                                tempSprite.GetComponent<Tile>().topDoor = true;
                                //Disable the door sprite cuz we are just going to spawn a door object on it.
                                //tempSprite.SetActive(false);
                                //tempSprite.GetComponent<SpriteRenderer>().sprite = null;
                                //tempSprite.GetComponent<SpriteRenderer>().enabled = false;
                                //tempSprite.GetComponent<Tile>().blocksLight = true;
                                //tempSprite.GetComponent<Tile>().wall = true;
                                break;
                            case CLOSEDDOORONEBOTTOM:
                                tempSprite.name = "Door";
                                GameObject door = (GameObject)Instantiate(doorObject, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);
                                door.GetComponent<Tile>().xIndex = horizontalIndex;
                                door.GetComponent<Tile>().yIndex = verticalIndex;
                                door.GetComponent<Tile>().blocksLight = true;
                                door.GetComponent<Tile>().wall = true;
                                //GameObject doorTop = new GameObject();
                                //doorTop.transform.position = new Vector2((tileWidth * horizontalIndex), (tileHeight * verticalIndex + 1));
                                //Tile doorTopTile = doorTop.AddComponent<Tile>();
                                //doorTopTile.xIndex = horizontalIndex;
                                //doorTopTile.yIndex = verticalIndex + 1;
                                //doorTopTile.blocksLight = true;
                                //doorTopTile.wall = true;

                                //door.GetComponent<Door>().topPart = doorTop;
                                //Actually disable the door sprite
                                tempSprite.SetActive(false);
                                break;
                            case SPAWNDUDETOP:
                                tempSprite.SetActive(false);
                                break;
                            case SPAWNDUDEBOTTOM:
                                player.transform.position = tempSprite.transform.position;
                                tempSprite.SetActive(false);
                                break;
                            case SKELETONTOP:
                                tempSprite.SetActive(false);
                                break;
                            case SKELETONBOTTOM:
                                tempSprite.SetActive(false);
                                GameObject tempSkeleton = (GameObject)Instantiate(skeleton, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);
                                tempSkeleton.transform.position = tempSprite.transform.position;
                                tempSkeleton.GetComponentInChildren<ScriptSkeleton>().xIndex = horizontalIndex;
                                tempSkeleton.GetComponentInChildren<ScriptSkeleton>().yIndex = verticalIndex;
                                break;
                        }
                    }

                    //Testing Disableing the spriteRenderer to make it black
                    spriteRenderer.enabled = false;

                }

                horizontalIndex++;
                if(horizontalIndex % layerWidth == 0)
                {
                    //Increase our virtical location
                    verticalIndex--;
                    //reset our horizontal location.
                    horizontalIndex = 0;
                }
            }
        }

        //Generate Tile neighbors
        foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            tile.GetComponent<Tile>().MakeNeighbors();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        #region CheatCodes
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ClearLevel();
            LoadMap(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ClearLevel();
            LoadMap(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ClearLevel();
            LoadMap(2);
        }
        Time.timeScale = 1;

        #endregion
    }

    void ClearLevel()
    {
        foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            Destroy(tile);
        }
    }
}
