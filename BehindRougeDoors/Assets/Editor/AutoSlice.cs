using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Slices sprites with empty sprites.
/// </summary>
public class AutoSlice : MonoBehaviour {

    [MenuItem("RougeTools/AutoSlice")]
    static void Slice()
    {
        //Load in the texture from the project folder.
        Texture2D myTex = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/dungeon_tileset_calciumtrice.png", typeof(Texture2D));
        //Holds the path to the file.
        string path = AssetDatabase.GetAssetPath(myTex);

        //Lets you modify texture files.
        TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter;

        


        //Do a check to see if its in multiple mode
        //if(ti.spriteImportMode == SpriteImportMode.Multiple)
        //{
        //    ti.spriteImportMode = SpriteImportMode.Single;
        //    AssetDatabase
        //}


        //Holds the inner sprite data
        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        //How big you want the sprice to slice.
        int sliceWidth = 16;
        int sliceHeight = 16;

        Debug.Log(ti.assetPath);

        //For every sliceWidth cut a new one.
        for(int i = 0; i < myTex.width; i += sliceWidth)
        {
            for(int j = myTex.height; j > 0; j -= sliceHeight)
            {
                //Create a new inner sprite
                SpriteMetaData smd = new SpriteMetaData();
                //Set the pivot to the center.
                smd.pivot = new Vector2(0.5f, 0.5f);
                //9 = custom alignment, 0 = center.
                smd.alignment = 9;

                smd.name = (myTex.height - j) / sliceHeight + ", " + i / sliceWidth;
                smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);

                newData.Add(smd);

            }
        }

        SpriteMetaData[] tempSpriteSheet = newData.ToArray();

        //set the sprite sheet to the list we just created.
        ti.spritesheet = tempSpriteSheet;
        ti.spriteImportMode = SpriteImportMode.Multiple;

        ti.SaveAndReimport();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.Default);
    }
    
}
