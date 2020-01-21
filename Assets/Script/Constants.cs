﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
	//Prefabs paths
	public const string ballObjectPath = "prefabs/Ball";
	public const string gridObjectPath = "prefabs/Grid";

	//Game constant Values
	public const int maxBallSpawnCount = 10;
	public const float gridPositionOffset = 1.5f;
	public const int gridNumberOffset_Row = 2;
	public const int gridNumberOffset_Column = 1;


	//Save game constant Keys
	public const string saveHighScore = "highScore";


    //Game elements values
	public enum TAG
	{
		Ground,
		none
	};
}
