using UnityEngine;
using System.Collections.Generic;
using System;

public class ScriptPlayer : MonoBehaviour {

    public float moveSpeed = 2.0f;
    public GameObject orb;

    bool left, right, up, down;
    Animator animator;
    float orbSpeed = 20f;
    int sightRange = 5;
    int xIndex = 3;
    int yIndex = 3;
    Collider2D myCollider;

    List<GameObject> curLitTiles;

    Vector2 targetPos;

	// Use this for initialization
	void Start () {
        myCollider = GetComponent<Collider2D>();
        LightArea();

        left = right = up = false;
        down = true;
        animator = GetComponent<Animator>();
	}

    #region LightArea
    void ClearRange()
    {
        foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            tile.GetComponent<Tile>().tempRange = 0;
        }
    }

    List<GameObject> FindTiles(int pRange)
    {
        ClearRange();
        List<GameObject> tempList = new List<GameObject>();
        //Grab the tile i am on.
        
        GameObject myTile = GameObject.Find("Background <" + xIndex +", " + yIndex + ">");
        tempList.Add(myTile);

        //Add my 1 distance neighbors
        if(myTile.GetComponent<Tile>().northTiles.Count > 0)
        {
            foreach(GameObject neighborTile in myTile.GetComponent<Tile>().northTiles)
            {
                neighborTile.GetComponent<Tile>().tempRange = 1;
                tempList.Add(neighborTile);
            }
        }
        if(myTile.GetComponent<Tile>().southTiles.Count > 0)
        {
            foreach(GameObject neighborTile in myTile.GetComponent<Tile>().southTiles)
            {
                neighborTile.GetComponent<Tile>().tempRange = 1;
                tempList.Add(neighborTile);
            }
        }
        if(myTile.GetComponent<Tile>().eastTiles.Count > 0)
        {
            foreach(GameObject neighborTile in myTile.GetComponent<Tile>().eastTiles)
            {
                neighborTile.GetComponent<Tile>().tempRange = 1;
                tempList.Add(neighborTile);
            }
        }
        if (myTile.GetComponent<Tile>().westTiles.Count > 0)
        {
            foreach(GameObject neighborTile in myTile.GetComponent<Tile>().westTiles)
            {
                neighborTile.GetComponent<Tile>().tempRange = 1;
                tempList.Add(neighborTile);
            }
        }


        for (int range = 1; range <= pRange; range++)
        {
            foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
            {
                if(tile != null && tile.GetComponent<Tile>().tempRange == range)
                {
                    if(range < pRange)
                    {
                        Tile tempTile = tile.GetComponent<Tile>();
                        //add their neighbors to the list
                        foreach(GameObject neighborTile in tile.GetComponent<Tile>().neighbors)
                        {
                            if (!tempList.Contains(neighborTile) && !tempTile.blocksLight)
                            {
                                neighborTile.GetComponent<Tile>().tempRange = range + 1;
                                tempList.Add(neighborTile);
                            }
                        }
                        //if (tempTile.southTile != null && !tempList.Contains(tempTile.southTile))
                        //{
                        //    tempTile.southTile.GetComponent<Tile>().tempRange = range+1;
                        //    tempList.Add(tempTile.southTile);
                        //}
                        //if (tempTile.eastTile != null && !tempList.Contains(tempTile.eastTile))
                        //{
                        //    tempTile.eastTile.GetComponent<Tile>().tempRange = range+1;
                        //    tempList.Add(tempTile.eastTile);
                        //}
                        //if (tempTile.westTile != null && !tempList.Contains(tempTile.westTile))
                        //{
                        //    tempTile.westTile.GetComponent<Tile>().tempRange = range+1;
                        //    tempList.Add(tempTile.westTile);
                        //}

                    }
                }
            }
        }

        //int z = pUnitIndex[0];
        //int zBack = z - 1;
        //int zFoward = z + 1;

        //int x = pUnitIndex[1];
        //int xLeft = x - 1;
        //int xRight = x + 1;

        //int y = pUnitIndex[2];
        ////int yUp = y + 1;
        ////int yDown = y - 1;

        ////Check back
        //if (zBack >= 0)
        //{
        //    GameObject upTile = GameObject.Find("Background <" + 18 + 28 +">");
        //    if (upTile != null && !pList.Contains(upTile))
        //    {
        //        upTile.GetComponent<Tile>().tempRange += pRange;
        //        pList.Add(upTile);
        //    }
        //}

        ////Checkfoward
        //if (zFoward < scriptGrid.gridLength)
        //{
        //    GameObject downTile = scriptGrid.grid[zFoward, x, y];
        //    if (
        //        downTile.GetComponent<ScriptTile>().range += pRange;
        //        pList.Add(downTile);
        //    }downTile != null && !pList.Contains(downTile))
        //    {
        //}

        ////CheckLeft
        //if (xLeft >= 0)
        //{
        //    GameObject leftTile = scriptGrid.grid[z, xLeft, y];
        //    if (leftTile != null && !pList.Contains(leftTile))
        //    {
        //        leftTile.GetComponent<ScriptTile>().range += pRange;
        //        pList.Add(leftTile);
        //    }
        //}

        ////CheckRight
        //if (xRight < scriptGrid.gridWidth)
        //{
        //    GameObject rightTile = scriptGrid.grid[z, xRight, y];
        //    if (rightTile != null && !pList.Contains(rightTile))
        //    {
        //        rightTile.GetComponent<ScriptTile>().range += pRange;
        //        pList.Add(rightTile);
        //    }
        //}

        return tempList;
    }


    void LightArea()
    {

        curLitTiles = FindTiles(2);

        foreach(GameObject tile in curLitTiles)
        {
            tile.GetComponent<Tile>().LightTile();
        }
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

    #endregion

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, targetPos.normalized, Color.red);
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

    void CheckAndMove(Vector2 pPos)
    {
        bool hitWall = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, pPos.normalized, 1f);
        for(int i = 0; i < hit.Length; i++)
        {
            if(hit[i].collider.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Obstacles")
            {
                Debug.Log("Hit wall");
                hitWall = true;
                break;
            }
        }

        //If we didn't detect a wall then move.
        if (!hitWall)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y) + pPos;
            xIndex = (int)transform.position.x;
            yIndex = (int)transform.position.y;
            LightArea();
        }

    }

    private void MoveCharacter()
    {
        targetPos = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.D))
        {

            animator.SetTrigger("walkRight");

            left = up = down = false;
            right = true;

            GetComponent<SpriteRenderer>().flipX = false;

            targetPos += Vector2.right;
            //xIndex++;

        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            
            animator.SetTrigger("walkRight");

            right = up = down = false;
            left = true;

            GetComponent<SpriteRenderer>().flipX = true;


            targetPos += Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {

            right = left = down = false;
            up = true;

            targetPos += Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {

            right = left = up = false;
            down = true;

            targetPos += Vector2.down;
        }

        CheckAndMove(targetPos);

    }
}
