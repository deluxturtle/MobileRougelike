using UnityEngine;
using System.Collections;
using System;

public class ScriptPlayer : MonoBehaviour {

    public float moveSpeed = 2.0f;
    bool left, right, up, down;
    Animator animator;

    //orbstuff
    public GameObject orb;
    float orbSpeed = 20f;



	// Use this for initialization
	void Start () {
        left = right = up = false;
        down = true;
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
#elif UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    SpawnOrb();
                }
            }
        }
#endif
    }

    void SpawnOrb()
    {
        
        GameObject orbInstance = (GameObject)Instantiate(orb, transform.position, Quaternion.identity);
        Rigidbody2D orbRigid = orbInstance.GetComponent<Rigidbody2D>();
        if (right)
        {
            orbRigid.velocity = new Vector2(orbSpeed, 0f);

        }
        if (left)
        {
            orbRigid.velocity = new Vector2(-orbSpeed, 0f);
        }
        if (up)
        {
            orbRigid.velocity = new Vector2(0, orbSpeed);
        }
        if (down)
        {
            orbRigid.velocity = new Vector2(0, -orbSpeed);
        }
    }

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

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("left", false);
            animator.SetBool("right", false);
            animator.SetBool("up", false);
            animator.SetBool("down", true);

            right = left = up = false;
            down = true;

            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

    }
}
