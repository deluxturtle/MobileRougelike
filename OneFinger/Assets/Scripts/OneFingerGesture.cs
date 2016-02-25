using UnityEngine;
using System.Collections;

public class OneFingerGesture : MonoBehaviour {

    public int i_comfort = 3;
    GameObject objectToMove;
    private Vector2 v2_previous;
    private Vector2 v2_current;
    private float f_touch_delta;
	
	// Update is called once per frame
	void Update () {
	    if(Input.touchCount == 1)
        {
            //Begin Touch Phase
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                v2_previous = Input.GetTouch(0).position;
            }

            //On touch End.
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                v2_current = Input.GetTouch(0).position;
                f_touch_delta = v2_current.magnitude - v2_previous.magnitude;

                //If the magnitude of swipe is greater than 3
                if (Mathf.Abs(f_touch_delta) > i_comfort)
                {
                    Debug.Log("Swipe");
                    //If swiping left or down
                    if(f_touch_delta < 0)
                    {
                        //if x is greater to the left than going down then we are going left
                        if (Mathf.Abs(v2_current.x - v2_previous.x) > Mathf.Abs(v2_current.y - v2_previous.y))
                        {
                            print("Left");
                        }
                        else
                        {
                            print("Down");
                        }
                    }
                    //else we are swiping right or up.
                    else
                    {
                        if(Mathf.Abs(v2_current.x - v2_previous.x) > Mathf.Abs(v2_current.y - v2_previous.y))
                        {
                            print("Right");
                        }
                        else
                        {
                            print("Up");
                        }
                    }
                }
                else
                {
                    //Single Tap
                    if (Input.GetTouch(0).tapCount == 1)
                    {

                        RaycastHit hit;
                        Ray ray;
                        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                        if (Physics.Raycast(ray, out hit, 100.0f))
                        {

                            if (objectToMove != null && hit.transform.name != objectToMove.transform.name)
                            {
                                //Stop particle.
                                objectToMove.transform.GetChild(0).gameObject.SetActive(false);
                            }
                            objectToMove = hit.transform.gameObject;

                            objectToMove.transform.GetChild(0).gameObject.SetActive(true);

                        }
                    }
                    //Double Tap
                    else if (Input.GetTouch(0).tapCount == 2)
                    {
                        if (objectToMove != null)
                        {
                            Vector3 pos;
                            pos.x = Input.GetTouch(0).position.x;
                            pos.y = Input.GetTouch(0).position.y;

                            pos.z = Mathf.Abs(Camera.main.transform.position.z - objectToMove.transform.position.z);

                            objectToMove.transform.position = Camera.main.ScreenToWorldPoint(pos);
                        }
                    }
                }

            }

        }
    }
}
