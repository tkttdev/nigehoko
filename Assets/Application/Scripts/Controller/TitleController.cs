using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : SingletonBehaviour<TitleController> {

	[SerializeField] private Text bestScoreText;
	private Text tapText;

	protected override void Initialize (){
		bestScoreText = GameObject.Find ("BestScoreText").GetComponent<Text> ();
		bestScoreText.text = string.Format ("BEST SCORE : {0}", ScoreManager.I.GetHighScore());


		tapText = GameObject.Find ("TapText").GetComponent<Text> ();
		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));
	}

	public void GameButton(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoGame ();
	}

	public void CreditButton(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoCredit ();
	}

	public void HowToButton(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoHowTo ();
	}

	public void RankingButton(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoRanking ();
	}
}
