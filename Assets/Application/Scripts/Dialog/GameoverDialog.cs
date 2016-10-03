using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;

	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text highScoreText;

	[SerializeField] private GameObject backgroundImage;

	protected override void Start () {

		restartButton = GameObject.FindWithTag ("RestartButton");
		exitButton = GameObject.FindWithTag ("ExitButton");

		resultScoreText = GameObject.Find ("ResultScoreText").GetComponent<Text> ();
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text> ();

		backgroundImage = GameObject.FindGameObjectWithTag ("GameOverDialogBackground");

		SetComponentsInactive ();

		base.Start ();
	}

	public override void Show() {
		base.Show ();
		SetComponentsActive ();
		resultScoreText.text = string.Format ("{0} cm 生きのびた!", ScoreManager.I.GetScore ());
		highScoreText.text = string.Format ("BEST : {0} cm", ScoreManager.I.GetHighScore ());
	}

	private void SetComponentsInactive(){
		restartButton.SetActive (false);
		exitButton.SetActive (false);
		backgroundImage.SetActive (false);
	}

	private void SetComponentsActive(){
		restartButton.SetActive (true);
		exitButton.SetActive (true);
		backgroundImage.SetActive (true);
	}

	public void Restart(){
		SceneManager.LoadScene ("Game");
	}

	public void Exit(){
		SceneManager.LoadScene ("Title");
	}
}
