using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehP : MonoBehaviour
{
    public GameObject character;
    public GameObject auxWall;
    public GameObject enemy;
    public GameObject auxPortal;
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
    // Start is called before the first frame update
    void Start()
    {
        depth = -1;
        Reset();

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
        print("lives"+ lives);
    }

    // Update is called once per frame
    void Update()
    {
        print(rb.velocity.magnitude);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 create = new Vector3(hit.point.x, hit.point.y, 0.9f);
            if (Mathf.Abs(create.y)<5.4 && Mathf.Abs(create.x) < 7.4) { // dentro de la caja


                if (Input.GetMouseButton(0) && !auxPortalBlock)
                {
                    auxPortalBlock = true;
                    countAuxPortalBlock = 0;

                    t.position = new Vector3(create.x, create.y, depth);
                    ap =Instantiate(auxPortal, create, Quaternion.Euler(90,0,0));
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
                if (Input.GetAxisRaw("Horizontal") == 1 && aw!=null && !rotateBlock)
                {
                    aw.transform.Rotate(new Vector3(0,0,1),45f);
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
        countAuxPortalBlock ++;
        countRotateBlock++;
        if (countAuxPortalBlock>50)
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
                print("no hace nada");
                Destroy(collision.gameObject, 2);
            }
            else
            {
                lives--;
                print("lives: " + lives);
                if (lives == 0)
                {
                    // TODO: Create the end game UI
                    Destroy(e);
                    print("sin vidas");
                    var pcScript = PortalController.GetComponent<PortalControllerScript>();
                    pcScript.Reset();
                    Reset();
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
