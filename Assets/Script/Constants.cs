using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
	//Prefabs paths
	public const string ballObjectPath = "prefabs/Ball";
	public const string gridObjectPath = "prefabs/Grid";
	public const string screenGamePlayPanel_UI = "prefabs/UI/GamePlayPanel";

	//Game constant Values
	public const int maxBallSpawnCount = 60;
	public const float ballSpeed = 4.5f;
	public const int platformCount = 20;
	public const int scoreCountMultiplier = 5;
	public const int scoreTimeCountMultiplier = 2;
	public const float gridPositionOffset = 1.5f;
	public const int gridNumberOffset_Row = 2;
	public const int gridNumberOffset_Column = 1;



	//Save game constant Keys
	public const string saveHighScore = "highScore";
	public const string saveTime = "highScoreTime";


	//Game elements values
	public enum TAG
	{
		Ground,
		none
	};

	public enum SCENE_NAME
	{
		MainMenuScene,
		GamePlayScene
	}

	public enum SPAWNER_STATE
	{
		ideal,
		shotting,
		shooted,
		finish,
		none
	}
}
