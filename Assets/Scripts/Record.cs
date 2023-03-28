using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Record : MonoBehaviour
{

    const string FILENAME = "/SaveData.json";

    public static Record instance;

    public string playerName;
    public int playerBest;

    public int highScore;
    public string champion;



    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            playerBest = 0;
            LoadSaveData();
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int achievement;
        public string achiever;
    }
    
    public void SaveHighScore(int newBest)
    {
        if(newBest > playerBest)
        {
            playerBest = newBest;
        }
        if(playerBest > highScore)
        {
            SaveData saveData = new SaveData();
            saveData.achievement = playerBest;
            saveData.achiever = playerName;

            champion = playerName;
            highScore = playerBest;

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + FILENAME, json);
        }
        
    }

    public void LoadSaveData()
    {
        string path = Application.persistentDataPath + FILENAME;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            champion = data.achiever;
            highScore = data.achievement;
        }
        else
        {
            highScore = 0;
            champion = "Nobody";
        }
    }
}
