using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Beh : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, bullet;
    Rigidbody rb;
    Transform t, tf;
    float xr, yr, rr, radius;
    int level;
    float angularVel, speedEnemy, radialFrec, radialSpeed;
   // private Coroutine coroutine;

    void Start()
    {
        //coroutine = StartCoroutine("shooting");
        speedEnemy = 7f;
        level = 1;

        angularVel = level * speedEnemy;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        xr = t.position.x; //- tf.position.x;
        yr = t.position.y; //- tf.position.y;
        radius = Mathf.Sqrt(xr * xr + yr * yr);
        radialFrec = 0.5f;
        radialSpeed = 20f;
    }

    void Update()
    {
        xr = t.position.x; //- tf.position.x;
        yr = t.position.y; //- tf.position.y;
        rr = Mathf.Sqrt(xr * xr + yr * yr);
        radialSpeed += radialFrec * (radius - rr);
        rb.velocity = new Vector3(angularVel * (yr / rr) + radialSpeed * (xr / rr), radialSpeed * (yr / rr) - angularVel * (xr / rr), 0);
    }
}
