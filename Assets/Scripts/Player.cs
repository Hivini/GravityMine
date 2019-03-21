using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tubeReference;
    public float jumpForce=200;
    public float speed=5;
    public float speedRadial=3;
    public float rotationSpeed = 15;
    public float playerGravity = 5;

    private bool lastJ;
    private Rigidbody rigidbody;
    float angleRad, angleDeg;

    public float AngleRad { get => angleRad; set => angleRad = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastJ = false;
        angleDeg = -90f;
        AngleRad = (Mathf.PI / 180f) * angleDeg;
    }

    // Update is called once per frame
    void Update()
    {
        float changeOfangle = Input.GetAxis("Horizontal");
        angleDeg += speedRadial*changeOfangle;
        AngleRad = (Mathf.PI / 180f) * angleDeg;
        float z = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * z);
        rigidbody.AddForce(playerGravity * new Vector3(0, -1, 0), ForceMode.Acceleration);
        //rigidbody.AddForce(9.8f*(new Vector3(Mathf.Cos(AngleRad), Mathf.Sin(AngleRad), 0)), ForceMode.Acceleration);
        tubeReference.transform.Rotate(0, 0, changeOfangle * Time.deltaTime * rotationSpeed, Space.World);
        //tubeReference.transform.rotation = new Quaternion(0, 0, tubeReference.transform.rotation.z - changeOfangle, 1);
        //transform.Translate(new Vector3(x * Time.deltaTime * speed, transform.position.y, z * Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Reset the velocity to not affect the force
            // TODO Fix some jump bugs that are present
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(0, jumpForce, 0);
            //rigidbody.AddForce(jumpForce*(new Vector3(-Mathf.Cos(AngleRad), -Mathf.Sin(AngleRad), 0)));
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
