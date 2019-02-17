using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Beh : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player, bullet;
    GameObject b;
    Rigidbody rb;
    Transform t;
    float x, y, r;
    int level;
    float angularVel, frecuencyOfShooting;
    private Coroutine coroutine;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();

    }


    public void SetEnemy1(float speedEnemy, float frecuencyOfShooting)
    {
        this.frecuencyOfShooting = frecuencyOfShooting;
        this.angularVel = speedEnemy;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        coroutine = StartCoroutine("Shoot");
    }


    void Update()
    {
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        rb.velocity = new Vector3(angularVel * (y / r), -angularVel * (x / r), 0);
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            x = t.position.x;
            y = t.position.y;
            r = Mathf.Sqrt(x * x + y * y);
            Vector3 pos = new Vector3(x - 1f * (y / r), y - 1f * (x / r), 0);
            b = Instantiate(bullet, pos, Quaternion.identity);
            SetBullet(b);
            yield return new WaitForSeconds(frecuencyOfShooting);
        }

    }


    public void SetBullet(GameObject e)
    {
        if (e != null)
        {
            var myScriptReference = e.GetComponent<BulletBeh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetBullet(angularVel * 2f, false, true, true);
            }
        }
    }
}
