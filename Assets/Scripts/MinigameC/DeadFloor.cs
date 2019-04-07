using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFloor : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector3(0, 0, 5 * z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = startPos;
    }
}
