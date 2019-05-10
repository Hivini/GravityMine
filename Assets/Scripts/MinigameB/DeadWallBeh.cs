using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWallBeh : MonoBehaviour
{
    Transform ot, t;
    Rigidbody orb;
    System.Random r = new System.Random();
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        t = this.GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animator.SetBool("Hit", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animator.SetBool("Hit", false);
        }
    }
}
