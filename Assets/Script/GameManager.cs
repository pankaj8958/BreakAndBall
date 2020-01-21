using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameData gameData;

    public static GameManager instance;

    public int currentLevel;
    public List<LevelData> levelDataList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Init();
    }

    private void Init()
    {
        gameData = new GameData(0);
        if(PlayerPrefs.HasKey(Constants.saveHighScore))
        {
            gameData.highScore = PlayerPrefs.GetInt(Constants.saveHighScore);
        }
    }

    public void UpdateHighScore (int highScore)
    {
        gameData.highScore = highScore;
    }

}

class GameData
{
    public int highScore;

    public GameData () { }
    public GameData(int score)
    {
        highScore = score;
    }

}
