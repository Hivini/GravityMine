using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBeh : MonoBehaviour
{
    public GameObject bullet, enemy1, enemy2;
    List<GameObject> en = new List<GameObject>();
    GameObject b, e;
    Rigidbody rb;
    public int level, lives;
    Transform t;
    float[] radios = new float[] { 10.5f, -10.5f, -13.5f, 13.5f, 5.6f, 20.3f, 2.2f, 17f, -5.6f, -20.3f, -2.2f, -17f };
    float w, x, y, r, vr;
    int i, waitForShoot;
    public float radialSpeed, radialAccel, maxAngularVel;
    float angularVel, radialVel;
    bool block;
    public Text text;
    int fibon, fibon1, rr;
    int sizeEnemies;
    System.Random rand = new System.Random();


    void Start()
    {
        sizeEnemies = 2;
        fibon = 1;
        fibon1=1;

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
        e= Instantiate(enemy2, new Vector3(radios[3],0,0),Quaternion.identity);
        SetEnemy2(e, 7f,3f);
        e = Instantiate(enemy1, new Vector3(radios[0], 0, 0), Quaternion.identity);
        SetEnemy1(e, 7f, 3f);
        //this.sizeEnemies = 2;

    }

    void Update()
    {
        print(sizeEnemies);
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
    public void enemyDestroyed(GameObject e)
    {
        print(sizeEnemies);
        this.sizeEnemies--;
        print(sizeEnemies);
        if (sizeEnemies == 0)
        {
            level++;
            for (int i=0; i<fibon;i++)
            {
                rr = rand.Next(11);
                e = Instantiate(enemy1, new Vector3(radios[rr], 0, 0), Quaternion.identity);
                SetEnemy1(e, 7f+level, 3f+(level/6f));
                sizeEnemies++;
            }
            int aux;
            aux = fibon + fibon1;
            fibon1 = fibon;
            fibon = aux;
            
            for (int j = 0; j < level; j++)
            {
                rr = rand.Next(11);
                for (int i=0;i<radios.Length ;i++)
                {
                    print(radios[i]);
                }
                e = Instantiate(enemy2, new Vector3(radios[rr], 0, 0), Quaternion.identity);
                SetEnemy2(e, 7f+level, 3f+(level / 6f));
                sizeEnemies++;
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