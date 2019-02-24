using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeh : MonoBehaviour
{
    public float thickness;

    float speed, gForce;
    Rigidbody rb;
    Transform t;
    int status,i, lives, level;
    bool block;
    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        i= 0;
        thickness = 2.0f;
        status = -1;
        speed = 1.2f;
        gForce = 3.2f;
        block = false;
        lives = 5;
        level = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        int h = (int)Input.GetAxisRaw("Horizontal");
        if (!block)
        {
            status += h;
            status = status % 4;
            block=true;
            i= 0;
        }
        
        if (status!=-1)
        {
            bool sign = (status ==1 || status == 2);
            bool isVertical = (status % 2 == 0);
            if (h!=0)
            {
                
                rb.velocity = new Vector3(
                        (isVertical ? 0 : (speed * (sign ? 1 : -1)) ),
                        (isVertical ? (speed * (sign ? 1 : -1)) : 0 ), 0) ;
            }

            rb.AddForce((isVertical ? 0: (gForce* (sign ? 1 : -1)) ),(isVertical? (gForce * (sign ? 1 : -1)) : 0 ),0,ForceMode.Acceleration);
        }
        if (i==7)
        {
            block = false;
            i= 0;
        }
        i++;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            level++;
            print("level: " + level);
        }
        else if (other.tag == "DeadWall")
        {
            lives--;
            print("lives: " + lives);
        }
    }

}