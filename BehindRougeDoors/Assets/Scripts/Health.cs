using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health = 100;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Damage(int amount)
    {
        health -= amount;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if(health <= 0)
        {
            animator.SetBool("Dead", true);
        }
    }
}
