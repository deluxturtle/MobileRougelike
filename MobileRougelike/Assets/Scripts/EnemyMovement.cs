using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    GameObject heroObj;
    bool enemyRight, enemyLeft, enemyUp, enemyDown;
    float enemySpeed;
    Animator enemyAnimator;

	// Use this for initialization
	void Start () {
        enemyDown = enemyRight = enemyLeft = enemyUp = false;
        enemyAnimator = GetComponent<Animator>();
        heroObj = GameObject.FindGameObjectWithTag("Player");
        enemySpeed = 1.0f;
        InvokeRepeating("Accelerate", 2f, 5f);
	}
	
    void Accelerate()
    {
        enemySpeed++;
    }

	// Update is called once per frame
	void Update () {
        EnemyMove();

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Time!");
            Time.timeScale = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Orb(Clone)")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        if(heroObj != null)
        {
            if(transform.position.y > heroObj.transform.position.y)//if enemy is above player?
            {
                enemyAnimator.SetBool("down", true);
                enemyAnimator.SetBool("up", false);
                enemyAnimator.SetBool("left", false);
                enemyAnimator.SetBool("right", false);

                enemyDown = true;
                enemyUp = enemyLeft = enemyRight = false;

                transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
            }
            else//enemy must be below the player.
            {
                enemyAnimator.SetBool("down", false);
                enemyAnimator.SetBool("up", true);
                enemyAnimator.SetBool("left", false);
                enemyAnimator.SetBool("right", false);

                enemyUp = true;
                enemyDown = enemyLeft = enemyRight = false;

                transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);
            }

            if(transform.position.x < heroObj.transform.position.x)//is enemy left of the player?
            {
                enemyAnimator.SetBool("down", false);
                enemyAnimator.SetBool("up", false);
                enemyAnimator.SetBool("left", false);
                enemyAnimator.SetBool("right", true);

                enemyRight = true;
                enemyDown = enemyLeft = enemyUp = false;
                transform.Translate(Vector3.right * enemySpeed * Time.deltaTime);
            }
            else
            {
                enemyAnimator.SetBool("down", false);
                enemyAnimator.SetBool("up", false);
                enemyAnimator.SetBool("left", true);
                enemyAnimator.SetBool("right", false);

                enemyLeft = true;
                enemyDown = enemyRight = enemyUp = false;
                transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
            }
        }
    }
}
