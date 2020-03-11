using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

	GameObject ballPrefab;
	Vector2 initialPoint;
	Vector2 finalPoint;


	public static int spawnBallCount = 0;

	public Constants.SPAWNER_STATE spawnerState;

	public List<ShootingPlayerBall> shootingBallList;
    // Start is called before the first frame update
    void Start()
    {
		spawnBallCount = 0;
		spawnerState = Constants.SPAWNER_STATE.ideal;
		shootingBallList = new List<ShootingPlayerBall>();
		ballPrefab = Resources.Load(Constants.ballObjectPath) as GameObject;
    }

	private void Update()
	{
		if(spawnerState == Constants.SPAWNER_STATE.shooted)
		{
			if (spawnBallCount <= 0)
			{
				if (UIManager.instance)
					UIManager.instance.GameStateChange();
				spawnerState = Constants.SPAWNER_STATE.finish;
			}
		}
	}

	private void OnMouseDown()
	{
		if (spawnerState == Constants.SPAWNER_STATE.ideal)
		{
			spawnerState = Constants.SPAWNER_STATE.shotting;
			initialPoint = Input.mousePosition;
			initialPoint = Camera.main.ScreenToWorldPoint(initialPoint);
		}
	}

	

	private void OnMouseUp()
	{
		if (spawnerState == Constants.SPAWNER_STATE.shotting)
		{
			spawnerState = Constants.SPAWNER_STATE.shooted;
			finalPoint = Input.mousePosition;
			finalPoint = Camera.main.ScreenToWorldPoint(finalPoint);

			Vector2 ballDirection = (finalPoint - initialPoint).normalized;

			this.GetComponent<SpriteRenderer>().enabled = false;

			SpawnBallToDirection(-ballDirection);
		}
	}

	void SpawnBallToDirection (Vector2 direction)
	{
		StartCoroutine(SpawnBallInDelay(direction, 0.05f));
	}

	IEnumerator SpawnBallInDelay (Vector2 direction, float delay)
	{
		
		GameObject ballObjectClones = null;

		for (int i = 0; i < Constants.maxBallSpawnCount; i++)
		{
			if (ballPrefab)
			{
				ShootingPlayerBall shootingPlayerBall = null;
				if (shootingBallList != null && i < shootingBallList.Count)
				{
					shootingPlayerBall = shootingBallList[i];
					DisplayBallPlayer(i,this.transform.position);
				}
				else
				{
					ballObjectClones = Instantiate(ballPrefab, this.transform.position, Quaternion.identity);
					shootingPlayerBall = ballObjectClones.GetComponent<ShootingPlayerBall>();
					shootingBallList.Add(shootingPlayerBall);
				}
				shootingPlayerBall.AddForceToDirection(direction);
				spawnBallCount++;
				yield return new WaitForSeconds(delay);
			}
			else
				Debug.LogError("Ball Prefab Is Null");
		}

	}

	void DisplayBallPlayer(int index, Vector2 newPos)
	{
		if (shootingBallList != null && shootingBallList.Count > 0)
		{

			if (index < shootingBallList.Count)
				shootingBallList[index].ShowObject(newPos);

		}
	}

	public void RepositionSpawner (Vector2 newPos)
	{
		this.transform.position = newPos;
	}
}
