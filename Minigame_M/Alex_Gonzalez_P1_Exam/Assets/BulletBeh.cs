using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBeh : MonoBehaviour

{
    public float power;
    Rigidbody rb, rbp;
    GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        float angle = this.transform.position.z;
        transform.position=new Vector3(this.transform.position.x, this.transform.position.y, 0);
        rb = this.GetComponent<Rigidbody>();
        Vector3 s = new Vector3(Mathf.Cos(Mathf.PI*angle/180),Mathf.Sin(Mathf.PI * angle / 180),0);

        power = 20f;
        rb.AddForce(power *s , ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
