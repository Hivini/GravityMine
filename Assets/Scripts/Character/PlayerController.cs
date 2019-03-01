using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody rigidbody;
    private Animator animator;
    private Collider[] groundCollisions;
    private bool grounded;
    private bool facingRight;

    private const float groundCheckRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        // The player will always start facing right
        facingRight = true;
        grounded = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Maybe change this to Fixed Update ?
        // Draw a tiny sphere and check what is colliding
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        animator.SetBool("grounded", grounded);

        // Jump if its on the ground
        if (grounded && Input.GetButton("Jump"))
        {
            grounded = false;
            animator.SetBool("grounded", grounded);
            // Do the jump
            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }

        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(h));

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, speed * h);

        if (h > 0 && !facingRight)
        {
            // Changing to right
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            // Changing to left
            Flip();
        }
    }

    private void FixedUpdate()
    {

    }

    private void Flip()
    {
        // Just invert the value
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }
}
