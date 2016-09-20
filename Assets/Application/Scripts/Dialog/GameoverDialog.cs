using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;

	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text highScoreText;

	protected override void Start () {

		restartButton = GameObject.FindWithTag ("RestartButton");
		exitButton = GameObject.FindWithTag ("ExitButton");

		resultScoreText = GameObject.Find ("ResultScoreText").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();

		SetButtonsInactive ();

		base.Start ();
	}

	public override void Show() {
		base.Show ();
		SetButtonsActive ();
		resultScoreText.text = string.Format ("SCORE : {0}", ScoreManager.I.GetScore ());
		highScoreText.text = string.Format ("HIGH SCORE : {0}", ScoreManager.I.GetHighScore ());
	}

	private void SetButtonsInactive(){
		restartButton.SetActive (false);
		exitButton.SetActive (false);
	}

	private void SetButtonsActive(){
		restartButton.SetActive (true);
		exitButton.SetActive (true);
	}

	public void Restart(){
		SceneManager.LoadScene ("Game");
	}

	public void Exit(){
		SceneManager.LoadScene ("Title");
	}
}
