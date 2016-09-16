using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

	[SerializeField] private float speed = 5.0f;
	[SerializeField] private float scale = 10.0f;
	[SerializeField] private float threshold = 0.2f;
	[SerializeField] private float limitScale = 15.0f;
	[SerializeField] private float lowestScale = 3.0f;

	public bool isMove = false;

	public float desX = 0.0f;
	public float desY = 0.0f;

	public float disX;
	public float disY;

	public float dis;

	public Vector3 worldPos;

	float maxDistance = 20.0f;
	int layerMask = 1;

	void Start () {
		desX = gameObject.transform.position.x;
		desY = gameObject.transform.position.y;

		scale = gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			/*Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, maxDistance, layerMask);

			if (hit.collider) {
				
			}*/

			worldPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));

			desX = worldPos.x;
			desY = worldPos.y;

			isMove = true;
		}
		if (isMove) {
			disX = desX - gameObject.transform.position.x;
			disY = desY - gameObject.transform.position.y;

			dis = Mathf.Abs (disX) + Mathf.Abs (disY);

			if (dis < threshold) {
				gameObject.transform.position = new Vector3 (desX, desY, 0);
				scale += 2.0f;
				isMove = false;
			}
		
			gameObject.transform.position += new Vector3 (disX / dis * speed*Time.deltaTime, disY / dis * speed*Time.deltaTime, 0);
		}

		if (scale > limitScale) {
			scale = limitScale;
		}

		scale -= 0.05f;

		gameObject.transform.localScale = new Vector3 (scale, scale, 1);

		CheckScale ();
	}

	void CheckScale(){
		if (gameObject.transform.localScale.x < lowestScale) {
			Debug.Log ("GameOver");
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("GameOver");
		Destroy (gameObject);
	}

}
