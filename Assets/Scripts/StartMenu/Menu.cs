using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody[] btns;
    public Camera mainCamera;

    private bool gravityStatus;
    private bool wasClicked;
    void Start()
    {
        foreach(Rigidbody btn in btns)
        {
            btn.useGravity = false;
        }
        wasClicked = false;
        gravityStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isClicked = Input.GetMouseButtonDown(0);
        if (isClicked && !wasClicked)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; ;

            if(Physics.Raycast(ray, out hit))
            {
                string collisionName = hit.collider.transform.name;
                switch (collisionName)
                {
                    case "ContinueBtn":
                        continueGame();
                        break;
                    case "NewGameBtn":
                        newGame();
                        break;
                    case "GravityBtn":
                        activateGravity();
                        break;
                    case "ExitBtn":
                        exitGame();
                        break;
                    default:
                        break;
                }
            }
        }
        wasClicked = isClicked;
    }

    void activateGravity()
    {
        if (!gravityStatus)
        {
            foreach (Rigidbody btn in btns)
            {
                btn.useGravity = true;
            }
        }
    }

    void newGame()
    {
        SceneManager.LoadScene("PresentationScene");
    }

    void continueGame()
    {
        // TODO: Load data and change to the first scene
        SceneManager.LoadScene("PresentationScene");
    }

    void exitGame()
    {
        Application.Quit();
    }
}
