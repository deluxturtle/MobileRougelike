using UnityEngine;
using System.Collections;

public class SaveInfo : MonoBehaviour {

    public GameObject directionsPanel;
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
            GameObject.Find("Menus").GetComponent<Animator>().SetTrigger("directions");
            directionsPanel.SetActive(true);
        }

        switch (PlayerPrefs.GetString("savedLevel"))
        {
            case "Level1":
                savedLevelName = "Level1";
                break;
            case "Level2":
                savedLevelName = "Level2";
                break;
            default:
                savedLevelName = "none";
                break;
        }

        Debug.Log(savedLevelName);
    }
	
}
