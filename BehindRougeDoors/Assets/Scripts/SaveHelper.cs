using UnityEngine;
using System.Collections;

/// <summary>
/// Grabs refernces to the save info singleton.
/// </summary>
public class SaveHelper : MonoBehaviour {

    [HideInInspector]
    public SaveInfo saveInfo;

	// Use this for initialization
	void Start ()
    {
#if UNITY_EDITOR
        if(GameObject.Find("SaveInfo") == null)
        {
            Debug.Log("(Saving Disabled) Load Game from mainMenu.");
        }
        else
        {
            saveInfo = GameObject.Find("SaveInfo").GetComponent<SaveInfo>();
        }
#else
        saveInfo = GameObject.Find("SaveInfo").GetComponent<SaveInfo
#endif
	}

    public void _SaveAndQuit()
    {
        saveInfo.SaveGame();
        Application.Quit();
    }
}
