﻿using UnityEngine;
using System.Collections;

public class SoccerBallComponent : MonoBehaviour {

    private int dir;

	[Range(50,360)]
	[SerializeField] private float perRotateZ = 90;
	[Range(0.1f,2.0f)]
	[SerializeField] private float thresholdY = 1.5f;
	[Range(1.0f,2.0f)]
	[SerializeField] private float speed = 1;
	private int addForceNum = 1300000;
	private Rigidbody2D ballRigidbody2D;


	private float rotateZ = 0;

	void Start () {
		if (this.gameObject.transform.position.x < 0)
		{
			dir = -1;
		}
		else
		{
			dir = 1;
		}
		rotateZ = 0;
		ballRigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		addForceNum = (int)speed * 1000000;
	}

	void Update(){
		if (this.gameObject.transform.position.y - Camera.main.transform.position.y <= thresholdY && GameManager.I.IsPlaying ()) {
			if (ballRigidbody2D.velocity.x == 0) {
				if (dir == -1) {
					ballRigidbody2D.AddForce (new Vector2 (addForceNum, 0));
				} else {
					ballRigidbody2D.AddForce (new Vector2 (-addForceNum, 0));
				}
			}

			if (dir == -1) {
				rotateZ -= perRotateZ * Time.deltaTime;
			} else {
				rotateZ += perRotateZ * Time.deltaTime;
			}
			gameObject.transform.rotation = Quaternion.Euler (0, 0, rotateZ);
		} else if (!GameManager.I.IsPlaying ()) {
			if (ballRigidbody2D.velocity.x != 0) {
				ballRigidbody2D.velocity = Vector2.zero;
			}
		}
	}
}

