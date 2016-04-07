using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Holds functions to load scenes.
/// </summary>
public class LoadScenes : MonoBehaviour {

    public GameObject loadingPanel;

    /// <summary>
    /// Loads a new game making a random map load.
    /// Plays cave music.
    /// </summary>
    public void _NewGame()
    {
        SaveInfo saveInfoScript = FindObjectOfType<SaveInfo>();
        saveInfoScript.newGame = true;

        loadingPanel.SetActive(true);
        SceneManager.LoadScene("LoadMap");
        MusicManager music = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        music.PlayCaveMusic();
    }

    public void _LoadGame()
    {
        SaveInfo saveInfoScript = FindObjectOfType<SaveInfo>();
        saveInfoScript.newGame = false;

        loadingPanel.SetActive(true);
        SceneManager.LoadScene("LoadMap");


        MusicManager music = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        music.PlayCaveMusic();

    }

    /// <summary>
    /// Loads the main menu and plays the main menu music.
    /// </summary>
    public void _MainMenu()
    {
        SceneManager.LoadScene("mainMenu");
        MusicManager music = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        music.PlayMenuMusic();
    }

    public void _GameOver()
    {
        if(FindObjectOfType<Score>() && FindObjectOfType<SaveInfo>())
        {
            Score curScoreScript = FindObjectOfType<Score>();
            SaveInfo saveInfoScript = FindObjectOfType<SaveInfo>();
            //Grab score stuff if available.
            if (curScoreScript && saveInfoScript && !saveInfoScript.newGame)
            {
                saveInfoScript.score = curScoreScript.score;
                saveInfoScript.savedLevelIndex = -1;
                saveInfoScript.savedHealth = 100;
            }
        }
#if UNITY_EDITOR
        else
        {
            Debug.Log("Couldn't find save info script. or score script");
        }
#endif

        SceneManager.LoadScene("GameOver");
        MusicManager music = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        music.PlayCaveMusic();
    }

    public void NextLevel()
    {
        if (FindObjectOfType<SaveInfo>())
        {
            FindObjectOfType<SaveInfo>().lvlsCompleted++;
        }
    }


}
