using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharaMovment : MonoBehaviour
{
    public int speed=3;
    public float power;
    float h, v,u;
    public Rigidbody rb;
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        power = 3;
        rb= GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
}
    
    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        u = Input.GetAxis("Jump");
        transform.Translate(speed * Time.deltaTime*h,0, speed * Time.deltaTime * v, Space.World);
        //print(u);
       
        
    }
    public void OnCollisionStay(Collision collision)
    {
        //print(collision.transform.name);
        if (u!=0)
        {
            rb.AddForce(0, power * u, 0, ForceMode.Impulse);
        }

    }

}

