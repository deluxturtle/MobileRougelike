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
	
    public void ChangeDifficutly(Difficutly pDifficulty)
    {
        currentDifficulty = pDifficulty;
    }

}
