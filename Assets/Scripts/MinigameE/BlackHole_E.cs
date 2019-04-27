using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_E : MonoBehaviour
{
    GameObject player;
    Transform tp, t;
    Rigidbody rbp;
    public float Gm;
    // Start is called before the first frame update
    void Start()
    {
        Gm = 500;
        t = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            tp = player.transform;
            rbp = player.GetComponent<Rigidbody>();
            Vector3 r = tp.position - t.position;
            float r3 = r.magnitude * r.magnitude * r.magnitude;
            rbp.AddForce(-Gm * r / r3, ForceMode.Acceleration);
        }
    }
}
