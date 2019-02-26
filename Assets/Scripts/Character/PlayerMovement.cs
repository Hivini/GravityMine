using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody rb;
    private bool onGround;
    private bool secondJump;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        onGround = false;
        secondJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(0, 0, z * Time.deltaTime * speed);
        
        if (onGround && y > 0)
        {
            rb.AddForce(0, jumpForce, 0);
            onGround = false;
            secondJump = true;

        }
        else if (!onGround && y > 0)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            secondJump = false;
        }
    }
}
