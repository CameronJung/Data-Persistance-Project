using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public Text recordText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private int num_Bricks = 0;
    private int bricksStruck = 0;

    private bool unsaved = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //The player had a chance to set a name if they don't have one
        if(Record.instance.playerName == null || Record.instance.playerName == "")
        {
            Record.instance.playerName = "Anonymous";
        }
        recordText.text = ("Best Score: " + Record.instance.highScore + " Name: " + Record.instance.champion);
        CreateLevel();
    }

    private void CreateLevel()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                num_Bricks++;
            }
        }
    }


    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point + bricksStruck;
        //add a bonus for hitting multiple bricks
        bricksStruck++;
        num_Bricks--;
        ScoreText.text = $"Score : {m_Points}";
        if(m_Points > Record.instance.highScore)
        {

            recordText.text = ("Best Score: " + m_Points + " Name: " + Record.instance.playerName);
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        Record.instance.SaveHighScore(m_Points);
        GameOverText.SetActive(true);
    }


    

    //The paddle calls this method
    public void PaddleStrike()
    {
        bricksStruck = 0;
        if (num_Bricks == 0)
        {
            //Build a new level if the bricks are all gone
            //Building the level during a paddle strike ensures the ball won't be inside the bricks
            CreateLevel();
        }
    }

    public void QuitGame()
    {
        Record.instance.SaveHighScore(m_Points);
        SceneManager.LoadScene(0);
    }
}
