using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @Author: Andrew Seba
/// </summary>
public class Score : MonoBehaviour {

    public Text scoreText;
    public int score = 5000;

    public int timeDeduction = 5;

	// Use this for initialization
	void Start ()
    {
        //initialize the score text
        UpdateScoreText();

        //every second you loose points.
        InvokeRepeating("DeductScore", 0, 1);
	}

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    //Used to deduct a default amount(time deduction)
    void DeductScore()
    {
        score -= timeDeduction;
        UpdateScoreText();
    }

    void DeductScore(int amount)
    {
        score -= amount;
    }
}
