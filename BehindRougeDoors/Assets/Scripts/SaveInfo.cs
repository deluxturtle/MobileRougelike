using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Checks to see if you've played before and
/// Sets the loaded level
/// </summary>
public class SaveInfo : MonoBehaviour {

    public GameObject directionsPanel;
    public GameObject menus;

    static bool created = false;

    string savedLevelName = "";


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;

        }
        else
        {
            Destroy(gameObject);
            return;
        }



        if (PlayerPrefs.GetString("PlayedBefore") != "yes")
        {
            PlayerPrefs.SetString("PlayedBefore", "yes");
            if (GameObject.Find("Menus"))
            {
                GameObject.Find("Menus").GetComponent<Animator>().SetTrigger("directions");
                menus.GetComponent<SwipeInput>().currentMenu = MainMenus.DIRECTIONS;
                directionsPanel.SetActive(true);
            }
        }

        switch (PlayerPrefs.GetString("savedLevel"))
        {
            case "Level1":
                savedLevelName = "Level1";
                break;
            case "Level2":
                savedLevelName = "Level2";
                break;
            case "Level3":
                savedLevelName = "Level3";
                break;
            default:
                savedLevelName = "none";
                break;
        }

        Debug.Log(savedLevelName);
    }

    public void SaveGame()
    {
        LoadTiles levelLoader = GameObject.Find("LevelLoad").GetComponent<LoadTiles>();
        string currentLevel = levelLoader.mapInformation[levelLoader.levelNum].ToString();
    }
	
}
