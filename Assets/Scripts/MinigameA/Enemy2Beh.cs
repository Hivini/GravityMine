using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Beh : MonoBehaviour
{

    GameObject player;
    public GameObject bullet;
    public float frecuencyOfShooting;

    GameObject b;
    Rigidbody rb;
    Transform t, tf;
    float x, y, r, radius, angularVel, speedEnemy, radialFrec, radialSpeed;
    int level;
    bool block;

    private Coroutine coroutine;
    

    void Update()
    {
        player = GameObject.Find("Planeta");
        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);
        radialSpeed += radialFrec * (radius - r);
        rb.velocity = new Vector3(angularVel * (y / r) + radialSpeed * (x / r),
                                    radialSpeed * (y / r) - angularVel * (x / r), 0);
    }


    public void SetEnemy2(float speedEnemy, float frecuencyOfShooting)
    {
        block = false;
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
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.tag.Equals("Player") || collision.collider.tag.Equals("BulletFriend"))&&(!block))
        {
            block = true;
            IVeDestroyed(gameObject, player);
            Destroy(gameObject);
        }
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
    public void IVeDestroyed(GameObject e, GameObject p)
    {
        if (e != null && p != null)
        {
            var myScriptReference = p.GetComponent<PlayerBeh>();
            if (myScriptReference != null)
            {
                myScriptReference.enemyDestroyed(e);
            }
        }
    }
}
