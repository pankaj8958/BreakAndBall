using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

	GameObject ballPrefab;
	Vector2 initialPoint;
	Vector2 finalPoint;
	public List<ShootingPlayerBall> shootingBallList;
    // Start is called before the first frame update
    void Start()
    {
		shootingBallList = new List<ShootingPlayerBall>();
		ballPrefab = Resources.Load(Constants.ballObjectPath) as GameObject;
    }

	private void OnMouseDown()
	{

		initialPoint = Input.mousePosition;
		initialPoint = Camera.main.ScreenToWorldPoint(initialPoint);

	}

	private void OnMouseUp()
	{
		finalPoint = Input.mousePosition;
		finalPoint = Camera.main.ScreenToWorldPoint(finalPoint);

		Vector2 ballDirection = (finalPoint - initialPoint).normalized;

		Debug.DrawLine(initialPoint, initialPoint + ballDirection, Color.red, Mathf.Infinity);

		SpawnBallToDirection(-ballDirection);
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

	void DisplayBallPlayer(int index)
	{
		if (shootingBallList != null && shootingBallList.Count > 0)
		{

			if(index < shootingBallList.Count)
				shootingBallList[index].ShowObject();
			
		}
	}

	void DisableAllPlayerBall ()
	{
		if(shootingBallList != null && shootingBallList.Count > 0)
		{
			for (int i = 0; i < shootingBallList.Count; i++)
			{
				shootingBallList[i].HideObject();
			}
		}
	}


	public void RepositionSpawner (Vector2 newPos)
	{
		this.transform.position = newPos;
	}
}
