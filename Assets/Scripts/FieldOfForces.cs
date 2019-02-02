using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfForces : MonoBehaviour
{
    Rigidbody rb;
    public float fieldPower;
    private Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        fieldPower = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("FColliding with " + collision.transform.name);
    }

    // This methods is invoked during the collision
    // When they stop touching
    private void OnCollisionExit(Collision collision)
    {
        print("FExit");
    }


    private void OnTriggerEnter(Collider other)
    {
        print("FTrigger Enter");

    }

    private void OnTriggerStay(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        rb.AddForce(0.2f * fieldPower * t.position.z, 0.5f * fieldPower * t.position.x, 0.2f * fieldPower * t.position.x, ForceMode.Acceleration);
    }

    private void OnTriggerExit(Collider other)
    {
        print("FTrigger exit");
    }

    private void OnCollisionStay(Collision other)
    {

    }

}
