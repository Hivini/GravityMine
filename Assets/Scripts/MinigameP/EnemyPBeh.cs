using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPBeh : MonoBehaviour
{
    GameObject player;
    public float speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        var pscr = player.GetComponent<PlayerBehP>();
        Vector3 look = pscr.GetCharacterPosition();
        transform.LookAt(look);
        rb.velocity = speed * transform.forward;
        
    }
}
