using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject tubeReference;
    public float jumpForce=200;
    public float speed=5;
    public float speedRadial=3;
    public float rotationSpeed = 15;
    public float playerGravity = 5;
    public AudioClip bounce;
    private AudioSource audioSource;

    private bool lastJ;
    private Rigidbody rigidbody;
    private bool destroying;
    float angleRad, angleDeg;
    public int zPos;
    public Spawner spawner;

    Text distanceText;
    bool ended;
    private Vector3 startPos;
    private GameMenu gameMenu;
    public float AngleRad { get => angleRad; set => angleRad = value; }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameMenu = GameObject.Find("GameMenu").GetComponent<GameMenu>();
        distanceText = GameObject.Find("distance").GetComponent<Text>();
        zPos = 0;
        rigidbody = GetComponent<Rigidbody>();
        lastJ = false;
        angleDeg = -90f;
        AngleRad = (Mathf.PI / 180f) * angleDeg;
        destroying = false;
        startPos = transform.position;
        gameMenu.setStartInstructions("Go as far as possible!\n** Click to start playing **");
        gameMenu.setEndInstructions("End Game\n** Click anywhere to retry **\n** Click on exit to return to game selection**");
        string sceneName = SceneManager.GetActiveScene().name;
        gameMenu.setBestScoreText("Current best score is: \n" + GameControl.control[sceneName] + " meters");
        gameMenu.startDisplay();
        Time.timeScale = 0;
        ended = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ended && Time.timeScale != 0)
        {
            float changeOfangle = Input.GetAxis("Horizontal");
            angleDeg += speedRadial * changeOfangle;
            AngleRad = (Mathf.PI / 180f) * angleDeg;
            float z = Input.GetAxis("Vertical");

            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, speed * z );
            rigidbody.AddForce(playerGravity * new Vector3(0, -1, 0), ForceMode.Acceleration);
            //rigidbody.AddForce(9.8f*(new Vector3(Mathf.Cos(AngleRad), Mathf.Sin(AngleRad), 0)), ForceMode.Acceleration);
            tubeReference.transform.Rotate(0, 0, changeOfangle * Time.deltaTime * rotationSpeed, Space.World);
            //tubeReference.transform.rotation = new Quaternion(0, 0, tubeReference.transform.rotation.z - changeOfangle, 1);
            //transform.Translate(new Vector3(x * Time.deltaTime * speed, transform.position.y, z * Time.deltaTime * speed));
            zPos = (int)transform.position.z;
            distanceText.text = "Distance: " + zPos + " m.";
            if (zPos % 100 >= 50 && !destroying)
            {
                destroying = true;
                spawner.destroyPast();
                spawner.spawn();
            }
            destroying = !(zPos % 100 < 50) && destroying;
        }
        else if (Input.GetMouseButton(0) && ended)
        {
            spawner.destroyAll();
            ended = false;
            transform.position = startPos;
            gameMenu.hideAll();
            spawner.spawn(0);
            Time.timeScale = 1;
        }
        else if (Input.GetMouseButton(0))
        {
            print("Clicked");
            Time.timeScale = 1;
            gameMenu.hideAll();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            audioSource.PlayOneShot(bounce, 1F);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(0, jumpForce, 0);
            // Reset the velocity to not affect the force
            //rigidbody.AddForce(jumpForce*(new Vector3(-Mathf.Cos(AngleRad), -Mathf.Sin(AngleRad), 0)));
        }
        else if(collision.gameObject.layer==LayerMask.NameToLayer("Water"))
        {
            Time.timeScale = 0;
            ended = true;
            string sceneName = SceneManager.GetActiveScene().name;
            GameControl.control.FinishMinigame(sceneName, zPos);
            GameControl.control.Save();
            gameMenu.setBestScoreText("Current best score is: \n" + GameControl.control[sceneName] + " meters");
            gameMenu.endDisplay();
        }
    }

    
}
