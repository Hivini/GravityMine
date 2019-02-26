using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBeh : MonoBehaviour
{
    public GameObject bullet, enemy1, enemy2;
    GameObject b, e1,e2;
    Rigidbody rb;
    public int level, lives;
    Transform t;
    float w, x, y, r, vr;
    int i, waitForShoot;
    public float radialSpeed, radialAccel, maxAngularVel;
    float angularVel, radialVel;
    bool block;
    public Text text;


    void Start()
    {
       
        level = 1;
        lives = 10;
        text.text = "Level:    " + level + "     Lives: " + lives;

        // Controller mechanics parameters.
        
        angularVel = 1f;
        radialVel = 0;
        radialSpeed = 10f;
        maxAngularVel = 20f;
        radialAccel = 0.3f;

        // frecuency of shooting
        waitForShoot = 30;
        i = 0;// counter for wait

        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();

        block = false;
        e2= Instantiate(enemy2, new Vector3(-13.73f,0,0),Quaternion.identity);
        SetEnemy2(e2, 7f,3f);
        e1 = Instantiate(enemy1, new Vector3(-10.68f, 0, 0), Quaternion.identity);
        SetEnemy1(e1, 7f, 3f);


    }

    void Update()
    {
        w = Input.GetAxis("Horizontal");
        vr = Input.GetAxis("Vertical");
        radialVel = radialSpeed * vr;
        angularVel += w * radialAccel;
        angularVel = Mathf.Abs(angularVel) > maxAngularVel ? maxAngularVel * Mathf.Sign(angularVel) : angularVel;

        x = t.position.x;
        y = t.position.y;
        r = Mathf.Sqrt(x * x + y * y);

        if (r>24)// Player can't go out of the screen.
            rb.velocity = new Vector3(angularVel * (y / r) + (radialVel>0?0:radialVel) * (x / r),
                (radialVel > 0 ? 0 : radialVel) * (y / r) - angularVel * (x / r), 0);
        else
            rb.velocity = new Vector3(angularVel * (y / r) + radialVel * (x / r), radialVel * (y / r) - angularVel * (x / r), 0);
       
        if (Input.GetKey(KeyCode.Space) &&(!block))//Shoot
        {
            
            b = Instantiate(bullet, this.t.position + new Vector3(1f * (y / r)* (angularVel > 0 ? 1 : -1),
                -1f* (angularVel > 0 ? 1 : -1) * (x / r), 0), Quaternion.identity);
            SetBullet(b);
            block = true;
            i = 0;
        }
        i += 1;
        if (i == waitForShoot)// Unlocks shooting after a certain number of frames.
        {
            i = 0;
            block = false;
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.tag.Equals( "BulletFriend"))// you can't shoot yourself hahahahaha

        {
            lives--;
            text.text = "Level:    " + level + "     Lives: " + lives;
        }
    }
    public void SetBullet(GameObject e)
    {
        if (e != null)
        {
            var myScriptReference = e.GetComponent<BulletBeh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetBullet(radialSpeed * 2.5f, angularVel > 0, false,false);
            }
        }
    }

    public void SetEnemy2(GameObject e, float speedEnemy, float frecuencyOfShooting)
    { 
        if (e != null)
        {
            var myScriptReference = e.GetComponent<Enemy2Beh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetEnemy2( speedEnemy,  frecuencyOfShooting);
            }
        }
    }

    public void SetEnemy1(GameObject e, float speedEnemy, float frecuencyOfShooting)
    {
        if (e != null)
        {
            var myScriptReference = e.GetComponent<Enemy1Beh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetEnemy1(speedEnemy, frecuencyOfShooting);
            }
        }
    }
}