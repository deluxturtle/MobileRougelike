using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: CameraScreen 
/// </summary>

public class CameraControl : MonoBehaviour {

    #region Fields

    public float perspectiveZoomSpeed = 0.5f;
    public float orthographicZoomSpeed = 0.5f;

    float hMovement = 0f;
    float vMovement = 0f;
    float cameraMoveSpeed = 0.01f;
    float cameraMove = 0.01f;
    Camera myCam;
    bool isOrthographic = false;

    #endregion

    //Andrew
    public bool justFollow = true;

    void Start()
    {
        myCam = GetComponent<Camera>();

        if (myCam.orthographic)
        {
            cameraMoveSpeed = cameraMove * myCam.orthographicSize;
            isOrthographic = true;
        }
        else
        {
            cameraMoveSpeed = cameraMove * myCam.fieldOfView;
        }
    }

    void Update()
    {
        if (justFollow)
        {
            //Follow me
        }
        else
        {
            transform.Translate(hMovement, vMovement, 0);

            Zoom();

            float tempX = (float)System.Math.Round(Input.acceleration.x, 2);
            float tempY = (float)System.Math.Round(Input.acceleration.y, 2);

            if ((tempX > 0.02f || tempX < -0.02f) || (tempY > 0.02f || tempY < -0.02f))
            {
                transform.Translate(tempX * cameraMoveSpeed, tempY * cameraMoveSpeed, 0f);
                //print(string.Format("im moving!!!! {0}x {1}y", tempX * cameraMoveSpeed, tempY * cameraMoveSpeed));
            }
        }
    }

    void Zoom()
    {
        if (Input.touchCount == 2)
        {
            Touch zeroTouch = Input.GetTouch(0);
            Touch oneTouch = Input.GetTouch(1);

            Vector2 zeroTouchPrevious = zeroTouch.position - zeroTouch.deltaPosition;
            Vector2 oneTouchPrevious = oneTouch.position - oneTouch.deltaPosition;

            float previousTouchDeltaMag = (zeroTouchPrevious - oneTouchPrevious).magnitude;
            float touchDeltaMag = (zeroTouch.position - oneTouch.position).magnitude;

            float deltaMagDifference = previousTouchDeltaMag - touchDeltaMag;

            //if ortho
            if (isOrthographic)
            {
                myCam.orthographicSize += deltaMagDifference * orthographicZoomSpeed;
                myCam.orthographicSize = Mathf.Clamp(myCam.orthographicSize, 2f, 5f);
                cameraMoveSpeed = cameraMove * myCam.orthographicSize;
            }
            else
            {
                myCam.fieldOfView += deltaMagDifference * perspectiveZoomSpeed;
                myCam.fieldOfView = Mathf.Clamp(myCam.fieldOfView, 0.1f, 179.9f);
                cameraMoveSpeed = cameraMove * myCam.fieldOfView;
            }
        }
    }

}
