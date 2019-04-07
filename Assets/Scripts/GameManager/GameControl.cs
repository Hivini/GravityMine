using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public float playerX;
    public float playerY;
    public float playerZ;
    public bool hasPos;
    public int scene; // <--- TODO Later implementation to know in which pointsToCollect is

    // Start is called before the first frame update
    void Awake()
    {
        hasPos = false;
        if (control == null)
        {
            // Kinda like a singleton pattern
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            // Kill anyother
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        // Create the serializable class and store the data here
        PlayerData data = new PlayerData
        {
            playerBX = playerX,
            playerBY = playerY,
            playerBZ = playerZ
        };

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved succesfully");
        Debug.Log(data.playerBX);
        Debug.Log(data.playerBY);
        Debug.Log(data.playerBZ);
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            // Create the class again with the data using casting
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerX = data.playerBX;
            playerY = data.playerBY;
            playerZ = data.playerBZ;
            hasPos = true;
            Debug.Log("Loaded succesfully");
            Debug.Log(playerX);
            Debug.Log(playerY);
            Debug.Log(playerZ);
        }
    }
}

[System.Serializable]
class PlayerData
{
    public float playerBX;
    public float playerBY;
    public float playerBZ;
}