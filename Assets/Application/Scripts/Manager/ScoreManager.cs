using UnityEngine;
using System.Collections;

public class ScoreManager : SingletonBehaviour<ScoreManager> {

	[SerializeField] private int score;

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
			score += 10;
			UIManager.I.SetScoreText (score);
			yield return new WaitForSeconds (1.0f);
		}
	}

	private void SetHighScore(int score){
		PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
		PlayerPrefs.Save();
	}

	public int GetHighScore(){
		return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
	}
}
