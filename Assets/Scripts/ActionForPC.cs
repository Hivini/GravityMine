﻿using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionForPC : MonoBehaviour, IActionOnInteraction
{
    public void action(string option)
    {
        switch (option)
        {
            case "hola\r":
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
