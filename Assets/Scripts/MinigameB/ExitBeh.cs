using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBeh : MonoBehaviour
{
    Transform ot,t ;
    Rigidbody orb;
    System.Random r = new System.Random();

    void Start()
    {
        t=this.GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            ot = other.GetComponent<Transform>();
            orb = other.GetComponent<Rigidbody>();
            ot.SetPositionAndRotation(new Vector3(-7f, -2.5f, 0), new Quaternion(0, 0, 0, 0));
            t.SetPositionAndRotation(new Vector3( 3.1f * ((r.Next(20000) / 10000f) - 1), 3.1f * ((r.Next(20000) / 10000f) - 1),0), new Quaternion(0, 0, 0, 0));
            orb.velocity = new Vector3(0, 0, 0);
        }
    }
}
