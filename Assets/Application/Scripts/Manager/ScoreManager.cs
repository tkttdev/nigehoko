using UnityEngine;
using System.Collections;

public class ScoreManager : SingletonBehaviour<ScoreManager> {

	static int score;
	const string HIGH_SCORE_KEY = "highScore";

	private void SetHighScore(int score){
		PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
		PlayerPrefs.Save();
	}

	public int GetHighScore(){
		return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
	}
}
