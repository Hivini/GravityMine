using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;

    private bool lastJ;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastJ = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(speed * x, rigidbody.velocity.y, speed * z);
        //transform.Translate(new Vector3(x * Time.deltaTime * speed, transform.position.y, z * Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Reset the velocity to not affect the force
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "DestructablePlatform")
        {
            Destroy(collision.gameObject);
        }
    }
}
