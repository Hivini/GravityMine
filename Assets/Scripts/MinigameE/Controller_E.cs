using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_E : MonoBehaviour
{
    public GameObject p, w, bw;
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


    // Start is called before the first frame update
    void Start()

    {
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
        level = 3;
        waitPlayer = 0;
        isWaitingPlayer = false;
        loadGO = GameObject.Find("Load");
        var load = loadGO.GetComponent<LoadLevel>();
        load.LoadLev(level);
       

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //selected = null; // TODO HEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE WROOOONG PARA PRUEBAS
       // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.position - hit.point);
        if (player==null)
        {  // solving mode, no playing mode.
            if (Input.GetKey(KeyCode.C) && selected == null && !isWaitingPlayer)
            {
                isWaitingPlayer = true;
                points -= 2;
                //INSTANCIA WALL
                if (Physics.Raycast(ray, out hit))
                {
                    wa = Instantiate(w, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                    allWalls.Enqueue(wa);
                }
            }
            if (Input.GetKey(KeyCode.X) && selected == null && !isWaitingPlayer && level!=3)
            {
                isWaitingPlayer = true;
                //INSTANCIA WALL BOUNCY
                points -= 2;
                if (Physics.Raycast(ray, out hit))
                {
                    wa = Instantiate(bw, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                    allWalls.Enqueue(wa);
                }
            }
            if (Input.GetKey(KeyCode.D) && selected != null && !isWaitingPlayer)
            {
                isWaitingPlayer = true;
                points -= 2;
                if (selected.tag.Equals("Wall"))
                {
                    var scr = selected.GetComponent<WallScr>();
                    scr.IsSelected1 = false;
                }
                else
                {
                    var scr = selected.GetComponent<BouncyWallScr>();
                    scr.IsSelected1 = false;
                }
                Destroy(selected);
                selected = null;
            }
            if (Input.GetKey(KeyCode.M) && selected != null)//&& !isWaitingPlayer)
            {
                // rotar
                isWaitingPlayer = true;
                selected.transform.Rotate(new Vector3(0, 0,1f));
            }
            if (Input.GetKey(KeyCode.N) && selected != null )//&& !isWaitingPlayer)
            {
                //rotar
                isWaitingPlayer = true;
                selected.transform.Rotate(new Vector3(0, 0, -1f));
            }

            //MOVE SELECTED WALL WITH AXiS.
            float h = -1f*Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (selected != null)
            {
                selected.transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * 3, Space.World);
            }

            // START PLAYING
            if (Input.GetKey(KeyCode.Space) && !isWaitingPlayer)
            {
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
                                selected =  wall ;
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
                Destroy(player);
                waitPlayer = 0;
                player = null;
                isWaitingPlayer = true;
            }
            if (player != null)
            {
                if (Mathf.Abs(player.transform.position.y) > 50 || Mathf.Abs(player.transform.position.x) > 100)
                {
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
    public void nextLevel()
    {
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
