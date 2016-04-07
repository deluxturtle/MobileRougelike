using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Checks to see if you've played before and
/// Sets the loaded level
/// </summary>
public class SaveInfo : MonoBehaviour {

    public GameObject directionsPanel;
    public GameObject menus;
    public GameObject saveNotifierPanel;

    //saved properties
    public int savedHealth;
    public int savedLevelIndex = 0;

    public bool newGame = false;

    //gameover stats
    public int lvlsCompleted;
    public int score;

    static bool created = false;



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
        else
        {
            //Load in the player pref save info
            savedHealth = PlayerPrefs.GetInt("health");
            savedLevelIndex = PlayerPrefs.GetInt("savedLevel");
        }

        //switch (PlayerPrefs.GetInt("savedLevel"))
        //{
        //    case 1:
        //        savedLevelIndex = 1;
        //        break;
        //    case 2:
        //        savedLevelIndex = 2;
        //        break;
        //    case 3:
        //        savedLevelIndex = 3;
        //        break;
        //    default:
        //        savedLevelIndex = 0;
        //        break;
        //}

        Debug.Log(savedLevelIndex);
    }

    public void SaveGame()
    {
        LoadTiles levelLoader = GameObject.Find("LevelLoad").GetComponent<LoadTiles>();
        int currentLevel = levelLoader.levelNum + 1;

        int currentHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().health;

        Debug.Log("Level: " + currentLevel);
        PlayerPrefs.SetInt("savedLevel", currentLevel);
        Debug.Log("Health: " + currentHealth);
        PlayerPrefs.SetInt("health", currentHealth);
    }

    public void LoadGame()
    {
        if(SceneManager.GetActiveScene().name == "mainMenu")
        {
            if(savedLevelIndex == 0)
            {
                saveNotifierPanel.SetActive(true);
            }
            else
            {
                FindObjectOfType<LoadScenes>()._LoadGame();
            }
        }
    }
	
}
