using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Beh : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, bullet;
    Rigidbody rb;
    Transform t;
    float x, y, r;
    int level;
    float angularVel, speedEnemy;
    //private Coroutine coroutine;

    void Start()
    {
       // coroutine = StartCoroutine("shooting");
        speedEnemy = 5f;
        level = 1;

        angularVel = level * speedEnemy;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();

    }

    void Update()
    {
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        rb.velocity = new Vector3(angularVel * (y / r), -angularVel * (x / r), 0);
    }
}
