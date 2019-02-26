using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public GameObject bullet;

    private bool lastJ;
    private bool fired;


    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastJ = false;
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float fire = Input.GetAxisRaw("Jump");

        transform.Translate(new Vector3(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed), Space.World);


        if (!fired && fire > 0)
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
        fired = fire > 0;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!lastJ && collision.gameObject.tag == "Ground")
        {
            lastJ = true;
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        lastJ = false;
    }
}
