using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcsActions : MonoBehaviour
{
    public void start(string option)
    {
        switch (option)
        {
            case "hola":
                Debug.Log("hola, como estas?");
                break;
            case "print\r":
                Debug.Log("Tu no me dices que imprimir");
                break;
            default:
                break;
        }
    }
}
