using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = 300f;
    public float smoothTime;

    private bool isOnGround;
    private bool isOnPlatform;
    private Rigidbody2D rigidbody;
    private Vector2 velocity = Vector2.zero;

    void Awake()
    {
        // Get the rigidbody from the entity
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(float horizontalSpeed, bool gonnaJump)
    {
        // Move horizontally
        // Move the character by finding the target velocity
        Vector2 targetVelocity = new Vector2(horizontalSpeed * 10f, rigidbody.velocity.y);
        // And then smoothing it out and applying it to the character
        // It basically looks better
        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, smoothTime);

        // Jump
        if (isOnGround && gonnaJump)
        {
            isOnGround = false;
            rigidbody.AddForce(new Vector2(0f, jumpForce));
        }
        // Limit the jump, might be changed
        if (!isOnGround)
        {
            // Clamp limits the lower and upper boundries
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y - 0.3f, -10, 10));
        }
    }

    // TODO This two collision might change based on the need of the project
    private void OnCollisionStay2D(Collision2D collision)
    {
        // If is in collision with the ground it will be set to true
        isOnGround |= collision.gameObject.tag == "ground";

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If we exit the ground 
        isOnGround &= collision.gameObject.tag != "ground";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
