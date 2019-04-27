using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlaneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       // print("que pedo");
        var scrTele = gameObject.GetComponentInParent<Teletransport>();
        scrTele.TeletransportObject(collision.gameObject);
        //print("colided!");
    }
    
}
