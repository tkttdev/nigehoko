using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager> {

	private bool isTap = false;
	[SerializeField] private Text tapText;
	Vector3 tapTextScale;

	[SerializeField] private Text scoreText;


	protected override void Initialize () {
		isTap = false;
		tapText = GameObject.Find ("TapText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
			
		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			SetStartText (false);
		}
	}

	public void SetStartText(bool active) {
		if (active) {
			tapText.color = new Color (0, 0, 0, 1);
		} else {
			iTween.Stop (tapText.gameObject, "scale");
			tapText.color = new Color (0, 0, 0, 0);
		}
	}

	public void SetScoreText(int score){
		scoreText.text = string.Format ("SCORE : {0}", score);
	}



}
