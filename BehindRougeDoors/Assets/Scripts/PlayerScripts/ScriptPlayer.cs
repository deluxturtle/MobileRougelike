using UnityEngine;
using System.Collections.Generic;
using System;

public class ScriptPlayer : MonoBehaviour {

    public int xIndex = 3;
    public int yIndex = 3;
    public int lightRadius = 2;
    [Tooltip("Place the projectile graphic here.")]
    public GameObject orb;
    [Tooltip("Place the shield graphic here.")]
    public GameObject shieldCanvas;

    bool left, right, up, down;
    Animator animator;
    bool canFire = true;
    float orbSpeed = 20f;

    List<GameObject> curLitTiles;

    Vector2 targetPos;

    [Tooltip("How long the swipe must be to be registered as a \"Swipe\"")]
    public int minSwipeDistance = 3;
    private Vector2 swipeStartPos;
    private Vector2 swipeCurrentPos;
    private float swipeMagnitude;


    // Use this for initialization
    void Start () {
        LightArea();

        left = right = up = false;
        down = true;
        animator = GetComponent<Animator>();
        
        //Initialize 
        xIndex = (int)transform.position.x;
        yIndex = (int)transform.position.y;

        LightArea();
    }

    #region LightArea
    void ClearRange()
    {
        foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if(tile.GetComponentInChildren<Tile>() != null)
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
                if(tile != null && tile.GetComponent<Tile>()!= null && tile.GetComponent<Tile>().tempRange == range)
                {
                    if(range < pRange)
                    {
                        Tile tempTile = tile.GetComponent<Tile>();
                        //add their neighbors to the list
                        foreach(GameObject neighborTile in tile.GetComponent<Tile>().neighbors)
                        {
                            if (!tempList.Contains(neighborTile) && !tempTile.blocksLight)
                            {
                                //Vector2 direction = (transform.position - neighborTile.transform.position);
                                //if(!Physics2D.Raycast(transform.position, direction, lightRadius,))
                                //{
                                    
                                    bool belowLightBlock = false;
                                    foreach(GameObject below in neighborTile.GetComponent<Tile>().topTiles)
                                    {
                                        if (below.GetComponent<Tile>().blocksLight)
                                        {
                                            belowLightBlock = true;
                                            break;
                                        }
                                    }
                                    if (!belowLightBlock)
                                    {
                                        neighborTile.GetComponent<Tile>().tempRange = range + 1;
                                        tempList.Add(neighborTile);
                                    }
                                //}
                            }
                        }
                    }
                }
            }
        }
        return tempList;
    }


    void LightArea()
    {

        curLitTiles = FindTiles(lightRadius);

        foreach(GameObject tile in curLitTiles)
        {
            tile.GetComponent<Tile>().LightTile();
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        string direction = "";
//#if UNITY_EDITOR
//        Debug.DrawRay(transform.position, targetPos.normalized, Color.red);
//        if (Input.GetButtonDown("Fire1"))
//        {
//            animator.SetTrigger("blast");
//            Invoke("SpawnOrb", 0.5f);
//        }
//#elif MOBILE_INPUT
        //if(Input.touchCount > 0)
        //{
        //    foreach(Touch touch in Input.touches)
        //    {
        //        if(touch.phase == TouchPhase.Began)
        //        {
        //            SpawnOrb();
        //        }
        //    }
        //}

        	    //If only 1 touch went down
        if(Input.touchCount == 1 && GetComponent<Health>().health > 0)
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
                            direction = "left";
                        }
                        else
                        {
                            //print("Down");
                            direction = "down";
                        }
                    }
                    //else we are swiping right or up.
                    else
                    {
                        if (Mathf.Abs(swipeCurrentPos.x - swipeStartPos.x) > Mathf.Abs(swipeCurrentPos.y - swipeStartPos.y))
                        {
                            //print("Right");
                            direction = "right";
                        }
                        else
                        {
                            //print("Up");
                            direction = "up";
                        }
                    }
                }
                else
                {
                    if (canFire)
                    {
                        canFire = false;
                        animator.SetTrigger("blast");
                        Invoke("SpawnOrb", 0.5f);
                    }
                }

            }
        }

//#endif
        if(GetComponent<Health>().health > 0)
            MoveCharacter(direction);
    }



    void SpawnOrb()
    {
        Vector2 orbOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject orbInstance = (GameObject)Instantiate(orb, orbOrigin, Quaternion.identity);
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
        canFire = true;
    }

    void CheckAndMove(Vector2 pPos)
    {
        bool hitWall = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, pPos.normalized, 1f);
        for(int i = 0; i < hit.Length; i++)
        {
            GameObject obj = hit[i].collider.gameObject;
            if (obj.GetComponent<SpriteRenderer>().sortingLayerName == "Obstacles")
            {
                //Debug.Log("Hit wall");
                hitWall = true;
                break;
            }
            if(obj.GetComponent<SpriteRenderer>().sortingLayerName == "Interactive")
            {
                if(obj.GetComponentInChildren<Door>() != null)
                {
                    Debug.Log("Hit Door");
                    obj.GetComponent<Door>().OpenDoor();
                    hitWall = true;
                    break;
                }
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

    private void MoveCharacter(string pInputDirection)
    {
        targetPos = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || pInputDirection == "right")
        {

            animator.SetTrigger("walkRight");

            left = up = down = false;
            right = true;

            GetComponent<SpriteRenderer>().flipX = false;

            targetPos += Vector2.right;
            //xIndex++;

        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || pInputDirection == "left")
        {
            
            animator.SetTrigger("walkRight");

            right = up = down = false;
            left = true;

            GetComponent<SpriteRenderer>().flipX = true;


            targetPos += Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || pInputDirection == "up")
        {
            animator.SetTrigger("walkRight");
            right = left = down = false;
            up = true;

            targetPos += Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || pInputDirection == "down")
        {
            animator.SetTrigger("walkRight");
            right = left = up = false;
            down = true;

            targetPos += Vector2.down;
        }

        CheckAndMove(targetPos);

    }
}
