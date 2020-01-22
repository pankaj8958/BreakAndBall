using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
	public Text scoreText;
	public Text timerText;
	public Transform canvasTrans;
	public GameObject hudPanel;
	public int highScore, timeTaken;

	[HideInInspector]
	public static UIManager instance;

	public int gridCount = 0;
	bool isGameFinish = false;

	private int countVar;
	private GameObject mainGameplayPanel;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		} else
		{
			Destroy(this.gameObject);
			return;
		}
		InitUIValue();

	}

	DateTime timerCurrent;
	DateTime timerPre;

	private void Update()
	{
		if(GameManager.instance.isGameStarted)
		{
			if (timerCurrent == null)
			{
				timerPre = timerCurrent = DateTime.Now;
			} else
			{
				timerCurrent = DateTime.Now;
				TimeSpan differenceTime = (timerCurrent - timerPre);
				if(differenceTime.TotalSeconds > 1)
				{
					UpdateTime(+1);
					timerPre = timerCurrent;
				}
			}
		}
	}

	public void CreateGamePlayScreen ()
	{
		if(mainGameplayPanel != null)
		{
			GameObject panelObject = Instantiate(mainGameplayPanel, canvasTrans);
			panelObject.GetComponent<GamePlayPanel>().TextBasedOnScreen();
		}
	}

	public void CreateLevelLooseScreen ()
	{
		if (mainGameplayPanel != null)
		{
			GameObject panelObject = Instantiate(mainGameplayPanel, canvasTrans);
			panelObject.GetComponent<GamePlayPanel>().TextBasedOnScreen(true);
		}
	}

	private void InitUIValue (bool isRetry = false)
	{
		UpdateHudStatus(false);
		gridCount = 0;
		highScore = 0;
		timeTaken = 0;
		countVar = 0;
		isGameFinish = false;
		
		UpdateUIHudScore();
		UpdateUIHudTimer();
		if(mainGameplayPanel == null)
			mainGameplayPanel = Resources.Load(Constants.screenGamePlayPanel_UI) as GameObject;
		if (isRetry)
			CreateLevelLooseScreen();
		else
			CreateGamePlayScreen();

		GameManager.instance.currentLevelData = new GameData(0, 0);
	}

	public void UpdateHudStatus (bool enable)
	{
		hudPanel.SetActive(enable);
	}

	public void UpdateScore (int multiple)
	{
		if (highScore == 0)
			highScore = 1;

		countVar += multiple;

		highScore += (countVar * Constants.scoreCountMultiplier);
		UpdateUIHudScore();
	}

	public void FinalScore ()
	{
		highScore -= (timeTaken * Constants.scoreTimeCountMultiplier);
		UpdateUIHudScore();
	}

	void UpdateUIHudScore()
	{
		scoreText.text = highScore.ToString();
	}

	void UpdateTime(int sec)
	{
		timeTaken += sec;
		UpdateUIHudTimer();
	}

	void UpdateUIHudTimer()
	{
		timerText.text = GetFormattedTime(timeTaken);
	}

	string GetFormattedTime (int time)
	{
		string timeString = string.Empty;
		
		TimeSpan timeSpanVal = TimeSpan.FromSeconds(time);
		if(timeSpanVal != null)
		{
			timeString = (timeSpanVal.Minutes < 10 ? "0" + timeSpanVal.Minutes : timeSpanVal.Minutes.ToString() )+ 
				":" + (timeSpanVal.Seconds < 10 ? "0" + timeSpanVal.Seconds : timeSpanVal.Seconds.ToString());
		}

		return timeString;
	}

	#region Grid Data

	public void UpdateGridCount (int val, bool isGridRemoved = false)
	{
		gridCount += val;
		if (isGridRemoved && gridCount <= 0)
		{
			isGameFinish = true;
		}
	}

	void GameFinishAction ()
	{
		FinalScore();
		GameManager.instance.UpdateHighScore(highScore, timeTaken);
		GameManager.instance.StopGame();
		InitUIValue();
		GameManager.instance.SwitchScene(Constants.SCENE_NAME.MainMenuScene);
		
	}

	public void GameStateChange()
	{
		if(isGameFinish)
		{
			GameFinishAction();
		} else
		{
			GameLooseAction();
		}
	}

	void GameLooseAction()
	{
		FinalScore();
		GameManager.instance.currentLevelData = new GameData(0, 0);
		GameManager.instance.StopGame();
		InitUIValue(true);
		GameManager.instance.SwitchScene(Constants.SCENE_NAME.MainMenuScene);
	}

	#endregion
}
