using UnityEngine;
using System.Collections;

/// <summary>
/// What difficulty the game is initially set at.
/// </summary>
public enum Difficutly
{
    EASY,
    MEDIUM,
    HARD
}

/// <summary>
/// @Author: Andrew Seba
/// @Description: Controls difficulty and level scaling between levels when
/// traversing through the game.
/// </summary>
public class ScriptGameManager : MonoBehaviour
{
    public Difficutly currentDifficulty = Difficutly.MEDIUM;

    public int enemiesKilled = 0;

    public void IncreaseDifficulty()
    {
        switch (currentDifficulty)
        {
            case Difficutly.EASY:
                break;
            case Difficutly.MEDIUM:
                break;
            case Difficutly.HARD:
                break;
        }
    }
	
    public void ChangeDifficutly(Difficutly pDifficulty)
    {
        currentDifficulty = pDifficulty;
    }

}
