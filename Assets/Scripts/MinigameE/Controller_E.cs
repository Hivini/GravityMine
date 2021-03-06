﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_E : MonoBehaviour
{
    public GameObject p, w, bw;
    public AudioClip createSound, startSound, moveSound, coinSound, hitSound;
    GameObject player;
    Vector3[] inicioPlayer;
    int level;
    int waitPlayer;
    bool isWaitingPlayer;
    int points;
    GameObject loadGO;
    GameObject selected;
    Queue<GameObject> allWalls;
    private Ray ray;
    RaycastHit hit;
    GameObject wa;

    public GameMenu gameMenu;
    private string sceneName;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        gameMenu.setStartInstructions("Create your own maze and move the ball to the box!\nUse X and C to create two types of walls\n Click on them to select (and unselect) them.\n  Move the selected one with WASD or the arrow keys, ER to rotate it, and Q to delete it\n Press space to spawn the ball!\n** Click to start playing **");
        gameMenu.setEndInstructions("End Game\n** Click anywhere to retry **\n** Click on exit to return to game selection**");
        gameMenu.setBestScoreText("Try to reach level 3!\nClick end when you want to exit\n Enjoy! ");
        audioSource = GetComponent<AudioSource>();
        allWalls  = new Queue<GameObject>();
        selected=null;
        points = 100;
        inicioPlayer = new Vector3[10];
        inicioPlayer[0] = new Vector3(-11.5f, 42f,0);
        inicioPlayer[1] = new Vector3(-3, 42f, 0);
        inicioPlayer[2] = new Vector3(-30, 42f, 0);
        inicioPlayer[3] = new Vector3(-11.5f, 42f, 0);
        inicioPlayer[4] = new Vector3(2, 42f, 0);
        inicioPlayer[5] = new Vector3(4, 42f, 0);
        inicioPlayer[6] = new Vector3(-11.5f, 42f, 0);
        inicioPlayer[7] = new Vector3(-11.5f, 42f, 0);
        inicioPlayer[8] = new Vector3(0, 42f, 0);
        inicioPlayer[9] = new Vector3(-11.5f, 42f, 0);
        level = 1;
        waitPlayer = 0;
        isWaitingPlayer = false;
        loadGO = GameObject.Find("Load");
        var load = loadGO.GetComponent<LoadLevel>();
        load.LoadLev(level);
        Time.timeScale = 0;
        gameMenu.startDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //selected = null; // TODO HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE WROOOONG PARA PRUEBAS
            // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.position - hit.point);
            if (player == null)
            {  // solving mode, no playing mode.
                if (Input.GetKey(KeyCode.C) && selected == null && !isWaitingPlayer)
                {
                    isWaitingPlayer = true;
                    points -= 2;
                    //INSTANCIA WALL
                    if (Physics.Raycast(ray, out hit))
                    {
                        audioSource.PlayOneShot(createSound, 1F);
                        wa = Instantiate(w, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                        allWalls.Enqueue(wa);
                    }
                }
                if (Input.GetKey(KeyCode.X) && selected == null && !isWaitingPlayer && level != 3)
                {
                    isWaitingPlayer = true;
                    //INSTANCIA WALL BOUNCY
                    points -= 2;
                    if (Physics.Raycast(ray, out hit))
                    {
                        audioSource.PlayOneShot(createSound, 1F);
                        wa = Instantiate(bw, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                        allWalls.Enqueue(wa);
                    }
                }
                if (Input.GetKey(KeyCode.Q) && selected != null && !isWaitingPlayer)
                {
                    isWaitingPlayer = true;
                    points -= 2;
                    if (selected.tag.Equals("Wall"))
                    {
                        var scr = selected.GetComponent<WallScr>();
                    }
                    else
                    {
                        var scr = selected.GetComponent<BouncyWallScr>();
                        scr.IsSelected1 = false;
                    }
                    Destroy(selected);
                    selected = null;
                }
                if (Input.GetKey(KeyCode.E) && selected != null)//&& !isWaitingPlayer)
                {
                    // rotar
                    isWaitingPlayer = true;
                    selected.transform.Rotate(new Vector3(0, 0, 1f));
                }
                if (Input.GetKey(KeyCode.R) && selected != null)//&& !isWaitingPlayer)
                {
                    //rotar
                    isWaitingPlayer = true;
                    selected.transform.Rotate(new Vector3(0, 0, -1f));
                }

                //MOVE SELECTED WALL WITH AXiS.
                float h = -1f * Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                if (selected != null)
                {
                    selected.transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * 3, Space.World);
                }

                // START PLAYING
                if (Input.GetKey(KeyCode.Space) && !isWaitingPlayer)
                {
                    audioSource.PlayOneShot(startSound, 1F);
                    player = Instantiate(p, inicioPlayer[level - 1], Quaternion.identity);
                    waitPlayer = 0;
                    isWaitingPlayer = true;

                }

                // select -  deselect a wall with mouse.
                if (Input.GetMouseButton(0) && !isWaitingPlayer)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        //print("hit");

                        GameObject wall = hit.collider.gameObject;
                        //print(wall.name);
                        if (wall.gameObject.Equals(selected))
                        {
                            if (wall.tag.Equals("BWall"))
                            {
                                var scr = wall.GetComponent<BouncyWallScr>();
                                scr.Select(false);
                                selected = null;
                            }
                            else if (wall.tag.Equals("Wall"))
                            {
                                var scr = wall.GetComponent<WallScr>();
                                scr.Select(false);
                                selected = null;
                            }
                        }
                        else
                        {
                            if (selected == null)
                            {
                                if (wall.tag.Equals("BWall"))
                                {
                                    //print("hitBw");
                                    var scr = wall.GetComponent<BouncyWallScr>();
                                    scr.Select(true);
                                    selected = wall;
                                }
                                else if (wall.tag.Equals("Wall"))
                                {
                                    //print("hitw");
                                    var scr = wall.GetComponent<WallScr>();
                                    scr.Select(true);
                                    selected = wall;
                                }
                            }
                            else
                            {
                                if (selected.tag.Equals("BWall"))
                                {
                                    if (wall.tag.Equals("BWall"))
                                    {
                                        var s = selected.GetComponent<BouncyWallScr>();
                                        s.Select(false);
                                        var s2 = wall.GetComponent<BouncyWallScr>();
                                        s2.Select(true);
                                        selected = wall;


                                    }
                                    else if (wall.tag.Equals("Wall"))
                                    {
                                        var s = selected.GetComponent<BouncyWallScr>();
                                        s.Select(false);
                                        var s2 = wall.GetComponent<WallScr>();
                                        s2.Select(true);
                                        selected = wall;
                                    }


                                }
                                else if (selected.tag.Equals("Wall"))
                                {
                                    if (wall.tag.Equals("BWall"))
                                    {
                                        var s = selected.GetComponent<WallScr>();
                                        s.Select(false);
                                        var s2 = wall.GetComponent<BouncyWallScr>();
                                        s2.Select(true);
                                        selected = wall;


                                    }
                                    else if (wall.tag.Equals("Wall"))
                                    {
                                        var s = selected.GetComponent<WallScr>();
                                        s.Select(false);
                                        var s2 = wall.GetComponent<WallScr>();
                                        s2.Select(true);
                                        selected = wall;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            { // playing mode.
              // DESTROY PLAYER OUTSIDE PLANE.
                if (Input.GetKey(KeyCode.Space) && !isWaitingPlayer)
                {
                    audioSource.PlayOneShot(hitSound, 1F);
                    Destroy(player);
                    waitPlayer = 0;
                    player = null;
                    isWaitingPlayer = true;
                }
                if (player != null)
                {
                    if (Mathf.Abs(player.transform.position.y) > 50 || Mathf.Abs(player.transform.position.x) > 100)
                    {
                        audioSource.PlayOneShot(hitSound, 1F);
                        Destroy(player);
                        //print("destruido");
                    }
                }


            }
            waitPlayer++;
            if (waitPlayer == 50)
            {
                isWaitingPlayer = false;
                waitPlayer = 0;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            gameMenu.hideAll();
            Time.timeScale = 1;
        }

    }
    public void nextLevel()
    {
        audioSource.PlayOneShot(coinSound, 1F);
        level++;
            points += 100;
        var load = loadGO.GetComponent<LoadLevel>();
            load.LoadLev(level);
        while (allWalls.Count>0)
        {
            Destroy(allWalls.Dequeue());
        }
    }

}
