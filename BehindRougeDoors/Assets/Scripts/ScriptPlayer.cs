using UnityEngine;
using System.Collections.Generic;
using System;

public class ScriptPlayer : MonoBehaviour {

    public int lightRadius = 2;
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
    }

    //void FixedUpdate()
    //{
        
    //}

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
            animator.SetTrigger("walkRight");
            right = left = down = false;
            up = true;

            targetPos += Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("walkRight");
            right = left = up = false;
            down = true;

            targetPos += Vector2.down;
        }

        CheckAndMove(targetPos);

    }
}
