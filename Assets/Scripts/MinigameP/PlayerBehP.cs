using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehP : MonoBehaviour
{
    public GameObject character;
    public GameObject auxWall;
    public GameObject enemy;
    public GameObject auxPortal;
    public Text liveText;
    GameObject ap, aw, e;
    public GameObject ch;
    float speed;
    int wall;
    int lives, level,
        countAuxPortalBlock,
        countRotateBlock;
    float countCreateWall;
    bool auxPortalBlock, auxWallBlock, rotateBlock, hasEnter;
    GameObject exitPortal, enterPortal;
    public GameObject PortalController;
    Rigidbody rb;
    Transform t;
    Ray ray;
    private float depth;
    private string sceneName;

    GameObject startGameInstructions;
    GameObject endGameInstructions;
    Text bestScore;
    Text livesText;
    bool ended;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        livesText = GameObject.Find("lives").GetComponent<Text>();
        startGameInstructions = GameObject.Find("startInstructions");
        endGameInstructions = GameObject.Find("endGameInstructions");
        bestScore = GameObject.Find("bestScore").GetComponent<Text>();

        depth = -1;
        Reset();

        Time.timeScale = 0;
        ended = false;
        bestScore.text = "Current best score is: \nLevel: " + GameControl.control[sceneName];
        endGameInstructions.SetActive(false);

    }
    private void Reset()
    {
        
        hasEnter = false;
        speed = 3;
        rotateBlock = false;
        auxPortalBlock = false;
        auxWallBlock = false;
        countAuxPortalBlock = 0;
        countRotateBlock = 0;
        countCreateWall = 0;
        if (ch != null)
        {
            Destroy(ch);
        }
        level = 1;
        lives = 3;
        ch = Instantiate(character, new Vector3(0, 0, depth), Quaternion.identity);
        rb = ch.GetComponent<Rigidbody>();
        t = ch.GetComponent<Transform>();
        float direction = Random.Range(0f, 360f);
        rb.velocity = new Vector3(speed * Mathf.Cos(direction * Mathf.PI / 180f), speed * Mathf.Sin(direction * Mathf.PI / 180f), 0);
        liveText.text = "Lives: " + lives + " Level: " + level;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ended && Time.timeScale != 0)
        {
            print(rb.velocity.magnitude);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 create = new Vector3(hit.point.x, hit.point.y, 0.9f);
                if (Mathf.Abs(create.y) < 5.4 && Mathf.Abs(create.x) < 7.4)
                { // dentro de la caja


                    if (Input.GetMouseButton(0) && !auxPortalBlock &&
                        (Mathf.Abs(create.y) < 3.5f && Mathf.Abs(create.x) < 6f)
                        )
                    {
                        auxPortalBlock = true;
                        countAuxPortalBlock = 0;

                        t.position = new Vector3(create.x, create.y, depth);
                        ap = Instantiate(auxPortal, create, Quaternion.Euler(90, 0, 0));
                        Destroy(ap, 1);

                    }


                    if (Input.GetMouseButton(1) && !auxWallBlock)
                    {
                        auxWallBlock = true;
                        Vector3 createWall = new Vector3(create.x, create.y, depth);
                        aw = Instantiate(auxWall, createWall, Quaternion.identity);
                        Destroy(aw, 5);
                        countCreateWall = Time.time + .3f;
                    }
                    if (Input.GetAxisRaw("Horizontal") == 1 && aw != null && !rotateBlock)
                    {
                        aw.transform.Rotate(new Vector3(0, 0, 1), 45f);
                        rotateBlock = true;
                        countRotateBlock = 0;

                    }
                    else if (Input.GetAxisRaw("Horizontal") == -1 && aw != null && !rotateBlock)
                    {
                        aw.transform.Rotate(new Vector3(0, 0, 1), -45f);
                        rotateBlock = true;
                        countRotateBlock = 0;

                    }

                }
            }
            countAuxPortalBlock++;
            countRotateBlock++;
            if (countAuxPortalBlock > 50)
            {
                countAuxPortalBlock = 0;
                auxPortalBlock = false;
            }
            if (countRotateBlock > 15)
            {
                countRotateBlock = 0;
                rotateBlock = false;
            }
            if (countCreateWall < Time.time)
            {
                countCreateWall = -1;
                auxWallBlock = false;
            }
        }
        else if (Input.GetMouseButton(0) && ended)
        {
            ended = false;
            endGameInstructions.SetActive(false);
            var pcScript = PortalController.GetComponent<PortalControllerScript>();
            pcScript.Reset();
            Reset();
            bestScore.text = "";
            Time.timeScale = 1;
        }
        else if (Input.GetMouseButton(0))
        {
            bestScore.text = "";
            Time.timeScale = 1;
            startGameInstructions.SetActive(false);
        }
    }

    public void CollisionCharacter(Collision collision)
    {
        if (!hasEnter)
        {
            if (collision.collider.tag.Equals("EnterPortal"))
            {
                hasEnter = true;
                rb.velocity = Vector3.zero;
                StartCoroutine("wait");
                level++;
                lives++;
                liveText.text = "Lives: " + lives + " Level: " + level;
                if (level == 2)
                {
                    e=Instantiate(enemy, Vector3.zero, Quaternion.identity);
                }
                print("player level" + level);
                auxWallBlock = false;
                speed += 0.1f;
                print(exitPortal.transform.position);
                var pcScript = PortalController.GetComponent<PortalControllerScript>();
                wall = pcScript.GetExitWall();
                Vector2 wallPositionCor = new Vector2((wall % 2 == 0) ? 0 : (wall == 1 ? 1 : -1), (wall % 2 == 0) ? (wall == 0 ? -1 : 1) : 0);
                Vector3 newPositionBall = new Vector3(
                    .6f * wallPositionCor.x + exitPortal.transform.position.x,
                    .6f * wallPositionCor.y + exitPortal.transform.position.y,
                    t.position.z);


                t.SetPositionAndRotation(newPositionBall, t.rotation);
                float direction = pcScript.GetDirectionSpeedPlayer();
                rb.velocity = speed * (new Vector3(Mathf.Cos(Mathf.Deg2Rad * (direction)), Mathf.Sin(Mathf.Deg2Rad * (direction)), 0));
                pcScript.Upgrade();

            }
            else if (collision.collider.tag.Equals("AuxWall"))
            {
                //print("no hace nada");
                Destroy(collision.gameObject, 2);
            }
            else
            {
                lives--;
                liveText.text = "Lives: " + lives + " Level: " + level;
                if (lives == 0)
                {
                    Time.timeScale = 0;
                    ended = true;
                    GameControl.control.FinishMinigame(sceneName, level);
                    GameControl.control.Save();
                    bestScore.text = "Current best score is: \nLevel: " + GameControl.control[sceneName];
                    endGameInstructions.SetActive(true);
                    Destroy(e);
                    print("I reach this place");
                    //print("sin vidas");
                }
                else
                {
                    //print("WROOOONG");
                    t.position = new Vector3(0, 0, depth);
                    float direction = Random.Range(0f, 360f);
                    Vector3 vel = new Vector3(speed * Mathf.Cos(direction * Mathf.PI / 180f), speed * Mathf.Sin(direction * Mathf.PI / 180f), 0);
                    print(vel);
                    print(vel.magnitude);
                    rb.velocity = vel;
                }
            }
        }
    }

    public void SetExitPortal(GameObject xp)
    {
        exitPortal = xp;
    }
    public void SetEnterPortal(GameObject ep)
    {
        enterPortal = ep;   
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        hasEnter = false;
    }
    public Vector3 GetCharacterPosition()
    {
        return (ch == null) ? Vector3.zero : ch.transform.position;
    }
}
