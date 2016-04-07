using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: 
/// </summary>
public class LoadTiles : MonoBehaviour {

    //holds the .xml file.
    [Header("Map Info")]
    public int levelNum;
    public TextAsset[] mapInformation;
    public int layerWidth;
    public int layerHeight;
    [Header("Interactive Props")]
    public GameObject doorObject;

    [Header("Enemies")]
    [Tooltip("Place the skeleton prefab here.")]
    public GameObject skeletonPrefab;
    [Tooltip("Plase the tower enemy prefab here.")]
    public GameObject towerPrefab;
    
    [Header("Other")]
    public GameObject player;

    [HideInInspector]
    public List<GameObject> skeletonSpawnPoints = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> powerupSpawnPoints = new List<GameObject>();


    //private variables
    private Sprite[] spritesLevelOne;
    private Sprite[] spritesLevelTwo;

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
    //Skeletons Type1 enemy
    const int SKELETONTOP = 883;
    const int SKELETONBOTTOM = 913;
    //statue type 3 enemy
    const int STATUETOP = 590;
    const int STATUEBOTTOM = 620;


    void Awake()
    {
        if (GameObject.Find("SaveInfo") && !GameObject.Find("SaveInfo").GetComponent<SaveInfo>().newGame)
        {
            SaveInfo saveInfo = GameObject.Find("SaveInfo").GetComponent<SaveInfo>();
            levelNum = saveInfo.savedLevelIndex -1;

            //If no level num is -1 do random.
            if (levelNum < 0)
            {
                levelNum = UnityEngine.Random.Range(0, mapInformation.Length);
                
            }
            else//If there is a saved level load in the health
            {
                player.GetComponent<Health>().health = saveInfo.savedHealth;
            }
        }
        else//If you cant find save info default to random.
        {
            levelNum = UnityEngine.Random.Range(0, mapInformation.Length);
        }
        StartCoroutine(LoadMap(levelNum));

    }

	IEnumerator LoadMap (int mapNum)
    {
        ClearLevel();
        //Tiles were still referencing old tiles memory.
        yield return new WaitForEndOfFrame();

        levelNum = mapNum;

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
                            //tempTile.mask = 1 << 8;
                        }
                        if(spriteValue >= 121 && spriteValue <= 158)
                        {
                            tempSprite.name = "Wall";
                            tempTile.wall = true;
                            tempTile.blocksLight = true;
                            //tempTile.mask = 1 << 8;
                        }
                        //121 158
                    }

                    if(layerInfo.Attributes["name"].Value == "Interactive")
                    {
                        switch (spriteValue)
                        {
                            //Door Spawning
                            case CLOSEDDOORONETOP:
                                tempSprite.GetComponent<Tile>().topDoor = true;
                                break;
                            case CLOSEDDOORONEBOTTOM:
                                tempSprite.name = "Door";
                                GameObject door = (GameObject)Instantiate(doorObject, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);
                                door.GetComponent<Tile>().xIndex = horizontalIndex;
                                door.GetComponent<Tile>().yIndex = verticalIndex;
                                door.GetComponent<Tile>().blocksLight = true;
                                door.GetComponent<Tile>().wall = true;
                                tempSprite.SetActive(false);
                                break;
                            //Player spawn point
                            case SPAWNDUDETOP:
                                tempSprite.SetActive(false);
                                break;
                            case SPAWNDUDEBOTTOM:
                                player.transform.position = tempSprite.transform.position;
                                tempSprite.SetActive(false);
                                break;
                            //Skeleton Spawns
                            case SKELETONTOP:
                                tempSprite.SetActive(false);
                                break;
                            case SKELETONBOTTOM:
                                tempSprite.SetActive(false);
                                //tempSprite.GetComponent<SpriteRenderer>().enabled = false;
                                //GameObject tempSkeleton = (GameObject)Instantiate(skeletonPrefab, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);
                                //tempSkeleton.transform.position = tempSprite.transform.position;
                                //tempSkeleton.GetComponentInChildren<Tile>().xIndex = horizontalIndex;
                                //tempSkeleton.GetComponentInChildren<Tile>().yIndex = verticalIndex;
                                skeletonSpawnPoints.Add(tempSprite);
                                break;
                            case STATUETOP:
                                tempSprite.SetActive(false);
                                GameObject tempTower = (GameObject)Instantiate(towerPrefab, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);
                                tempTower.GetComponent<Tile>().xIndex = horizontalIndex;
                                tempTower.GetComponent<Tile>().yIndex = verticalIndex;
                                break;
                            case STATUEBOTTOM:
                                tempSprite.SetActive(false);
                                //add statue prefab.
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


        DoTheRandomSpawning();

        //Generate Tile neighbors
        foreach (Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            //Debug catch for some itermitant error.
            if (tile == null)
            {
                Debug.Log(tile.name);
            }

            tile.MakeNeighbors();
        }
        //foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //{
        //    enemy.GetComponent<Tile>().MakeNeighbors();
        //}
        player.GetComponent<ScriptPlayer>().Respawn();

        yield break;
	}

    /// <summary>
    /// Spawn enemies in on the spawn points per difficulty
    /// TODO
    /// </summary>
    void DoTheRandomSpawning()
    {
        int numSkeletons = 5;
        foreach(GameObject spawnPoint in skeletonSpawnPoints)
        {
            GameObject tempSkeleton = (GameObject)Instantiate(skeletonPrefab, spawnPoint.transform.position, Quaternion.identity);
            //tempSkeleton.transform.position = tempSprite.transform.position;
            tempSkeleton.GetComponentInChildren<Tile>().xIndex = spawnPoint.GetComponent<Tile>().xIndex;
            tempSkeleton.GetComponentInChildren<Tile>().yIndex = spawnPoint.GetComponent<Tile>().yIndex;

        }
    }
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_EDITOR//cheat codes
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(LoadMap(0));
            
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoadMap(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            LoadMap(2);
        }
        Time.timeScale = 1;

#endif
    }

    void ClearLevel()
    {
        foreach(Tile tile in GameObject.FindObjectsOfType<Tile>())
        {
            //Debug.Log(tile.gameObject.name);
            Destroy(tile.gameObject);
        }
    }
}
