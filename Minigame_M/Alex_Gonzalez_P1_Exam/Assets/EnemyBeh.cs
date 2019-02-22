using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeh : MonoBehaviour
{
    Rigidbody rb;
    bool block;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(3f, 0, 0);
        block = transform.position.x < 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!block)
        {
            if (Mathf.Abs(transform.position.x)>15)
            {
                rb.velocity *= -1;
                block = true;
            }
        }
        if (Mathf.Abs(transform.position.x) < .2)
        {
            block = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
