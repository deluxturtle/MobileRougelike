using UnityEngine;
using System;
using System.Collections;


enum MainMenus
{
    NEWGAME,
    LOADGAME,
    CREDITS,
    SETTINGS,
    DIRECTIONS
}

/// <summary>
/// @Author: Andrew Seba
/// @Description: Handles swiping left and right for menu selection.
/// </summary>
public class SwipeInput : MonoBehaviour {

    [Tooltip("How long the swipe must be to be registered as a \"Swipe\"")]
    public int minSwipeDistance = 3;

    public bool swipeDisabled = false;

    private Vector2 swipeStartPos;
    private Vector2 swipeCurrentPos;
    private float swipeMagnitude;

    Animator menuAnimator;

    MainMenus currentMenu = MainMenus.NEWGAME;

    //So we can use the sliders on the settings menu
    public void _DisableSwipe()
    {
        swipeDisabled = true;
    }
    public void _EnableSwipe()
    {
        swipeDisabled = false;
    }

    void Start()
    {
        menuAnimator = GetComponentInChildren<Animator>();
        currentMenu = MainMenus.NEWGAME;
    }
    
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GotoPreviousMenu();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GotoNextMenu();
        }
	    //If only 1 touch went down
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            //if touch is in the begin phase
            if(touch.phase == TouchPhase.Began)
            {
                swipeStartPos = touch.position;
            }

            //On touch end
            if(touch.phase == TouchPhase.Ended)
            {
                swipeCurrentPos = touch.position;
                swipeMagnitude = swipeCurrentPos.magnitude - swipeStartPos.magnitude;

                //If the magnitude of the swipe is greater than minSwipeDistance.
                if(Mathf.Abs(swipeMagnitude) > minSwipeDistance && !swipeDisabled)
                {
                    //Debug.Log("Swipe");
                    //If swiping left or down
                    if(swipeMagnitude < 0)
                    {
                        //if x is greater to the left than going down then we are going left
                        if(Mathf.Abs(swipeCurrentPos.x - swipeStartPos.x) > Mathf.Abs(swipeCurrentPos.y - swipeStartPos.y))
                        {
                            //print("Left");
                            GotoNextMenu();
                        }
                        else
                        {
                            //print("Down");
                        }
                    }
                    //else we are swiping right or up.
                    else
                    {
                        if (Mathf.Abs(swipeCurrentPos.x - swipeStartPos.x) > Mathf.Abs(swipeCurrentPos.y - swipeStartPos.y))
                        {
                            //print("Right");

                            GotoPreviousMenu();
                        }
                        else
                        {
                            //print("Up");
                        }
                    }
                }

            }
        }
	}

    public void GotoPreviousMenu()
    {

        string currentMenuString = currentMenu.ToString();

        //Get the previous menu enum.
        if((MainMenus)Enum.Parse(typeof(MainMenus), currentMenuString) - 1 < 0)
        {
            currentMenu = MainMenus.DIRECTIONS;
        }
        else
        {
            currentMenu = (MainMenus)Enum.Parse(typeof(MainMenus), currentMenuString) - 1;
        }

        //Move with mechanim
        menuAnimator.SetTrigger(currentMenu.ToString().ToLower());
    }

    public void GotoNextMenu()
    {

        //Get the next menu but go back to zero if its the last one.
        int nextMenuNum = ((int)currentMenu + 1) % 5;

        currentMenu = (MainMenus)nextMenuNum;

        menuAnimator.SetTrigger(currentMenu.ToString().ToLower());

    }
}
