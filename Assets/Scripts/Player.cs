using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float speedRadial=1;

    private bool lastJ;
    private Rigidbody rigidbody;
    float angleRad, angleDeg;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastJ = false;
        angleDeg = -90f;
        angleRad = (Mathf.PI / 180f) * angleDeg;
    }

    // Update is called once per frame
    void Update()
    {
        float changeOfangle = Input.GetAxis("Horizontal");
        angleDeg += speedRadial*changeOfangle;
        angleRad = (Mathf.PI / 180f) * angleDeg;
        float z = Input.GetAxis("Vertical");

        rigidbody.velocity =new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * z);
        rigidbody.AddForce(9.8f*(new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0)), ForceMode.Acceleration);
        //transform.Translate(new Vector3(x * Time.deltaTime * speed, transform.position.y, z * Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Reset the velocity to not affect the force
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(jumpForce*(new Vector3(-Mathf.Cos(angleRad), -Mathf.Sin(angleRad), 0)));
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
