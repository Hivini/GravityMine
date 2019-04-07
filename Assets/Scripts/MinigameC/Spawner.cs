using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject destructableCube;
    public GameObject normalGround;
    public GameObject tubeReference;
    readonly int numberOfTiles = 25;
    float radio;

    public float Radio { get => radio; set => radio = value; }

    // Start is called before the first frame update
    void Start()
    {
        int random;
        System.Random r = new System.Random();
        float halfThetaRad = Mathf.PI /(numberOfTiles); // rad
        float halfThetaDeg = (180f * halfThetaRad / Mathf.PI);
        //print(tenDegRad);
        Radio = 1f/Mathf.Tan(halfThetaRad); //cot (theta/2)°
        float x, y, angleRad, angleDegreesRotation;

        for (int z = 0; z <= 100; z += 2)
        {
            for (int phi = 0; phi < numberOfTiles; phi++)
            {
                angleDegreesRotation = 90f+phi * 360f / (0.0f+numberOfTiles);
                angleRad = phi * 2f*Mathf.PI / (0.0f + numberOfTiles);
                x = Radio * Mathf.Cos(angleRad);
                y = Radio * (1 + Mathf.Sin(angleRad));
                if (!(z==0 && phi == 0))
                {
                    
                    random = r.Next(100);
                    if (random <30)
                    {
                        var platform = Instantiate(normalGround, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                        platform.transform.parent = tubeReference.transform;
                    }
                    else if (random>80)
                    {
                        var platform = Instantiate(destructableCube, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                        platform.transform.parent = tubeReference.transform;
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
