using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// </summary>
public class ZoomRotate : MonoBehaviour {
    
    public GameObject cubeToRotate;
    public float minSwipeDistance = 2;
    public Camera myCamera;
    public float zoomAmount = 5;
    public float rotateAmount = 1;

    private Vector2 v2_current;
    private Vector2 v2_previous;
    private float touchMagnitude;
    private float angle;
	

	void Update ()
    {
	    if(Input.touchCount == 2 &&
            Input.GetTouch(0).phase == TouchPhase.Moved &&
            Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            v2_current = Input.GetTouch(0).position - Input.GetTouch(1).position;
            v2_previous = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) -
                (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //Calculate the distance between the two fingers.
            touchMagnitude = v2_current.magnitude - v2_previous.magnitude;
            //Grab the angle.
            angle = Vector2.Angle(v2_previous, v2_current);

            if(angle > 0.1)
            {
                Debug.Log(Vector3.Cross(v2_current, v2_previous));
                if(Vector3.Cross(v2_current, v2_previous).z < 0.0f)
                {
                    Debug.Log("Counter Clockwise");
                    cubeToRotate.transform.Rotate(Vector3.up, angle * -1 * rotateAmount);
                }
                else
                {
                    Debug.Log("Clockwise");
                    cubeToRotate.transform.Rotate(Vector3.up, angle * rotateAmount);
                }
            }

            //Zoom gesture
            if(Mathf.Abs(touchMagnitude) > minSwipeDistance)
            {
                //print("Zoom detected");
                if(touchMagnitude > 0)
                {
                    //zoom in
                    myCamera.fieldOfView = Mathf.Clamp( Mathf.Lerp(myCamera.fieldOfView, myCamera.fieldOfView - Mathf.Abs(touchMagnitude) * zoomAmount, Time.deltaTime), 10,70);

                    Debug.Log("Zoom In");
                }
                else
                {
                    //zoom out
                    myCamera.fieldOfView = Mathf.Clamp(Mathf.Lerp(myCamera.fieldOfView, myCamera.fieldOfView + Mathf.Abs(touchMagnitude) * zoomAmount, Time.deltaTime), 10, 70);

                    Debug.Log("Zoom Out");
                }
            }
        }
	}
}
