using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Attacks players when they get to close
/// </summary>
public class ScriptSkeleton : MonoBehaviour {

    public int xIndex;
    public int yIndex;

    public bool isDead = false;

    private Animator animator;
    private GameObject player;
    private SpriteRenderer sprite;
    private Health scriptHealth;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        scriptHealth = GetComponent<Health>();

        StartCoroutine("CheckForPlayer");
	}

    public void Activate()
    {
        InvokeRepeating("Attack", 0, 3);
    }

    void Attack()
    {
        if(player.transform.position.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        animator.SetTrigger("Attack");

        //hurt player.
        player.GetComponentInChildren<Health>().Damage(25);
        
    }
    IEnumerator CheckForPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!isDead)
            {
                if(player != null &&
                    Vector2.Distance(transform.position, player.transform.position) < 2)
                {
                    Activate();
                }
                else
                {
                    CancelInvoke("Attack");
                }
            }
            else//is dead
            {
                StopCoroutine("CheckForPlayer");
                CancelInvoke("Attack");
                break;
            }
            yield return null;
        }
    }
}
