﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBeh : MonoBehaviour
{
    public GameObject bullet, enemy1, enemy2;
    public AudioClip hit, shoot, enemySound;
    private AudioSource audioSource;
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
    Queue<GameObject> enemies = new Queue<GameObject>();
    private bool ended = false;
    public GameMenu gameMenu;
    string sceneName;
    Animator animator;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        animator = GetComponent<Animator>();
        fibon = 1;
        fibon1=1;

        level = 1;
        lives = 5;
        text.text = "Level:    " + level + "     Lives: " + lives;
        // Controller mechanics parameters.
        
        angularVel = 1f;
        radialVel = 0;
        radialSpeed = 10f;
        maxAngularVel = 30f;
        radialAccel = 0.3f;

        // frecuency of shooting
        waitForShoot = 35;
        i = 0;// counter for wait

        rb = this.GetComponent<Rigidbody>();
        t = this.GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();

        block = false;
        startEnemiesL1();

        Time.timeScale = 0;
        gameMenu.setStartInstructions("Get to the highest level as posible\n**Click to start playing**");
        gameMenu.setEndInstructions("End Game\n** Click anywhere to retry **\n** Click on exit to return to game selection**");
        gameMenu.setBestScoreText("Current best score is: \nLevel " + GameControl.control[sceneName]);
        gameMenu.startDisplay();
    }
    void startEnemiesL1()
    {
        e = Instantiate(enemy2, new Vector3(radios[3], 0, 0), Quaternion.identity);
        SetEnemy2(e, 7f, 3f);
        enemies.Enqueue(e);
        e = Instantiate(enemy1, new Vector3(radios[0], 0, 0), Quaternion.identity);
        SetEnemy1(e, 7f, 3f);
        enemies.Enqueue(e);
        sizeEnemies = 2;
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

        if (r>18)// Player can't go out of the screen.
            rb.velocity = new Vector3(angularVel * (y / r) + (radialVel>0?0:radialVel) * (x / r),
                (radialVel > 0 ? 0 : radialVel) * (y / r) - angularVel * (x / r), 0);
        else
            rb.velocity = new Vector3(angularVel * (y / r) + radialVel * (x / r), radialVel * (y / r) - angularVel * (x / r), 0);
       
        if (Input.GetKey(KeyCode.Space) &&(!block))//Shoot
        {
            audioSource.PlayOneShot(shoot, 1F);
            b = Instantiate(bullet, this.t.position + new Vector3(1f * (y / r)* (angularVel > 0 ? 1 : -1),
                -1f* (angularVel > 0 ? 1 : -1) * (x / r), 0), Quaternion.identity);
            SetBullet(b, false);
            b = Instantiate(bullet, this.t.position + new Vector3(1f * +2*(x / r), 1f+2*(y / r), 0), Quaternion.identity);
            SetBullet(b, true);
            block = true;
            i = 0;
        }
        i += 1;
        if (i == waitForShoot)// Unlocks shooting after a certain number of frames.
        {
            i = 0;
            block = false;
        }

        // Unpauses game
        if (Time.timeScale==0 && Input.GetMouseButton(0) && !ended)
        {
            Time.timeScale = 1;
            gameMenu.hideAll();
        }
        else if (Time.timeScale == 0 && Input.GetMouseButton(0) && ended)
        {
            fibon = 1;
            fibon1 = 1;

            level = 1;
            lives = 5;
            while (enemies.Count > 0)
            {
                Destroy(enemies.Dequeue());
            }

            startEnemiesL1();
            gameMenu.hideAll();
            text.text = "Level:    " + level + "     Lives: " + lives;
            Time.timeScale = 1;


        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.tag.Equals( "BulletFriend"))// you can't shoot yourself

        {
            audioSource.PlayOneShot(hit, 1F);
            lives--;
            text.text = "Level:    " + level + "     Lives: " + lives;
            if (lives==0)
            {
                Time.timeScale = 0;
                ended = true;
                
                GameControl.control.FinishMinigame(sceneName, level);
                GameControl.control.Save();
                gameMenu.setBestScoreText("Current best score is: \nLevel " + GameControl.control[sceneName]);


                gameMenu.endDisplay();
                //SceneManager.LoadScene("PresentationScene");

            }
            animator.SetTrigger("Damage");
            
        }
    }
    public void SetBullet(GameObject b, bool isRadial)
    {
        if (b != null)
        {
            var myScriptReference = b.GetComponent<BulletBeh>();
            if (myScriptReference != null)
            {
                myScriptReference.SetBullet(radialSpeed *(isRadial?8f:3.3f), isRadial?true:(angularVel > 0), false,isRadial);
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
        audioSource.PlayOneShot(enemySound, 1F);
        this.sizeEnemies--;
        if (sizeEnemies == 0)
        {
            while (enemies.Count > 0)
            {
               enemies.Dequeue();
            }
            level++;
            text.text = "Level:    " + level + "     Lives: " + lives;
            for (int i=0; i<fibon;i++)
            {
                rr = rand.Next(11);
                e = Instantiate(enemy1, new Vector3(radios[rr], 0, 0), Quaternion.identity);
                SetEnemy1(e, 7f+level, 3f+(level/6f));
                enemies.Enqueue(e);
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
                enemies.Enqueue(e);
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