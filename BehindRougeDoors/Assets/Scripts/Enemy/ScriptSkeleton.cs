using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Attacks players when they get to close
/// </summary>
public class ScriptSkeleton : MonoBehaviour {

    public int xIndex;
    public int yIndex;
    

    bool activated = false;

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
        activated = true;
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
        animator.SetTrigger("attack");

        //hurt player.
        Invoke("HitPlayer", 1);
    }

    void HitPlayer()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < 2)
        {
            player.GetComponentInChildren<Health>().Damage(25);
        }
    }

    IEnumerator CheckForPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (scriptHealth.health > 0)
            {
                if(player != null &&
                    Vector2.Distance(transform.position, player.transform.position) < 2 &&
                    !activated &&
                    player.GetComponent<Health>().health > 0)
                {
                        Activate();
                }
                else
                {
                    CancelInvoke("Attack");
                    activated = false;
                }
            }
            else//I am dead dead
            {
                StopCoroutine("CheckForPlayer");
                CancelInvoke("Attack");
                activated = false;
                break;
            }
            yield return null;
        }
    }
}
