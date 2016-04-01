using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScenes : MonoBehaviour {



    public void _NewGame()
    {
        SceneManager.LoadScene("LoadMap");
        MusicManager music = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        music.PlayCaveMusic();
    }


}
