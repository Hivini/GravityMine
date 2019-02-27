using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehMiniGameB : MonoBehaviour
{
    public float thickness;
    public GameObject exit;
    float speed, gForce;
    Rigidbody rb;
    Transform t;
    int status, i, lives, level, freeTiles, k, exitsLiving;
    bool block;
    bool[,] maze;
    int[,] freeCoordinates;
    //Text text;
    System.Random r = new System.Random();
    Queue<GameObject> exits = new Queue<GameObject>();
    GameObject ex;
    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        i= 0;
        thickness = 2.0f;
        status = -1;
        speed = 1.2f;
        gForce = 2.5f;
        block = false;
        lives = 10;
        level = 1;
        //text.text = "Level:    " + level + "     Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        int h = (int)Input.GetAxisRaw("Horizontal");
        int v = (int)Input.GetAxisRaw("Vertical");
        if (!block)
        {
            if (h != 0)
            {
                status = 2 - h;
            }
            else
            {
                if (v != 0)
                {
                    status = 1 + v;
                }
            }
            block=true;
            i= 0;
        }
        
        if (status!=-1)
        {
            bool sign = (status ==1 || status == 2);
            bool isVertical = (status % 2 == 0);
            if (h!=0)
            {
                
                rb.velocity = new Vector3(
                        (isVertical ? 0 : (speed * (sign ? 1 : -1)) ),
                        (isVertical ? (speed * (sign ? 1 : -1)) : 0 ), 0) ;
            }

            rb.AddForce((isVertical ? 0: (gForce* (sign ? 1 : -1)) ),(isVertical? (gForce * (sign ? 1 : -1)) : 0 ),0,ForceMode.Acceleration);
        }
        if (i==10)
        {
            block = false;
            i= 0;
        }
        i++;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            exitsLiving--;
            Destroy(other.gameObject);
            if (exitsLiving<=0)
            {
                level++;
                lives++;
                //text.text = "Level:    " + level + "     Lives: " + lives;
                while (exits.Count > 0)
                {
                    Destroy(exits.Dequeue());
                }
                for (int q = 0; q < level; q++)
                {
                    k = r.Next(freeTiles);
                    ex = Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
                    exits.Enqueue(ex);
                }
                exitsLiving = level;
            }

        }
        else if (other.tag == "DeadWall")
        {
            lives--;
            //text.text = "Level:    " + level + "     Lives: " + lives;
            if (lives==0)
            {
                t.position=new Vector3(-7.5f, 4.5f, 0);
                level = 1;
                lives = 10;
                status = -1;
                //text.text = "Level:    " + level + "     Lives: " + lives;
                while (exits.Count>0)
                {
                    Destroy(exits.Dequeue());
                }
                for(int q=0;q<level ;q++)
                {
                    k = r.Next(freeTiles);
                    ex = Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
                    exits.Enqueue(ex);
                }
                exitsLiving = level;
            }
        }
    }
    public void SetMaze(bool[,] maze)
    {
        print("tHIS WAS CALLED");
        freeTiles = 0;
        this.maze = maze;
        for (int i=0;i< maze.GetLength(0); i++){
            for (int j = 0; j < maze.GetLength(1); j++){
                freeTiles += maze[i, j] ? 0 : 1;
            }
        }
        freeCoordinates = new int[freeTiles, 2];
        k = 0;
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if(!maze[i, j])
                {
                    freeCoordinates[k, 0] = i;
                    freeCoordinates[k, 1] = j;
                    k++;
                }
                
            }
        }
        k = r.Next(freeTiles);
        ex=Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
        exits.Enqueue(ex);

    }
}