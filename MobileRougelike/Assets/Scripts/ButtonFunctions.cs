using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

	public void _RestartLevel()
    {
        Scene currentScene;
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        //reset time because when the player dies time scale gets set to 0.
        Time.timeScale = 1.0f;
    }

    public void _LevelOne()
    {
        SceneManager.LoadScene("levelOne");
    }

    public void _LoadXML()
    {
        SceneManager.LoadScene("LoadMap");
    }
}
