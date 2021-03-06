﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerBall : MonoBehaviour
{
	Transform ballTrans;
	Rigidbody2D ballRigidbody;
	float speed = 0;

    // Start is called before the first frame update
    void Awake()
    {
		speed = Constants.ballSpeed;
		ballTrans = this.gameObject.GetComponent<Transform>();
	}

	public void HideObject ()
	{
		this.gameObject.SetActive(false);
		if (BallSpawner.spawnBallCount > 0)
			BallSpawner.spawnBallCount--;		
	}

	public void ShowObject ()
	{
		this.gameObject.SetActive(true);
	}

	public void ShowObject(Vector2 newPos)
	{
		this.transform.position = newPos;
		this.gameObject.SetActive(true);
		
	}

	public void AddForceToDirection(Vector2 direction)
	{
		if (!ballRigidbody)
			ballRigidbody = this.GetComponent<Rigidbody2D>();
		ballRigidbody.AddForce(direction * speed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			HideObject();
		}
	}
}
