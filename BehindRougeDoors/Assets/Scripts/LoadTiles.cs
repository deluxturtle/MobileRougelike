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
    public TextAsset[] mapInformation;
    //private variables
    GameObject tileParent;
    private Sprite[] sprites;

    public int layerWidth;
    public int layerHeight;


    //BackgroundLyaer
    GameObject[] tileIndex;

    //Sprite Index for interactable entities.
    //const int MUSHROOM = 89;
    //const int POT = 450;


    void Start()
    {
        tileParent = new GameObject();
        LoadMap(UnityEngine.Random.Range(0, mapInformation.Length));

    }

	public void LoadMap (int mapNum)
    {
        //Load the tileset into the sprites array
        sprites = Resources.LoadAll<Sprite>("dungeon_tileset_calciumtrice");
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

            Debug.Log(layerInfo.Attributes["name"].Value);

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
                    //create a sprite
                    //GameObject tempSprite = new GameObject("Background");
                    GameObject tempSprite = new GameObject(layerInfo.Attributes["name"].Value + " <" + horizontalIndex + ", " + verticalIndex + ">");
                    //Add the tile script to it
                    Tile tempTile = tempSprite.AddComponent<Tile>();
                    tempTile.xIndex = horizontalIndex;
                    tempTile.yIndex = verticalIndex;

                    //Make a sprite renderer.
                    SpriteRenderer spriteRenderer = tempSprite.AddComponent<SpriteRenderer>();
                    //Get sprite from sheet.
                    spriteRenderer.sprite = sprites[spriteValue - 1];
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
                        }
                        if(spriteValue >= 121 && spriteValue <= 158)
                        {
                            tempSprite.name = "Wall";
                            tempTile.wall = true;
                        }
                        //121 158
                    }

                    if(layerInfo.Attributes["name"].Value == "Interactive")
                    {
                        switch (spriteValue)
                        {
                            //case MUSHROOM:
                            //    tempSprite.AddComponent<EntityMushroom>();
                            //    break;
                            //case POT:
                            //    tempSprite.AddComponent<EntityPot>();
                            //    break;
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
