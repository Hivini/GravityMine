using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public GameObject EnterPortal;
    public GameObject ExitPortal;
    public GameObject blackHole;
    public GameObject exit;
    Queue<GameObject> instantiated;
    GameObject go, en, ex;
    void Awake()
    {
        instantiated = new Queue<GameObject>();
    }
    // Start is called before the first frame update
    public void LoadLev(int level)
    {
        while (instantiated.Count > 0)
        {
            go = instantiated.Dequeue();
            if(go != null)
            {
                Destroy(go);
            }
            
        }

        switch (level)
        {
            case 1:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0,0, 270)));
                exit.transform.position=new Vector3(-48.7f, 33.7f,0);
                break;
            case 2:
                go = Instantiate(blackHole, new Vector3(18.3f, 23, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(    -24.6f, 14.8f,  0), Quaternion.Euler(new Vector3(0, 0, -55f)), new Vector3(   36.5f, 16.9f  , 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(     36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(      -4.8f, 10.8f,0), Quaternion.Euler(new Vector3(130, 270f, 0)));
                portalCouple(new Vector3(    -49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(      -12.7f,39.8f ,0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(     20.8f, 7.6f , 0), Quaternion.Euler(new Vector3(0, 0, 60)), new Vector3(      -31.5f, 23.3f , 0), Quaternion.Euler(new Vector3(41.23f, 270, 0)));
                portalCouple(new Vector3(     -50.5f, 31.4f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(      1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(31.8f, 42.3f, 0);
                break;
            case 3:
                go = Instantiate(blackHole, new Vector3(28.6f, 16.7f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, 0,0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-18.5f, 44, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-13.2f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(23.2f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(40.6f, 30.3f, 0);
                break;
                /*
            case 4:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 5:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 6:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 7:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 8:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 9:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
            case 10:
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                go = Instantiate(blackHole, new Vector3(-37.52f, 35.8f, 0), Quaternion.Euler(0, 0, 0));
                instantiated.Enqueue(go);
                portalCouple(new Vector3(-49.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(19.3f, 32f, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                portalCouple(new Vector3(-24.1f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-31.5f, 23.3f, 0), Quaternion.Euler(new Vector3(41.237f, 270f, 0)));
                portalCouple(new Vector3(-13.2f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(5.9f, 17.6f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(1.9f, -0.9f, 0), Quaternion.Euler(new Vector3(0, 0, 0)), new Vector3(-26.2f, 16.9f, 0), Quaternion.Euler(new Vector3(130, 270, 0)));
                portalCouple(new Vector3(36.5f, 35.2f, 0), Quaternion.Euler(new Vector3(0, 0, 120)), new Vector3(1.3f, 30.6f, 0), Quaternion.Euler(new Vector3(0, 0, 270)));
                exit.transform.position = new Vector3(-48.7f, 33.7f, 0);
                break;
                */
            default:

                break;
        }
    }
    void portalCouple(Vector3 ent,Quaternion qent ,Vector3 exit, Quaternion qexit)
    {
        en = Instantiate(EnterPortal, ent, qent);
        instantiated.Enqueue(en);
        ex = Instantiate(ExitPortal, exit, qexit);
        instantiated.Enqueue(ex);
        var tel = en.GetComponent<Teletransport>();
        tel.exitPortal = ex;
    }

}
