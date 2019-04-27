using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransport : MonoBehaviour
{
    public GameObject exitPortal;
   // GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TeletransportObject(GameObject t_transported)
    {

       // if (t_transported.Equals(player))
        //{
            Rigidbody rbt = t_transported.GetComponent<Rigidbody>();
            Transform tt = t_transported.GetComponent<Transform>();
            float speed = rbt.velocity.magnitude;
            rbt.velocity = Vector3.zero;
            tt.position = exitPortal.transform.position;
            rbt.velocity = speed * exitPortal.transform.up;
        //}
    }

}
