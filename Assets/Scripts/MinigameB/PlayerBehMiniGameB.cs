using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehMiniGameB : MonoBehaviour
{
    public float thickness;
    public GameObject exit;

    private Text healthText;
    float speed, gForce;
    Rigidbody rb;
    Transform t;
    int status, i, lives, pointsToCollect, freeTiles, k, exitsLiving, currentPoints;
    bool block;
    bool[,] maze;
    int[,] freeCoordinates;
    //Text text;
    System.Random r = new System.Random();
    Queue<GameObject> exits = new Queue<GameObject>();
    GameObject ex;
    bool ended;
    GameMenu gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        t = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        i= 0;
        thickness = 2.0f;
        status = -1;
        speed = 1.2f;
        gForce = 2.5f;
        block = false;
        lives = 10;
        pointsToCollect = exitsLiving = 3;
        currentPoints = 0;
        healthText.text = "Points:    " + currentPoints + "     Lives: " + lives;
        ended = false;
        string currentScene = SceneManager.GetActiveScene().name;
        gameMenu.setStartInstructions("Collect all white boxes without touching the black ones\n** Click to start playing **");
        gameMenu.setEndInstructions("End Game\n** Click anywhere to retry **\n** Click on exit to return to game selection**");
        gameMenu.setBestScoreText("Current best score is: \n" + GameControl.control[currentScene] + " Boxes");
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ended && Time.timeScale!=0)
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
                block = true;
                i = 0;
            }

            if (status != -1)
            {
                bool sign = (status == 1 || status == 2);
                bool isVertical = (status % 2 == 0);
                if (h != 0)
                {

                    rb.velocity = new Vector3(
                            (isVertical ? 0 : (speed * (sign ? 1 : -1))),
                            (isVertical ? (speed * (sign ? 1 : -1)) : 0), 0);
                }

                rb.AddForce((isVertical ? 0 : (gForce * (sign ? 1 : -1))), (isVertical ? (gForce * (sign ? 1 : -1)) : 0), 0, ForceMode.Acceleration);
            }
            if (i == 10)
            {
                block = false;
                i = 0;
            }
            i++;
        }
        else if (Input.GetMouseButton(0) && ended)
        {
            ended = false;
            pointsToCollect = 3;
            lives = 10;
            status = -1;
            while (exits.Count > 0)
            {
                Destroy(exits.Dequeue());
            }
            for (int q = 0; q < pointsToCollect; q++)
            {
                k = r.Next(freeTiles);
                ex = Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
                exits.Enqueue(ex);
            }
            exitsLiving = pointsToCollect;
            Time.timeScale = 1;
            gameMenu.hideAll();
            healthText.text = "Points:    " + currentPoints + "     Lives: " + lives;
        }
        else if (Input.GetMouseButton(0))
        {
            Time.timeScale = 1;
            gameMenu.hideAll();
            healthText.text = "Points:    " + currentPoints + "     Lives: " + lives;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            exitsLiving--;
            Destroy(other.gameObject);
            currentPoints++;
            if (exitsLiving<=0)
            {
                lives += pointsToCollect;
                pointsToCollect = (int)(Random.value*4)+1;
                while (exits.Count > 0)
                {
                    Destroy(exits.Dequeue());
                }
                for (int q = 0; q < pointsToCollect; q++)
                {
                    k = r.Next(freeTiles);
                    ex = Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
                    exits.Enqueue(ex);
                }
                exitsLiving = pointsToCollect;
            }

        }
        else if (other.tag == "DeadWall" )
        {
            lives--;
            if (lives==0)
            {
                Time.timeScale = 0;

                t.position=new Vector3(-7.5f, 4.5f, 0);

                
                ended = true;
                string sceneName = SceneManager.GetActiveScene().name;
                GameControl.control.FinishMinigame(sceneName, currentPoints);
                GameControl.control.Save();
                gameMenu.setBestScoreText("Current best score is: \n" + GameControl.control[sceneName] + " Boxes");
                gameMenu.endDisplay();
                currentPoints = 0;
            }
        }
        healthText.text = "Points:    " + currentPoints + "     Lives: " + lives;
    }

    public void SetMaze(bool[,] maze)
    {
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

        for (int q = 0; q < 3; q++)
        {
            k = r.Next(freeTiles);
            ex = Instantiate(exit, new Vector3(freeCoordinates[k, 0] * 0.5f - 8, freeCoordinates[k, 1] * 0.5f - 6, 0), Quaternion.identity);
            exits.Enqueue(ex);
        }
        exitsLiving = pointsToCollect;

    }
}