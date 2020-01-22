using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameData gameSavedData;
    public GameData currentLevelData;

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
        PlayerPrefs.DeleteAll();
        FetchSavedScore();
        StopGame();
    }

    public void StartGame()
    {
        isGameStarted = true;
        UIManager.instance.UpdateHudStatus(true);
    }

    public void StopGame()
    {
        isGameStarted = false;

    }

	#region Player data Save and fetch
	public void SwitchScene(Constants.SCENE_NAME sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    public void UpdateHighScore (int highScore, int time)
    {
        currentLevelData.score = highScore;
        currentLevelData.timeTaken = time;

        if (highScore > gameSavedData.score)
        {
            gameSavedData.score = highScore;
            gameSavedData.timeTaken = time;
            SaveScore();
        }
    }

    private void FetchSavedScore()
    {
        gameSavedData = new GameData(0, 0);
        if (PlayerPrefs.HasKey(Constants.saveHighScore))
        {
            gameSavedData.score = PlayerPrefs.GetInt(Constants.saveHighScore);
        }

        if (PlayerPrefs.HasKey(Constants.saveTime))
        {
            gameSavedData.timeTaken = PlayerPrefs.GetInt(Constants.saveTime);
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(Constants.saveHighScore, gameSavedData.score);
        PlayerPrefs.SetInt(Constants.saveTime, gameSavedData.timeTaken);
    }
	#endregion
}

public class GameData
{
    public int score;
    public int timeTaken;
    public GameData () { }
    public GameData(int _score, int _time)
    {
        score = _score;
        timeTaken = _time;
    }

}
