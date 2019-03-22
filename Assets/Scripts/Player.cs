using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10;
    public float speed=5;
    public float angularspeed=0.03f;

    private bool lastJ;
    private Rigidbody rigidbody;
    float angleRad, angleDeg;
    Vector3 centro;
    public float AngleRad { get => angleRad; set => angleRad = value; }

    // Start is called before the first frame update
    void Start()
    {
        //provisional
        centro = new Vector3(0, 1f / Mathf.Tan(Mathf.PI / (25)), 0); //provisional
        rigidbody = GetComponent<Rigidbody>();
        lastJ = false;
        angleDeg = -90f;
        AngleRad = (Mathf.PI / 180f) * angleDeg;
    }

    // Update is called once per frame
    void Update()
    {
        float changeOfangle = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        AngleRad = Mathf.Atan2(gameObject.transform.position.y-centro.y, gameObject.transform.position.x-centro.x);

        rigidbody.velocity = new Vector3(rigidbody.velocity.x * (Mathf.Abs(Mathf.Cos(AngleRad))) - angularspeed * changeOfangle * Mathf.Sin(AngleRad),
                                         rigidbody.velocity.y * (Mathf.Abs(Mathf.Sin(AngleRad))) + angularspeed * changeOfangle*Mathf.Cos(AngleRad),
                                         speed*z);


        AngleRad = Mathf.Atan2(gameObject.transform.position.y - centro.y, gameObject.transform.position.x - centro.x);
        rigidbody.AddForce(9.8f*Mathf.Cos(angleRad),9.8f*Mathf.Sin(angleRad),0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Reset the velocity to not affect the force
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(jumpForce*(new Vector3(-Mathf.Cos(AngleRad), -Mathf.Sin(AngleRad), 0)));
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
