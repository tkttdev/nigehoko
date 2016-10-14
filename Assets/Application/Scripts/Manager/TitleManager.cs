using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : SingletonBehaviour<TitleManager> {

	[SerializeField] private AudioClip startSE;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private Text bestScoreText;
	[SerializeField] private bool isTouch = false;
	private Text tapText;

	protected override void Initialize (){
		bestScoreText = GameObject.Find ("BestScoreText").GetComponent<Text> ();
		bestScoreText.text = string.Format ("Best SCORE : {0}", ScoreManager.I.GetHighScore());


		tapText = GameObject.Find ("TapText").GetComponent<Text> ();
		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !isTouch && !Application.isShowingSplashScreen) {

			isTouch = true;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 20.0f,1);

			Debug.Log (hit.collider);

			if (hit.collider == null) {
				SoundManager.I.ButtonSE ();
				AppSceneManager.I.GoGame ();
			} else if (hit.collider.transform.tag == "CreditButton") {
				SoundManager.I.ButtonSE ();
				AppSceneManager.I.GoCredit ();
			} else if (hit.collider.transform.tag == "HowToButton") {
				SoundManager.I.ButtonSE ();
				AppSceneManager.I.GoHowTo ();
			}
				
		}
	}
}
