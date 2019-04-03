using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public GameObject pauseMenu;
    public Camera camera;

    private GameObject pauseMenuInstance;
    private Rigidbody rigidbody;
    private Animator animator;
    private Collider[] groundCollisions;
    private bool grounded;
    private bool facingRight;
    private bool openPauseMenu;
    private bool lastPressed;

    private const float groundCheckRadius = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        // The player will always start facing right
        facingRight = true;
        grounded = false;
        lastPressed = false;
        openPauseMenu = false;
        if (GameControl.control.hasPos)
        {
            Vector3 pos = new Vector3(GameControl.control.playerX,
                                        GameControl.control.playerY,
                                         GameControl.control.playerZ);
            Vector3 cameraPos =
                        new Vector3(camera.gameObject.transform.position.x,
                                    camera.gameObject.transform.position.y,
                                    pos.z);
            // Update both positions
            gameObject.transform.position = pos;
            camera.gameObject.transform.position = cameraPos;
        }

        StartCoroutine(UpdatePosition());
    }

    // Update is called once per frame
    void Update()
    {
        // Pause menu
        if (Input.GetButton("Pause"))
        {
            if (!lastPressed && !openPauseMenu)
            {
                pauseMenuInstance = Instantiate(pauseMenu);
                openPauseMenu = true;
                lastPressed = true;
            }
            else if (!lastPressed && openPauseMenu)
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
            rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }

        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(h));

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, speed * h);

        if (h > 0 && !facingRight)
        {
            // Changing to right
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            // Changing to left
            Flip();
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

    IEnumerator UpdatePosition()
    {
        // FIXME
        // We can maybe change this and when the player hits saves the
        // gamecontroller will lookup for the object by layer or tag
        while (true)
        {
            GameControl.control.playerX = transform.position.x;
            GameControl.control.playerY = transform.position.y;
            GameControl.control.playerZ = transform.position.z;
            yield return new WaitForSeconds(1);
        }

    }


    public void ClickTest()
    {
        GameControl.control.Save();
    }
}
