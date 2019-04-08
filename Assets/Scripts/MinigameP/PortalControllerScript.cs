using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControllerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject xp, ep;
    Animator xanim, eanim;
    public GameObject currentEnter, currentExit;
    int currentLevel;
    float pos_Norm_EP, pos_Norm_XP, dir_angle_XP;
     // wall: 0,1,2,3,   normalized: from -0.9 to 0.9, dir from 0 t0 45
    int wall_EP, wall_XP;
    // Start is called before the first frame update

    System.Random rand;
    void Start()
    {


        Reset();
    }

    public void Reset()
    {
        currentLevel = -1;
        Upgrade();
    }

    public void Upgrade()
    {
        rand = new System.Random();
        pos_Norm_EP = 1.8f * ((float)rand.NextDouble()) - 0.9f;
        wall_EP = rand.Next(0, 4);

        pos_Norm_XP = 1.8f * ((float)rand.NextDouble()) - 0.9f;
        wall_XP = rand.Next(0, 4);
        if (wall_XP == wall_EP)
        {
            wall_XP += rand.Next(1, 3);
            wall_XP %= 4;
        }
        dir_angle_XP = 45f * ((float)(rand.NextDouble()));

        currentLevel++;
        if (currentExit != null)
        {

            eanim.Play("desaparecer");
            Destroy(currentEnter,1);

        }
        if (currentExit != null)
        {
            xanim.Play("desaparecer2");
            Destroy(currentExit,1);
        }

        print("currentlevel portal: " + currentLevel);
        Vector3 position = GetPosition(pos_Norm_EP, wall_EP);
        Quaternion q = GetRotation(wall_EP);
        currentEnter = Instantiate(ep, position, q);
        eanim = currentEnter.GetComponent<Animator>();

        position = GetPosition(pos_Norm_XP, wall_XP);
        q = GetRotation(wall_XP);
        currentExit = Instantiate(xp, position, q);
        var playerScript = player.GetComponent<PlayerBehP>();
        playerScript.SetEnterPortal(currentEnter);
        playerScript.SetExitPortal(currentExit);
        xanim = currentExit.GetComponent<Animator>();
    }

    public float GetDirectionSpeedPlayer()
    {
        // in degrees.
        int cuadrante;
        if (currentExit.transform.position.y > 0)
        {
            if (currentExit.transform.position.x > 0)
            {
                cuadrante = 3;
            }
            else
            {
                cuadrante = 4;
            }
        }
        else
        {
            if (currentExit.transform.position.x > 0)
            {
                cuadrante = 2;
            }
            else
            {
                cuadrante = 1;
            }
        }
        return 90f * (cuadrante - 1) + dir_angle_XP;
    }

    Vector3 GetPosition(float position, int wall)
    {
        // par --> arriba/abajo
        // impar --> paredes laterales.s
        position *= (wall % 2 == 0) ? 7.3f : 5.3f;
        return new Vector3((wall % 2 == 0) ? position : 7.3f * (wall == 1 ? -1 : 1), (wall % 2 == 0) ? 5.3f * (wall == 0 ? 1 : -1) : position, -0.5f);

    }
    public int GetExitWall()
    {
        return wall_XP;
    }
    Quaternion GetRotation(int wall)
    {
        // par --> arriba/abajo
        // impar --> paredes laterales.s
        return (wall % 2 == 0) ? Quaternion.identity : Quaternion.Euler(0, 0, 90);

    }
}
