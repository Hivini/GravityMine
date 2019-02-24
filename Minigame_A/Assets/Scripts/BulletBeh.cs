using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBeh : MonoBehaviour
{
    bool isEnemy;
    Rigidbody rb;
    Transform t;
    float x, y,r;
    public float Gm;
    // Start is called before the first frame update
    void Start()
    {
        isEnemy = false;
        Gm = 0.5f;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        if (Mathf.Abs(t.position.x)>50 || Mathf.Abs(t.position.y) > 50)
        {
            Destroy(gameObject);
        }
        rb.AddForce(-Gm*x/r*r*r, -Gm * y / r * r * r, 0,ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
                Destroy(gameObject);
    }

    public void SetBullet(float speed, bool direction, bool isEnemy, bool isRadial)
    {
        this.isEnemy = isEnemy;
        this.tag = isEnemy ? "BulletEnemy" : "BulletFriend";
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        if(!isRadial)
        {
            rb.velocity = new Vector3((direction ? 1 : -1) * speed * (y / r), -1f * (direction ? 1 : -1) * speed * (x / r), 0);
        }
        else
        {
            rb.velocity = new Vector3( speed * (x / r),   speed * (y / r), 0);
        }

    }
}
