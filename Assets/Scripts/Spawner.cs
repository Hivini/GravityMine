using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject destructableCube;
    public GameObject normalGround;
    float numberOfTiles = 18;
    // Start is called before the first frame update
    void Start()
    {
        int random;
        System.Random r = new System.Random();
        float halfThetaRad = Mathf.PI /(numberOfTiles); // deg to rad
        //print(tenDegRad);
        float radio = 1f/Mathf.Tan(halfThetaRad); //cot (theta/2)°
        float x, y, angleRad, angleDegreesRotation;

        for (int z = 0; z <= 100; z++)
        {
            for (int phi = 0; phi <= numberOfTiles; phi++)
            {
                angleDegreesRotation = phi * 2f * (180f*halfThetaRad/Mathf.PI);
                angleRad = halfThetaRad * (phi * 2f + 9f);
                x = radio * Mathf.Cos(angleRad);
                y = radio * (1 + Mathf.Sin(angleRad));
                if (!(z==0 && phi == 0))
                {
                    
                    random = r.Next(100);
                    if (random <30)
                    {
                        Instantiate(normalGround, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                    }
                    else if (random>80)
                    {
                        Instantiate(destructableCube, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                    }
                    //Instantiate(normalGround, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
