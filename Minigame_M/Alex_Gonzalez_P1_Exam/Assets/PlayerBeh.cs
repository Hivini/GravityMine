using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBeh : MonoBehaviour
{
    public GameObject bullet, enemy;
    GameObject b;
    float x, y, z;
    Rigidbody rb;
    Random r;
    int j;
    Transform t;
    public Text text;

    public float speed;
    float lives;
    // Start is called before the first frame update
    void Start()
    {
        r = new Random();
        lives = 5;
         speed = 15f;
        rb = this.GetComponent<Rigidbody>();
        t= this.GetComponent<Transform>();
        j = 0;
        text.text = "Lives: "+lives;

    }

    // Update is called once per frame
    void Update()
    {
       
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rb.velocity = speed * (new Vector3(x, y, 0));

        if (Input.GetKey(KeyCode.Space))
        {
            x=t.position.x;
            y=t.position.y;
            z = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * (180 / Mathf.PI);
            if (z < 0) z += 360f;
            print(z);
            Vector3 pw = new Vector3(x, y, z);
            b = Instantiate(bullet, pw+new Vector3(.5f*Mathf.Cos(z),Mathf.Sin(z)*.5f,0), this.transform.rotation);
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (j >5) {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 p = new Vector3(Random.value, Random.value,0 ) * 10;
                    Instantiate(enemy, p, this.transform.rotation);
                }
                j = 0;
            }
            
        }
        j++;
        text.text = "Lives: "+lives;

    }
    //

}
