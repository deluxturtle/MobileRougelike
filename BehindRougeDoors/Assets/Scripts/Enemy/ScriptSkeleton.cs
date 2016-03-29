using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Attacks players when they get to close
/// </summary>
public class ScriptSkeleton : MonoBehaviour {

    public int xIndex;
    public int yIndex;

    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Activate()
    {
        InvokeRepeating("Attack", 0, 3);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

    }
}
