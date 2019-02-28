using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject destructableCube;
    public GameObject normalGround;

    // Start is called before the first frame update
    void Start()
    {
        int random;
        System.Random r = new System.Random();
        float tenDegRad = Mathf.PI * 1f / 18f; // deg to rad
        //print(tenDegRad);
        float radio = 1f/Mathf.Tan(tenDegRad); //cot 10°
        float x, y, angleRad, angleDegreesRotation;

        for (int z = 0; z <= 30; z++)
        {
            for (int theta = 0; theta <= 18; theta++)
            {
                angleDegreesRotation = theta * 2 * 10f;
                angleRad = tenDegRad *(theta * 2 + 9);
                x = radio * Mathf.Cos(angleRad);
                y = radio * (1 + Mathf.Sin(angleRad));
                if (!(z==0 && theta==0))
                {
                    random = r.Next(3);
                    if (random == 2)
                    {
                        Instantiate(normalGround, new Vector3(x,y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0,0,1)));
                    }
                    else if (random == 1)
                    {
                        Instantiate(destructableCube, new Vector3(x, y, z), Quaternion.AngleAxis(angleDegreesRotation, new Vector3(0, 0, 1)));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
