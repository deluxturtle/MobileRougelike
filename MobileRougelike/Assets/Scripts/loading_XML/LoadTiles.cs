using UnityEngine;
using System.Xml;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: 
/// </summary>
public class LoadTiles : MonoBehaviour {

    //holds the .xml file.
    public TextAsset mapInformation;
    //a temporary tile object
    public GameObject tempCube;

    private Sprite[] sprites;

	// Use this for initialization
	void Start () {
        //Load the tileset into the sprites array
        sprites = Resources.LoadAll<Sprite>("roguelikeDungeon_transparent");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(mapInformation.text);

        //Manuever the camera
        //Camera.main.transform.position = new Vector3();
        //Camera.main.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

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
            int layerWidth = int.Parse(layerInfo.Attributes["width"].Value);
            int layerHeight = int.Parse(layerInfo.Attributes["height"].Value);

            //In the background layer in the xml
            if(layerInfo.Attributes["name"].Value == "Background")
            {
                //Pull out of the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int verticalIndex = 0;
                int horizontalIndex = 0;

                //for each tile inside the background
                foreach(XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    //if sprite is not empty
                    if(spriteValue > 0)
                    {
                        //create a sprite
                        GameObject tempSprite = new GameObject("test");
                        SpriteRenderer spriteRenderer = tempSprite.AddComponent<SpriteRenderer>();
                        spriteRenderer.sprite = sprites[spriteValue - 1];

                        tempSprite.transform.position = new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex));
                        spriteRenderer.sortingLayerName = "Background";
                        //Instantiate(tempCube, new Vector3((tileWidth * horizontalIndex), (tileHeight * verticalIndex)), Quaternion.identity);

                    }

                    horizontalIndex++;
                    if(horizontalIndex % layerWidth == 0)
                    {
                        //Increase our virtical location
                        verticalIndex++;
                        //reset our horizontal location.
                        horizontalIndex = 0;
                    }
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
