using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PcsActions : MonoBehaviour
{
    public Text helpText;

    public void start(string option)
    {
        switch (option)
        {
            case "hola":
                Debug.Log("hola, como estas?");
                break;
            case "print":
                Debug.Log("Tu no me dices que imprimir");
                break;
            case "changeSceneA":
                SceneManager.LoadScene("Final_Minigame_A");
                break;
            case "changeSceneB":
                if (GameControl.control.passedLevels["Final_Minigame_A"])
                    SceneManager.LoadScene("Final_Minigame_B");
                else
                {
                    helpText.text = "You must complete game A";
                    StartCoroutine(ReturnHelpText());
                }

                break;
            case "changeSceneC":
                if (GameControl.control.passedLevels["Final_Minigame_A"] &&
                    GameControl.control.passedLevels["Final_Minigame_B"])
                    SceneManager.LoadScene("Final_Minigame_C");
                else
                {
                    helpText.text = "You must complete game A and B";
                    StartCoroutine(ReturnHelpText());
                }

                break;
            default:
                break;
        }
    }

    IEnumerator ReturnHelpText()
    {
        yield return new WaitForSeconds(3);
        helpText.text = "";
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("PresentationScene");
    }
}
