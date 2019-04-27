using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionLevel1 : MonoBehaviour
{
    public GameObject bouncyWall;
    public GameObject wall;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        int level = 1;
        if (level == 1)
        {
            go = Instantiate(bouncyWall, new Vector3(5.3f, 31f, 0f), Quaternion.Euler(0, 0, 90));
            var src = go.GetComponent<BouncyWallScr>();
            src.IsSelected1 = false;
            go = Instantiate(bouncyWall, new Vector3(24.1f, 6.5f, 0f), Quaternion.Euler(0, 0, -22));
            src = go.GetComponent<BouncyWallScr>();
            src.IsSelected1 = false;
            go = Instantiate(bouncyWall, new Vector3(-50.5f, 23.7f, 0f), Quaternion.Euler(0, 0, -34.72f));
            src = go.GetComponent<BouncyWallScr>();
            src.IsSelected1 = false;
            go = Instantiate(wall, new Vector3(-3.9f, 8.3f, 0f), Quaternion.Euler(0, 0, 0));
            var src2 = go.GetComponent<WallScr>();
            src2.IsSelected1 = false;
            go = Instantiate(wall, new Vector3(-31.7f, 5.2f, 0f), Quaternion.Euler(0, 0, -48.75f));
            src2 = go.GetComponent<WallScr>();
            src2.IsSelected1 = false;
            go = Instantiate(wall, new Vector3(18.1f, 37.3f, 0f), Quaternion.Euler(0, 0, 40));
             src2 = go.GetComponent<WallScr>();
            src2.IsSelected1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
