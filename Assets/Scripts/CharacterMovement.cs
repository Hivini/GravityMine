using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 15f;
    public float jump = 3.5f;
    // This variable might change
    public GameObject level;

    // Reference of the RigidBody2D of the object
    private bool isOnGround;
    private new Rigidbody2D rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCharacter();
        MoveCharacter2();
    }

    void FixedUpdate()
    {
               
    }

    private void MoveCharacter()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Jump");

        if (isOnGround && verticalMovement > 0)
        {
            print("Jump");
            isOnGround = false;
        }
        else
        {
            transform.Translate((horizontalMovement * speed) * Time.deltaTime, 0, 0);
        }


    }

    private void MoveCharacter2()
    {
        // FIXME If we jump in a corner the character will go to the space.

        // Horizontal movement
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(h, 0);
        // We add a force creating a vector with the value obtained from variable h.
        rigidbody.AddForce(movement * speed);

        // Checking for jump
        if (isOnGround && Input.GetAxis("Jump") > 0)
        {
            print("Jump");
            isOnGround = false;
            rigidbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isOnGround |= collision.gameObject.Equals(level);
    }
}