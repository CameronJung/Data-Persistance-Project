using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private Text recordText;
    [SerializeField] private Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        
        
        if(Record.instance.playerName != null)
        {
            nameText.text = "You are playing as: " + Record.instance.playerName;
        }
        if(Record.instance.highScore > 0)
        {
            recordText.text = ("Best Score: " + Record.instance.highScore + " Name: " + Record.instance.champion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetName(string name)
    {
        Record.instance.playerName = name;
        nameText.text = "You are playing as: " + name;
    }

}
