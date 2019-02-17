using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Beh : MonoBehaviour
{

    public GameObject player, bullet;
    public float frecuencyOfShooting;

    GameObject b;
    Rigidbody rb;
    Transform t, tf;
    float x, y, r, radius, angularVel, speedEnemy, radialFrec, radialSpeed;
    int level;
    
    private Coroutine coroutine;
    

    void Start()
    {  
            
    }


    void Update()
    {
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        radialSpeed += radialFrec * (radius - r);
        rb.velocity = new Vector3(angularVel * (y / r) + radialSpeed * (x / r),
                                    radialSpeed * (y / r) - angularVel * (x / r), 0);
    }


    public void SetEnemy2(float speedEnemy, float frecuencyOfShooting)
    {
        this.frecuencyOfShooting = frecuencyOfShooting;
        this.speedEnemy = speedEnemy;
        radialFrec = 0.5f;
        radialSpeed = 20f;
        radius = Vector3.Magnitude(this.transform.position);
        angularVel = 1.2f * speedEnemy;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        coroutine = StartCoroutine("Shoot");
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            // this.t.position + new Vector3(1f * (y / r), -1f * (x / r),0)
            x = t.position.x;
            y = t.position.y;
            r = Mathf.Sqrt(x * x + y * y);
            Vector3 pos = new Vector3(x - 1f * (x / r), y - 1f * (y / r), 0);
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
                myScriptReference.SetBullet(radialSpeed * 2f, angularVel > 0, true, false);
            }
        }
    }
}
