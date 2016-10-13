using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : SingletonBehaviour<TitleManager> {

	[SerializeField] private AudioClip startSE;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private Text highScoreText;
	private bool isTouch = false;

	protected override void Initialize (){
		audioSource = gameObject.GetComponent<AudioSource> ();
		startSE = Resources.Load ("SE/decision01") as AudioClip;
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();
		highScoreText.text = string.Format ("HIGH SCORE : {0}", ScoreManager.I.GetHighScore());
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !isTouch && !Application.isShowingSplashScreen) {

			isTouch = true;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 20.0f,1);

			if (hit.collider == null) {
				audioSource.clip = startSE;
				audioSource.Play ();
				Invoke ("MoveGame", 0.4f);
				return;
			}

			if (hit.collider.transform.tag == "Button") {
				audioSource.clip = startSE;
				audioSource.Play ();
				Invoke ("MoveCredit", 0.4f);
				return;
			}
		}
	}

	void MoveGame(){
		SceneManager.LoadScene ("Game");
	}

	void MoveCredit(){
		SceneManager.LoadScene ("Credit");
	}
}
