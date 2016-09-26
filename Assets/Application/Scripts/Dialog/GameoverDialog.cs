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
		resultScoreText.text = string.Format ("{0} cm 生きのびた!", ScoreManager.I.GetScore ());
		highScoreText.text = string.Format ("BEST : {0} cm", ScoreManager.I.GetHighScore ());
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
		Debug.Log ("TTT");
	}

	public void Exit(){
		SceneManager.LoadScene ("Title");
		Debug.Log ("TTT");
	}
}
