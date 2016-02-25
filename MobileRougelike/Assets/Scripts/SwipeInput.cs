using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Handles swiping left and right for menu selection.
/// </summary>
public class SwipeInput : MonoBehaviour {

    [Tooltip("How long the swipe must be to be registered as a \"Swipe\"")]
    public int minSwipeDistance = 3;

    private Vector2 swipeStartPos;
    private Vector2 swipeCurrentPos;
    private float swipeMagnitude;

    public Animator menuAnimator;
    bool onMain = true;

    
	// Update is called once per frame
	void Update ()
    {
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
                if(Mathf.Abs(swipeMagnitude) > minSwipeDistance)
                {
                    //Debug.Log("Swipe");
                    //If swiping left or down
                    if(swipeMagnitude < 0)
                    {
                        //if x is greater to the left than going down then we are going left
                        if(Mathf.Abs(swipeCurrentPos.x - swipeStartPos.x) > Mathf.Abs(swipeCurrentPos.y - swipeStartPos.y))
                        {
                            //print("Left");
                            //if on main then swipe to credits
                            if(menuAnimator.GetBool("onCredits") == false && onMain)
                            {
                                menuAnimator.SetBool("onCredits", true);
                                onMain = false;
                            }
                            
                            //if on settings then go back to main.
                            else if(menuAnimator.GetBool("onOptions") && !onMain)
                            {
                                menuAnimator.SetBool("onOptions", false);
                                onMain = true;
                            }
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
                            if(menuAnimator.GetBool("onCredits") && !onMain)
                            {
                                menuAnimator.SetBool("onCredits", false);
                                onMain = true;
                            }
                            else if(!menuAnimator.GetBool("onOptions") && onMain)
                            {
                                menuAnimator.SetBool("onOptions", true);
                                onMain = false;
                            }
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
}
