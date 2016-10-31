﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;
	[SerializeField] private GameObject retryButton;
	[SerializeField] private GameObject shareButton;
		
	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text bestScoreText;
	[SerializeField] private Text countText;

	[SerializeField] private Text rankText;

	[SerializeField] private GameObject backgroundImage;

	[SerializeField] private bool isFirst = true;

	protected override void Start () {

		restartButton = GameObject.Find ("RestartButton");
		exitButton = GameObject.Find ("ExitButton");
		retryButton = GameObject.Find ("RetryButton");
		shareButton = GameObject.Find ("ShareButton");

		rankText = GameObject.Find ("RankText").GetComponent<Text> ();
		resultScoreText = GameObject.Find ("ResultScoreText").GetComponent<Text> ();
		bestScoreText = GameObject.Find ("BestScoreText").GetComponent<Text> ();
		countText = GameObject.Find ("CountText").GetComponent<Text> ();

		backgroundImage = GameObject.FindGameObjectWithTag ("GameOverDialogBackground");

		SetComponentsInactive ();

		isFirst = true;

		countText.enabled = false;

		base.Start ();
	}

	public override void Show() {
		base.Show ();
		SetComponentsActive ();
		resultScoreText.text = string.Format ("{0} cm 生きのびた!", ScoreManager.I.GetScore ());
		bestScoreText.text = string.Format ("BEST : {0} cm", ScoreManager.I.GetHighScore ());
		if (isFirst) {
			isFirst = false;
		} else {
			restartButton.transform.localPosition = new Vector3 (restartButton.transform.localPosition.x, 160, restartButton.transform.localPosition.z);
			exitButton.transform.localPosition = new Vector3 (exitButton.transform.localPosition.x, 160, exitButton.transform.localPosition.z);
			shareButton.transform.localPosition = new Vector3 (shareButton.transform.localPosition.x, 160, shareButton.transform.localPosition.z);
			retryButton.SetActive (false);
		}
		CheckRank ();
	}

	public override void Hide () {
		base.Hide ();
		SetComponentsInactive ();
	}

	private void CheckRank(){
		int rank = ScoreManager.I.GetScore () / 400;
		switch (rank) {
		case 0:
			rankText.text = "D";
			rankText.color = new Color (142.0f/255.0f,  62.0f/255.0f,   3.0f/255.0f);
			break;
		case 1:
			rankText.text = "C";
			rankText.color = new Color (255.0f/255.0f, 204.0f/255.0f,   0.0f/255.0f);
			break;
		case 2:
			rankText.text = "B";
			rankText.color = new Color (  0.0f/255.0f,   0.0f/255.0f, 255.0f/255.0f);
			break;
		case 3:
			rankText.text = "A";
			rankText.color = new Color (142.0f/255.0f,  62.0f/255.0f,   3.0f/255.0f);
			break;
		case 4:
			rankText.text = "S";
			rankText.color = new Color (174.0f/255.0f,  68.0f/255.0f, 154.0f/255.0f);
			break;
		case 5:
			rankText.text = "SS";
			rankText.color = new Color (174.0f/255.0f,  68.0f/255.0f, 154.0f/255.0f);
			break;
		case 6:
			rankText.text = "SSS";
			rankText.color = new Color (174.0f/255.0f,  68.0f/255.0f, 154.0f/255.0f);
			break;
		}
	}

	private void SetComponentsInactive(){
		restartButton.SetActive (false);
		exitButton.SetActive (false);
		backgroundImage.SetActive (false);
		retryButton.SetActive (false);
		shareButton.SetActive (false);
	}

	private void SetComponentsActive(){
		restartButton.SetActive (true);
		exitButton.SetActive (true);
		backgroundImage.SetActive (true);
		retryButton.SetActive (true);
		shareButton.SetActive (true);
	}

	public void Restart(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoGame ();
	}

	public void Exit(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoTitle ();
	}

	public void ShareSNS(){
		UniTwitter.Share (string.Format ("{0} cm 生き延びたよ!!¥n", ScoreManager.I.GetScore ()));
	}

	public void Retry(){
		Hide ();
		AdsManager.I.ShowRewardedAd ();
	}

	public void StartRetryCount(){
		StartCoroutine (RetryCount ());
	}

	IEnumerator RetryCount(){
		countText.enabled = true;
		for (int count = 3; count > 0; count--) {
			countText.text = count.ToString ();
			yield return new WaitForSeconds (1.0f);
		}
		countText.text = "GO!";
		yield return new WaitForSeconds (0.5f);
		GameManager.I.SetStatePlaying ();
		countText.enabled = false;
		yield break;
	}
}
