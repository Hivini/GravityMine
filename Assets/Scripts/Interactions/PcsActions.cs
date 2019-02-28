using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PcsActions : MonoBehaviour
{
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
            default:
                break;
        }
    }
}
