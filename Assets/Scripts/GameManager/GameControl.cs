﻿using System.Collections;
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
    public Dictionary<string, bool> passedLevels;
    public Dictionary<string, int> levelsScores;
    public bool hasPos;
    public int scene; // <--- TODO Later implementation to know in which pointsToCollect is

    // Start is called before the first frame update
    void Awake()
    {
        hasPos = false;
        passedLevels = new Dictionary<string, bool>();
        levelsScores = new Dictionary<string, int>();

        // Dictionary of levels
        passedLevels.Add("Final_Minigame_A", false);
        passedLevels.Add("Final_Minigame_B", false);
        passedLevels.Add("Final_Minigame_C", false);

        levelsScores.Add("Final_Minigame_A", 0);
        levelsScores.Add("Final_Minigame_B", 0);
        levelsScores.Add("Final_Minigame_C", 0);

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
            playerBZ = playerZ,
            passedLevels = passedLevels,
            levelsScores = levelsScores
        };

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved succesfully");
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            // Create the class again with the data using casting
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerX = data.playerBX;
            playerY = data.playerBY;
            playerZ = data.playerBZ;
            passedLevels = data.passedLevels;
            hasPos = true;
            Debug.Log("Loaded succesfully");
        }
    }


    public void FinishMinigame(string sceneName, int score)
    {
        if (levelsScores[sceneName] < score)
        {
            levelsScores[sceneName] = score;
        }

        if (!passedLevels[sceneName])
        {
            passedLevels[sceneName] = true;
        }
    }
}

[System.Serializable]
class PlayerData
{
    public float playerBX;
    public float playerBY;
    public float playerBZ;
    public Dictionary<string, bool> passedLevels;
    public Dictionary<string, int> levelsScores;
}