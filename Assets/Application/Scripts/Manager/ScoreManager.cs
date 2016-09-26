using UnityEngine;
using System.Collections;

public class ScoreManager : SingletonBehaviour<ScoreManager> {

	private int score = -1;

	const string HIGH_SCORE_KEY = "highScore";

	protected override void Initialize (){
		base.Initialize ();
	}

	public void StopAddScore(){
		StopCoroutine ("AddScore");
	}

	public void StartAddScore(){
		StartCoroutine ("AddScore");
	}

	IEnumerator AddScore(){
		for (;;) {
				if (GameManager.I.IsPlaying()) {
				if (ObjectManager.I.player.transform.position.y * 10.0f > this.score) {
					this.score = (int)(ObjectManager.I.player.transform.position.y * 10.0f);
					UIManager.I.SetScoreText (score);
				}
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
