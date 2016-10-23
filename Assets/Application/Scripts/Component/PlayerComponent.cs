using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

	private float scale = 1.0f;
	private float thresholdDis = 0.4f;
	private float limitScale = 1.5f;
	private float lowestScale = 0.05f;
	[SerializeField] private float reduceNum = 0.06f; 
	[SerializeField] private float increaseNum = 0.5f;

	private AudioClip tapHit;
	private AudioClip tapMiss;
	private AudioClip dead;
	private AudioSource audioSource;

	[SerializeField] private float desX = 0.0f;
	[SerializeField] private float desY = 0.0f;

	private float disX;
	private float disY;

	private float dis;

	private Vector3 worldPos;

	[SerializeField] private float moveDis;

	public float moveSumY = 0.0f;
	private float maxY = 0.0f;

	private float addForceNum = 200.0f;

	private float maxDistance = 10.0f;
	private int layerMask = 1;

	[SerializeField] Vector3 lastPos;

	private float velocitySum;
	private float velocityX;
	private float velocityY;

	private Rigidbody2D playerRigid2D;

	void Start () {
		desX = gameObject.transform.position.x;
		desY = gameObject.transform.position.y;

		scale = 1.0f;
		maxY = 0.0f;
		moveSumY = 0.0f;

		tapHit = Resources.Load ("SE/TapHit") as AudioClip;
		tapMiss = Resources.Load ("SE/TapMiss") as AudioClip;
		dead = Resources.Load ("SE/Dead") as AudioClip;

		audioSource = gameObject.GetComponent<AudioSource> ();
		playerRigid2D = gameObject.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {

		if (!ObjectManager.I.IsEledust() && playerRigid2D.velocity != Vector2.zero || !GameManager.I.IsPlaying()) {
			playerRigid2D.velocity = Vector2.zero;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, maxDistance,layerMask);

			if (hit.collider) {
				if (hit.transform.tag == "EleDust") {
					return;
				}
				if (hit.transform.tag == "MenuButton") {
					audioSource.PlayOneShot (tapHit);
					return;
				}
				audioSource.PlayOneShot (tapMiss);
				return;
			}

			if (!GameManager.I.IsWaiting() && !GameManager.I.IsPlaying()) {
				return;
			}

			audioSource.PlayOneShot (tapHit);

			worldPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));

			ObjectManager.I.ActiveEleDust (worldPos);

			desX = worldPos.x;
			desY = worldPos.y;

			AddForcePlayer ();

			moveDis = 0.0f;
			lastPos = gameObject.transform.position;
		}

		if (GameManager.I.IsPlaying() && ObjectManager.I.IsEledust()) {
			
			CheckVelocity ();

			disX = desX - gameObject.transform.position.x;
			disY = desY - gameObject.transform.position.y;

			dis = Mathf.Abs (disX) + Mathf.Abs (disY);

			moveDis = Mathf.Abs (gameObject.transform.position.x - lastPos.x) + Mathf.Abs (gameObject.transform.position.y - lastPos.y);

			if (gameObject.transform.position.y > maxY) {
				moveSumY += gameObject.transform.position.y - lastPos.y;
				maxY = gameObject.transform.position.y;
			}

			lastPos = gameObject.transform.position;

			scale -= reduceNum * moveDis;

			if (dis < thresholdDis) {
				ObjectManager.I.InactiveEleDust ();
				playerRigid2D.velocity = Vector2.zero;
				scale += increaseNum;
			}

			if (scale > limitScale) {
				scale = limitScale;
			}

			gameObject.transform.localScale = new Vector3 (scale, scale, 1);
			CheckScale ();
		}
	}

	private void CheckVelocity(){

		velocitySum = Mathf.Abs (playerRigid2D.velocity.x) + Mathf.Abs (playerRigid2D.velocity.y);

		if (velocitySum > 5.0f) {
			velocityX = playerRigid2D.velocity.x;
			velocityY = playerRigid2D.velocity.y;
			playerRigid2D.velocity = Vector2.zero;
			playerRigid2D.AddForce (new Vector2 (velocityX / velocitySum * addForceNum, velocityY / velocitySum * addForceNum));
		}
	}

	public void AddForcePlayer(){
		disX = desX - gameObject.transform.position.x;
		disY = desY - gameObject.transform.position.y;

		dis = Mathf.Abs (disX) + Mathf.Abs (disY);

		playerRigid2D.velocity = Vector2.zero;

		playerRigid2D.AddForce (new Vector2 (disX / dis * addForceNum, disY / dis * addForceNum));
	}

	private void CheckScale(){
		if (gameObject.transform.localScale.x < lowestScale) {
			audioSource.PlayOneShot (dead);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			playerRigid2D.velocity = Vector2.zero;
			GameManager.I.SetStateEnd ();
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (GameManager.I.IsPlaying ()) {
			OtherCollision ();
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if (GameManager.I.IsPlaying ()) {
			OtherCollision ();
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (GameManager.I.IsPlaying ()) {
			OtherCollision ();
		}
	}

	void OtherCollision(){
		disX = desX - gameObject.transform.position.x;
		disY = desY - gameObject.transform.position.y;

		dis = Mathf.Abs (disX) + Mathf.Abs (disY);

		playerRigid2D.velocity = Vector2.zero;
		playerRigid2D.AddForce (new Vector2 (disX / dis * addForceNum, disY / dis * addForceNum));

		CheckVelocity ();

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "DeadZone") {
			audioSource.PlayOneShot (dead);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			playerRigid2D.velocity = Vector2.zero;
			GameManager.I.SetStateEnd ();
		}
	}

	public void RetryInitialize(){
		scale = 1.0f;
		maxY = 0.0f;
		gameObject.transform.localScale = new Vector3 (scale, scale, 1);
		gameObject.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
}