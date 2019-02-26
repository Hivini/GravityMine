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
        for (int i = 0; i <= 10; i++)
        {
            for (int y = 0; y <= 10; y++)
            {
                int random = Random.Range(0, 3);
                if (random == 2) 
                {
                    Instantiate(normalGround, new Vector3((2 * i) + 1, 0, 2 * y + 1), new Quaternion(0, 0 ,0 ,1));
                }
                else if (random == 1)
                {
                    Instantiate(destructableCube, new Vector3(2 * i + 1, 0, 2 * y + 1), new Quaternion(0, 0, 0, 1));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
