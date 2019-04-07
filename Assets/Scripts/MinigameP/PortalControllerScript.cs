using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControllerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject xp, ep;
    Animator xanim, eanim;
    public GameObject currentEnter, currentExit;
    public int totalNumberLevels = 20;
    int currentLevel;
    float[] pos_Norm_EP_PerLevel; // from -0.9 to 0.9)
    int[] wall_EP_PerLevel; // 0,1,2,3

    float[] pos_Norm_XP_PerLevel; // from -0.9 to 0.9)
    int[] wall_XP_PerLevel; // 0,1,2,3
    float[] dir_angle_XP_PerLevel;  // from 0 to 45 degrees
    // Start is called before the first frame update

    System.Random rand;
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentLevel = -1;

        rand = new System.Random();
        pos_Norm_EP_PerLevel = new float[totalNumberLevels];
        wall_EP_PerLevel = new int[totalNumberLevels];

        pos_Norm_XP_PerLevel = new float[totalNumberLevels];
        wall_XP_PerLevel = new int[totalNumberLevels];
        dir_angle_XP_PerLevel = new float[totalNumberLevels];

        for (int i = 0; i < totalNumberLevels; i++)
        {
            pos_Norm_EP_PerLevel[i] = 1.8f * ((float)rand.NextDouble()) - 0.9f;
            wall_EP_PerLevel[i] = rand.Next(0, 4);

            pos_Norm_XP_PerLevel[i] = 1.8f * ((float)rand.NextDouble()) - 0.9f;
            wall_XP_PerLevel[i] = rand.Next(0, 4);
            if (wall_XP_PerLevel[i] == wall_EP_PerLevel[i])
            {
                wall_XP_PerLevel[i] += rand.Next(1, 3);
                wall_XP_PerLevel[i] %= 4;
            }
            dir_angle_XP_PerLevel[i] = 45f * ((float)(rand.NextDouble()));
        }
        Upgrade();
    }

    public void Upgrade()
    {
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
        Vector3 position = GetPosition(pos_Norm_EP_PerLevel[currentLevel], wall_EP_PerLevel[currentLevel]);
        Quaternion q = GetRotation(wall_EP_PerLevel[currentLevel]);
        currentEnter = Instantiate(ep, position, q);
        eanim = currentEnter.GetComponent<Animator>();

        position = GetPosition(pos_Norm_XP_PerLevel[currentLevel], wall_XP_PerLevel[currentLevel]);
        q = GetRotation(wall_XP_PerLevel[currentLevel]);
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
        if (wall_XP_PerLevel[currentLevel] == 0)
        {
            cuadrante = (xp.transform.position.x > 0) ? 3 : 4;

        }
        else if (wall_XP_PerLevel[currentLevel] == 1)
        {
            cuadrante = (xp.transform.position.y > 0) ? 4 : 1;
        }
        else if (wall_XP_PerLevel[currentLevel] == 2)
        {
            cuadrante = (xp.transform.position.x > 0) ? 2 : 1;
        }
        else
        {
            cuadrante = (xp.transform.position.y > 0) ? 3 : 2;
        }
        return 90f * (cuadrante - 1) + dir_angle_XP_PerLevel[currentLevel];
    }

    Vector3 GetPosition(float position, int wall)
    {
        // par --> arriba/abajo
        // impar --> paredes laterales.s
        position *= (wall % 2 == 0) ? 7.3f : 5.3f;
        return new Vector3((wall % 2 == 0) ? position : 7.3f * (wall == 1 ? -1 : 1), (wall % 2 == 0) ? 5.3f * (wall == 0 ? 1 : -1) : position, -0.5f);

    }
    Quaternion GetRotation(int wall)
    {
        // par --> arriba/abajo
        // impar --> paredes laterales.s
        return (wall % 2 == 0) ? Quaternion.identity : Quaternion.Euler(0, 0, 90);

    }
}
