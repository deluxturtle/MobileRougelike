using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Andrew Seba
/// Description: handles some of the button functionaly and sprite changing.
/// </summary>
public class ScriptDifficultyButtonHelper : MonoBehaviour {

    [Header("Difficulty Buttons")]
    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;

    [Header("Button Graphics")]
    [Tooltip("The image you want to show on the normal buttons.")]
    public Sprite normalButton;
    [Tooltip("The image you want to show on the selected buttons.")]
    public Sprite selectedButton;

    private ScriptGameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<ScriptGameManager>();
        UpdateDifficultySelection();
    }

    void OnEnable()
    {
        if(gameManager == null)
            gameManager = GameObject.Find("GameManager").GetComponent<ScriptGameManager>();

        UpdateDifficultySelection();
    }

    /// <summary>
    /// Changes the sprites on the buttons to highlight the selected difficulty
    /// </summary>
    public void UpdateDifficultySelection()
    {
        switch (gameManager.currentDifficulty)
        {
            case Difficutly.EASY:
                easyButton.GetComponent<Image>().sprite = selectedButton;
                mediumButton.GetComponent<Image>().sprite = normalButton;
                hardButton.GetComponent<Image>().sprite = normalButton;
                break;
            case Difficutly.MEDIUM:
                easyButton.GetComponent<Image>().sprite = normalButton;
                mediumButton.GetComponent<Image>().sprite = selectedButton;
                hardButton.GetComponent<Image>().sprite = normalButton;
                break;
            case Difficutly.HARD:
                easyButton.GetComponent<Image>().sprite = normalButton;
                mediumButton.GetComponent<Image>().sprite = normalButton;
                hardButton.GetComponent<Image>().sprite = selectedButton;
                break;
        }
    }

    /// <summary>
    /// Button hook to change the difficulty to easy.
    /// </summary>
    public void _SetDifficultyEasy()
    {
        gameManager.ChangeDifficutly(Difficutly.EASY);
        UpdateDifficultySelection();
    }

    /// <summary>
    /// Button hook to change the difficulty to easy.
    /// </summary>
    public void _SetDifficultyMedium()
    {
        gameManager.ChangeDifficutly(Difficutly.MEDIUM);
        UpdateDifficultySelection();
    }

    /// <summary>
    /// Button hook to change the difficulty to easy.
    /// </summary>
    public void _SetDifficultyHard()
    {
        gameManager.ChangeDifficutly(Difficutly.HARD);
        UpdateDifficultySelection();
    }

}
