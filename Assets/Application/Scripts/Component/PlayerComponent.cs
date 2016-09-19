﻿using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

	private float speed = 5.0f;
	private float scale = 1.0f;
	private float threshold = 0.2f;
	private float limitScale = 1.5f;
	private float lowestScale = 0.05f;
	[SerializeField] private float reduceNum = 0.05f; 
	[SerializeField] private float increaseNum = 0.3f;

	private GameObject eleDust;

	private AudioClip tapHit;
	private AudioClip tapMiss;
	private AudioSource audioSource;

	public bool isMove = false;

	public float desX = 0.0f;
	public float desY = 0.0f;

	public float disX;
	public float disY;

	public float dis;

	public Vector3 worldPos;

	float maxDistance = 10.0f;
	int layerMask = 1;

	void Start () {
		desX = gameObject.transform.position.x;
		desY = gameObject.transform.position.y;

		scale = gameObject.transform.localScale.x;

		eleDust = Resources.Load ("Prefabs/EleDust") as GameObject;

		tapHit = Resources.Load ("SE/TapHit") as AudioClip;
		tapMiss = Resources.Load ("SE/TapMiss") as AudioClip;

		audioSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, maxDistance,layerMask);

			if (hit.collider) {
				if (hit.collider.transform.tag == "Player") {
					audioSource.PlayOneShot (tapMiss);
					return;
				}
			}

			audioSource.PlayOneShot (tapHit);

			worldPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));

			ObjectManager.I.ActiveEleDust (worldPos);

			desX = worldPos.x;
			desY = worldPos.y;

			isMove = true;
		}
		if (isMove) {
			disX = desX - gameObject.transform.position.x;
			disY = desY - gameObject.transform.position.y;

			dis = Mathf.Abs (disX) + Mathf.Abs (disY);
			scale -= reduceNum;

			if (dis < threshold) {
				gameObject.transform.position = new Vector3 (desX, desY, 0);
				scale += increaseNum;
				isMove = false;
			}
		
			gameObject.transform.position += new Vector3 (disX / dis * speed*Time.deltaTime, disY / dis * speed*Time.deltaTime, 0);
		}

		if (scale > limitScale) {
			scale = limitScale;
		}

		gameObject.transform.localScale = new Vector3 (scale, scale, 1);

		CheckScale ();
	}



	void CheckScale(){
		if (gameObject.transform.localScale.x < lowestScale) {
			Debug.Log ("GameOver");
			Destroy (gameObject);
			GameManager.I.SetStateEnd ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.tag == "EleDust") {
			//Destroy (other.gameObject);
			other.gameObject.SetActive(false);
		} else {
			Debug.Log ("GameOver");
			Destroy (gameObject);
		}
	}

}
