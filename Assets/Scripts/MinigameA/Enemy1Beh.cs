using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Beh : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject bullet;
    GameObject b;
    Rigidbody rb;
    Transform t;
    float x, y, r;
    int level;
    float angularVel, frecuencyOfShooting;
    private Coroutine coroutine;
    bool block;
    void Start()
    {
        block = false;
        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();

    }


    public void SetEnemy1(float speedEnemy, float frecuencyOfShooting)
    {
        player = GameObject.Find("Planeta");
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
            x = t.position.x;
            y = t.position.y;
            r = Mathf.Sqrt(x * x + y * y);
            Vector3 pos = new Vector3(x - 1f * (y / r), y - 1f * (x / r), 0);
            b = Instantiate(bullet, pos, Quaternion.identity);
            SetBullet(b);
            yield return new WaitForSeconds(frecuencyOfShooting);
        }

    }


    public void SetBullet(GameObject b)
    {
        if (b != null)
        {
            var myScriptReference = b.GetComponent<BulletBeh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetBullet(angularVel * 3f, false, true, true);
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

