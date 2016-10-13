using UnityEngine;
using System.Collections;

public class ScoreManager : SingletonBehaviour<ScoreManager> {

	private int score = -1;
	private Coroutine addScore;

	const string HIGH_SCORE_KEY = "highScore";

	private PlayerComponent playerComponent;

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (gameObject);
	}

	public void StartAddScore(){
		addScore = StartCoroutine (AddScore());
	}

	public void StopAddScore(){
		StopCoroutine (addScore);
	}

	IEnumerator AddScore(){
		playerComponent = ObjectManager.I.player.GetComponent<PlayerComponent> ();
		for (;;) {
			if (GameManager.I.IsPlaying()) {
				this.score = (int)(playerComponent.moveSumY * 10.0f);
				UIManager.I.SetScoreText (score);
			}
			yield return null;
		}
	}

	public int GetScore(){
		return this.score;
	}

	public void SetHighScore(){
		if (this.score > PlayerPrefs.GetInt (HIGH_SCORE_KEY, -1)) {
			PlayerPrefs.SetInt (HIGH_SCORE_KEY, score);
			PlayerPrefs.Save ();
		}
	}

	public int GetHighScore(){
		return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
	}
}
