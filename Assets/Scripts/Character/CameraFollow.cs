using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*
    Transform tt;
    GameObject target;
    public float smoothing = 5f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        tt = target.transform;
        offset = transform.position - tt.position;
    }

    public float GetAngleRad() { }
    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    */
    Transform tt;
    GameObject target;
    public float smoothing = 5f;
    public float zDistance = 5f;
    public float rDistance = 5f;
    float angleRad;
    Vector3 v;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("PlayerMinigameC");
        tt = target.transform;
    }


    private void FixedUpdate()
    {
        angleRad =  GetAngleRad(target);
        v = new Vector3(rDistance * Mathf.Cos(angleRad), rDistance * Mathf.Sin(angleRad), zDistance);
        Vector3 targetCamPos = tt.position - v;
        transform.position = targetCamPos;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(90f + (angleRad * 180f / Mathf.PI), new Vector3(0,0,1)), 0.8f);
        transform.Rotate(15f,0,0);
    }

    public float GetAngleRad(GameObject p)
    {
        if (p != null)
        {
            var myScriptReference = p.GetComponent<Player>();
            if (myScriptReference != null)
            {
                return myScriptReference.AngleRad;
            }
            return 0;
        }
        return 0;
    }

}

