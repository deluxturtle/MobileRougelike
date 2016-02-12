using UnityEngine;
using System.Collections;
using System;

public class ScriptPlayer : MonoBehaviour {

    public float moveSpeed = 2.0f;
    bool left, right, up, down;
    Animator animator;

	// Use this for initialization
	void Start () {
        left = right = up = down = false;
        animator = GetComponent<Animator>();
	}

 //   // Update is called once per frame
 //   void Update() {
 //       if (Input.GetAxis("Horizontal") < 0)
 //       {
 //           animator.SetBool("left", true);
 //       }
 //       if (Input.GetAxis("Horizontal") > 0)
 //       {
 //           animator.SetBool("right", true);
 //       }
 //       if(Input.GetAxis("Vertical") > 0)
 //       {
 //           animator.SetBool("up", true);
 //       }
 //       if(Input.GetAxis("Vertical") < 0)
 //       {
 //           animator.SetBool("down", true);
 //       }
	//}

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("left", false);
            animator.SetBool("right", true);
            animator.SetBool("up", false);
            animator.SetBool("down", false);

            left = up = down = false;
            right = true;

            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (right)
        {
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);

            right = up = down = false;
            left = true;


            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (left)
        {
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", true);
            animator.SetBool("down", false);

            right = left = down = false;
            up = true;

            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (up)
        {
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);

            right = left = up = false;
            up = false;

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        
        if (down)
        {
        }

    }
}
