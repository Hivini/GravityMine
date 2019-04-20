using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public static bool openPauseMenu;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public GameObject pauseMenu;
    public Camera camera;
    public Text scoreText;

    private GameObject pauseMenuInstance;
    private Rigidbody rigidbody;
    private Animator animator;
    private Collider[] groundCollisions;
    private bool grounded;
    private bool facingRight;
    private bool gravityDown;
    private bool lastPressed;
    private bool gravityLastPressed;
    private int score;

    private const float groundCheckRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO Load and save the score in the game controller.
        score = 0;
        UpdateScore(0);
        gravityLastPressed = false;
        if(PlayerPrefs.GetInt("gravityDown", 1) == 1)
        {
            PlayerPrefs.SetInt("gravityDown", 1);
            gravityDown = true;
        }
        else
        {
            gravityDown = false;
            this.transform.up *= -1;
        }
        
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        // The player will always start facing right
        facingRight = true;
        grounded = false;
        lastPressed = false;
        PlayerController.openPauseMenu = false;
        if (GameControl.control.hasPos)
        {
            Vector3 pos = new Vector3(GameControl.control.playerX,
                                        GameControl.control.playerY,
                                         GameControl.control.playerZ);
            Vector3 cameraPos =
                        new Vector3(camera.gameObject.transform.position.x,
                                    pos.y,
                                    pos.z);
            // Update both positions
            gameObject.transform.position = pos;
            camera.gameObject.transform.position = cameraPos;
        }
        else
        {
            GameControl.control.hasPos = true;
        }

        StartCoroutine(UpdatePos());
    }

    // Update is called once per frame
    void Update()
    {
        // Pause menu
        if (Input.GetButton("Pause"))
        {
            if (!lastPressed && !PlayerController.openPauseMenu)
            {
                GameControl.control.playerX = transform.position.x;
                GameControl.control.playerY = transform.position.y;
                GameControl.control.playerZ = transform.position.z;
                pauseMenuInstance = Instantiate(pauseMenu);
                openPauseMenu = true;
                lastPressed = true;
            }
            else if (!lastPressed && PlayerController.openPauseMenu)
            {
                Destroy(pauseMenuInstance.gameObject);
                openPauseMenu = false;
                lastPressed = true;
            }
        }
        else
        {
            lastPressed = false;
        }

        // TODO Maybe change this to Fixed Update ?
        // Draw a tiny sphere and check what is colliding
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        animator.SetBool("grounded", grounded);

        // Jump if its on the ground
        if (grounded && Input.GetButton("Jump"))
        {
            grounded = false;
            animator.SetBool("grounded", grounded);
            // Do the jump
            rigidbody.AddForce(new Vector3(0, jumpForce * (gravityDown?1:-1), 0));
        }

        float h = Input.GetAxis("Horizontal");
        

        animator.SetFloat("speed", Mathf.Abs(h));

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, speed * h);

        
        if (gravityDown && (h > 0 && !facingRight  || h < 0 && facingRight) || !gravityDown && (h > 0 && facingRight || h < 0 && !facingRight))
        {
            Flip();
        }

        if (Input.GetButton("ChangeGravity"))
        {
            if (!gravityLastPressed)
            {
                Physics.gravity = Physics.gravity * -1;
                gravityLastPressed = true;
                
                this.transform.up *= -1;
                
                gravityDown = !gravityDown;
                PlayerPrefs.SetInt("gravityDirection", gravityDown?1:0);
            }
        }
        else
        {
            gravityLastPressed = false;
        }

    }

    IEnumerator UpdatePos()
    {
        while (true)
        {
            GameControl.control.playerX = transform.position.x;
            GameControl.control.playerY = transform.position.y;
            GameControl.control.playerZ = transform.position.z;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Flip()
    {
        // Just invert the value
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;

        scale.z *= -1;
        
        transform.localScale = scale;
    }

    public void ClickTest()
    {
        GameControl.control.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Coin")
        {
            UpdateScore(100);
            Destroy(other.gameObject);
        }
    }

    private void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
