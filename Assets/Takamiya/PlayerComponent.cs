using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

	public float speed = 5.0f;

	public float desX = 0.0f;
	public float desY = 0.0f;

	public float disX;
	public float disY;

	public float dis;

	public Vector3 worldPos;

	// Use this for initialization
	void Start () {
		desX = gameObject.transform.position.x;
		desY = gameObject.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			worldPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));

			desX = worldPos.x;
			desY = worldPos.y;
		}
		if (gameObject.transform.position.x != desX && gameObject.transform.position.y != desY) {
			disX = desX - gameObject.transform.position.x;
			disY = desY - gameObject.transform.position.y;

			dis = Mathf.Abs (disX) + Mathf.Abs (disY);
			gameObject.transform.position += new Vector3 (disX / dis * speed*Time.deltaTime, disY / dis * speed*Time.deltaTime, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		Destroy (gameObject);
	}

}
