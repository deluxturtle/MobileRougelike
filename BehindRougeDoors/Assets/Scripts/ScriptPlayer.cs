using UnityEngine;
using System.Collections.Generic;
using System;

public class ScriptPlayer : MonoBehaviour {

    public float moveSpeed = 2.0f;
    bool left, right, up, down;
    Animator animator;

    //orbstuff
    public GameObject orb;
    float orbSpeed = 20f;

    int sightRange = 5;

    int xIndex = 3;
    int yIndex = 3;

    List<GameObject> lightTiles;
    

	// Use this for initialization
	void Start () {

        LightArea();

        left = right = up = false;
        down = true;
        animator = GetComponent<Animator>();
	}

    //Background<18, 28>

    void AddNeighbors(int pRange, int[] pUnitIndex, List<GameObject> pList)
    {
        int z = pUnitIndex[0];
        int zBack = z - 1;
        int zFoward = z + 1;

        int x = pUnitIndex[1];
        int xLeft = x - 1;
        int xRight = x + 1;

        int y = pUnitIndex[2];
        //int yUp = y + 1;
        //int yDown = y - 1;

        //Check back
        if (zBack >= 0)
        {
            GameObject upTile = GameObject.Find("Background <" + 18, 28 >);
            if (upTile != null && !pList.Contains(upTile))
            {
                upTile.GetComponent<ScriptTile>().range += pRange;
                pList.Add(upTile);
            }
        }

        //Checkfoward
        if (zFoward < scriptGrid.gridLength)
        {
            GameObject downTile = scriptGrid.grid[zFoward, x, y];
            if (
                downTile.GetComponent<ScriptTile>().range += pRange;
                pList.Add(downTile);
            }downTile != null && !pList.Contains(downTile))
            {
        }

        //CheckLeft
        if (xLeft >= 0)
        {
            GameObject leftTile = scriptGrid.grid[z, xLeft, y];
            if (leftTile != null && !pList.Contains(leftTile))
            {
                leftTile.GetComponent<ScriptTile>().range += pRange;
                pList.Add(leftTile);
            }
        }

        //CheckRight
        if (xRight < scriptGrid.gridWidth)
        {
            GameObject rightTile = scriptGrid.grid[z, xRight, y];
            if (rightTile != null && !pList.Contains(rightTile))
            {
                rightTile.GetComponent<ScriptTile>().range += pRange;
                pList.Add(rightTile);
            }
        }
    }


    void LightArea()
    {


        ////Test the light
        //foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        //{
        //    if (tile.GetComponent<Tile>().xIndex > xIndex - sightRange &&
        //        tile.GetComponent<Tile>().xIndex < xIndex + sightRange &&
        //        tile.GetComponent<Tile>().yIndex > yIndex - sightRange &&
        //        tile.GetComponent<Tile>().yIndex < yIndex + sightRange)
        //    {
        //        tile.GetComponent<Tile>().LightTile();
        //    }

        //}
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        MoveCharacter();
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("blast");
            Invoke("SpawnOrb", 0.5f);
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
        
    }

    private void MoveCharacter()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {

            animator.SetTrigger("walkRight");

            left = up = down = false;
            right = true;

            GetComponent<SpriteRenderer>().flipX = false;

            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            xIndex++;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
            animator.SetTrigger("walkRight");

            right = up = down = false;
            left = true;

            GetComponent<SpriteRenderer>().flipX = true;


            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            xIndex--;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            right = left = down = false;
            up = true;

            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
            yIndex++;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

            right = left = up = false;
            down = true;

            transform.position = new Vector2(transform.position.x, transform.position.y - 1);

            yIndex--;
        }

        LightArea();

    }
}
