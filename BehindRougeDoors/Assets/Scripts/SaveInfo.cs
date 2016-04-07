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

    public int score;
    public int lvlsCompleted;

    static bool created = false;

    int savedLevelIndex = 0;


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

        switch (PlayerPrefs.GetInt("savedLevel"))
        {
            case 1:
                savedLevelIndex = 1;
                break;
            case 2:
                savedLevelIndex = 2;
                break;
            case 3:
                savedLevelIndex = 3;
                break;
            default:
                savedLevelIndex = 0;
                break;
        }

        Debug.Log(savedLevelIndex);
    }

    public void SaveGame()
    {
        LoadTiles levelLoader = GameObject.Find("LevelLoad").GetComponent<LoadTiles>();
        int currentLevel = levelLoader.levelNum;

        int currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().health;

        Debug.Log("Level: " + currentLevel);
        PlayerPrefs.SetInt("savedLevel", currentLevel);
        Debug.Log("Health: " + currentHealth);
        PlayerPrefs.SetInt("health", currentHealth);
    }

    public void LoadGame()
    {

    }
	
}
