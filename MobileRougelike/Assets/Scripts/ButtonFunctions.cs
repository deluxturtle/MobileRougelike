using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

	public void _RestartLevel()
    {
        Scene currentScene;
        currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }
}
