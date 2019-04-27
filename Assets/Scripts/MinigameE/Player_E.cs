using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_E : MonoBehaviour
{
    Rigidbody rb;
    GameObject masterController;
    // Start is called before the first frame update
    void Start()
    {
        masterController = GameObject.Find("MasterController_E");
        //rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0,-20,0), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //print(rb.velocity.magnitude);   
    }
    private void OnTriggerEnter(Collider other)
    {
        {
            print("que pex");
            if (other.tag.Equals("Exit"))
            {
                print("diiiii");
                var cont = masterController.GetComponent<Controller_E>();
                cont.nextLevel();
                Destroy(gameObject);
            }
        }
    }
}

