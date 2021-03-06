﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldBeh : MonoBehaviour
{
    public float forceField;
    Rigidbody rbo;
    public float    xOrigin = 0,
                    yOrigin=0,
                    zOrigin=0;
    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        forceField = 0.3f;//fields magnitude

        origin = new Vector3(xOrigin, yOrigin, zOrigin);// origin of the force field.
        
    }

    private void OnTriggerStay(Collider other)
    {
        rbo = other.GetComponent<Rigidbody>();
        if (Vector3.Magnitude(rbo.velocity)>=0.01)
        {
            rbo.WakeUp();
        }
        float yt, xt, xr, yr, rCubo,fx,fy;
        yt = other.transform.position.y;
        xt = other.transform.position.x;
        yr = yt - origin.y;
        xr = xt - origin.x;
        rCubo = Mathf.Sqrt(yr * yr * yr + xr * xr * xr); // distance cubed from the origin to the object affected.

        
        fx = -(xr / (Mathf.Abs(rCubo)<0.001f?(0.001f*Mathf.Sign(rCubo)): rCubo)) * forceField;
        fy = -(yr / (Mathf.Abs(rCubo) < 0.001f ? (0.001f * Mathf.Sign(rCubo)) : rCubo)) * forceField;
        rbo.AddForce(   // force of the kind GmM / r^2
            (float.IsNaN(fx)?0:(Mathf.Abs(fx)>10f?Mathf.Sign(fx)*10f:fx)),
            (float.IsNaN(fy) ? 0 : (Mathf.Abs(fy) > 10f ? Mathf.Sign(fy) * 10f : fy)),
            0, ForceMode.Impulse);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(other);
    }
}
