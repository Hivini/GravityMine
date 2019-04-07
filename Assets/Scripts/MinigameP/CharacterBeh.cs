using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBeh : MonoBehaviour
{
    GameObject parent;

    private void Start()
    {
        parent = GameObject.Find("Player");   
    }
    private void OnCollisionEnter(Collision collision)
    {
        var playerScript = parent.GetComponent<PlayerBehP>();
        playerScript.CollisionCharacter(collision);
    }
}
