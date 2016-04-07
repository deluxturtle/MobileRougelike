using UnityEngine;
using System.Collections;

public class ScriptTower : MonoBehaviour {

    public float shootRate = 5f;
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed;


    private GameObject player;
    private Vector2 direction;
	
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("ShootAtPlayer", 0, shootRate);
    }

    //Raycasts and if the 
    void ShootAtPlayer()
    {
        direction = (player.transform.position - transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 10f);
        for(int i = 0; i < hits.Length; i++)
        {
            //Check to see if you hit a wall first.
            if(hits[i].collider.GetComponent<SpriteRenderer>().sortingLayerName == "Obstacle")
            {
                Debug.Log("hit wall first.");
                i = hits.Length;
            }
            if (hits[i].collider.tag == "Player")
            {
                SpawnOrb();
            }
        }
    }

    void SpawnOrb()
    {
        Vector2 orbOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
        GameObject orbInstance = (GameObject)Instantiate(projectilePrefab, orbOrigin, Quaternion.identity);
        Rigidbody2D orbRigid = orbInstance.GetComponent<Rigidbody2D>();

        orbRigid.velocity = direction.normalized;
    }

    public void Death()
    {
        CancelInvoke("ShootAtPlayer");
    }
}
