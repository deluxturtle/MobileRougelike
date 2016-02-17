using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    Text scoreBox;
    float score;

    void Start()
    {
        scoreBox = GameObject.Find("Text_Score").GetComponent<Text>();
        InvokeRepeating("AddTime", 0.5f, 0.5f);
    }

    void AddTime()
    {
        score += 0.5f;
        scoreBox.text = string.Format("{0:N2}", System.Math.Round(score, 1).ToString());
    }

}
