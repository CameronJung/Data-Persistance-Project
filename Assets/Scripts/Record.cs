using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Record : MonoBehaviour
{

    const string FILENAME = "/SaveData.json";

    public static Record instance;

    public string player_name;
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
            highScore = 0;
            champion = "Nobody";
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
    
    public void SaveHighScore(int newHighScore)
    {
        SaveData saveData = new SaveData();
        saveData.achievement = newHighScore;
        saveData.achiever = player_name;
    }
}
