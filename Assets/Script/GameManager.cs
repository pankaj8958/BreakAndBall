using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public bool isGameStarted;

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

        FetchSavedScore();
        StopGame();
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    public void StopGame()
    {
        isGameStarted = false;
        UIManager.instance.CreateGamePlayScreen();
    }

    public void UpdateHighScore (int highScore, int time)
    {
        if (highScore > gameData.highScore)
        {
            gameData.highScore = highScore;
            gameData.timeTaken = time;
            SaveScore();
        }
    }

    private void FetchSavedScore()
    {
        gameData = new GameData(0, 0);
        if (PlayerPrefs.HasKey(Constants.saveHighScore))
        {
            gameData.highScore = PlayerPrefs.GetInt(Constants.saveHighScore);
        }

        if (PlayerPrefs.HasKey(Constants.saveTime))
        {
            gameData.timeTaken = PlayerPrefs.GetInt(Constants.saveTime);
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(Constants.saveHighScore, gameData.highScore);
        PlayerPrefs.SetInt(Constants.saveTime, gameData.timeTaken);
    }

}

public class GameData
{
    public int highScore;
    public int timeTaken;
    public GameData () { }
    public GameData(int score, int time)
    {
        highScore = score;
        timeTaken = time;
    }

}
