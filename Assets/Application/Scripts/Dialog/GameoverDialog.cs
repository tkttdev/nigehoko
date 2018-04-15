using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;
	[SerializeField] private GameObject retryButton;
	[SerializeField] private GameObject postButton;
	[SerializeField] private GameObject rankingButton;

	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text bestScoreText;
	[SerializeField] private Text countText;

	[SerializeField] private Text rankText;

	[SerializeField] private GameObject backgroundImage;

	[SerializeField] private bool isFirst = true;

	protected override void Start () {

		restartButton = gameObject.transform.Find ("RestartButton").gameObject;
		exitButton = gameObject.transform.Find ("ExitButton").gameObject;
		retryButton = gameObject.transform.Find ("RetryButton").gameObject;
		postButton = gameObject.transform.Find ("PostButton").gameObject;
		rankingButton = gameObject.transform.Find ("RankingButton").gameObject;

		rankText = gameObject.transform.Find ("RankText").gameObject.GetComponent<Text> ();
		resultScoreText = gameObject.transform.Find ("ResultScoreText").gameObject.GetComponent<Text> ();
		bestScoreText = gameObject.transform.Find ("BestScoreText").gameObject.GetComponent<Text> ();
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
		if (isFirst && UIManager.I.rankingDialog.isPost) {
			retryButton.transform.localPosition = new Vector3 (150, rankingButton.transform.localPosition.y, rankingButton.transform.localPosition.z);
			rankingButton.SetActive (false);
		}else if (!isFirst && !UIManager.I.rankingDialog.isPost) {
			rankingButton.transform.localPosition = new Vector3 (150, rankingButton.transform.localPosition.y, rankingButton.transform.localPosition.z);
			retryButton.SetActive (false);
		} else if (!isFirst && UIManager.I.rankingDialog.isPost) {
			retryButton.SetActive (false);
			rankingButton.SetActive (false);
			restartButton.transform.localPosition = new Vector3 (restartButton.transform.localPosition.x, 190, restartButton.transform.localPosition.z);
			exitButton.transform.localPosition = new Vector3 (exitButton.transform.localPosition.x, 190, exitButton.transform.localPosition.z);
			postButton.transform.localPosition = new Vector3 (postButton.transform.localPosition.x, 190, postButton.transform.localPosition.z);
		}
		CheckRank ();
	}

	public override void Hide () {
		base.Hide ();
		SetComponentsInactive ();
	}

	private void CheckRank(){
		int rank = ScoreManager.I.GetScore () / 500;
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
		postButton.SetActive (false);
		rankingButton.SetActive (false);
	}

	private void SetComponentsActive(){
		restartButton.SetActive (true);
		exitButton.SetActive (true);
		backgroundImage.SetActive (true);
		retryButton.SetActive (true);
		postButton.SetActive (true);
		rankingButton.SetActive (true);
	}

	public void Restart(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoScene (SceneType.GAME);
	}

	public void Exit(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoScene (SceneType.TITLE);
	}

	public void Ranking (){
		UIManager.I.rankingDialog.Show ();
		Hide ();
	}


	public void PostSNS(){
		string msg = string.Format ("{0} cm 生き延びたよ!{1}君はどれだけ生き延びれるかな？？{1}" +
			"iOS版 : https://itunes.apple.com/jp/app/taoge-qiere!hokorikun!/id1158796150?l=en&mt=8{1}" +
			"Android版 : https://play.google.com/store/apps/details?id=com.finders.rundust&hl=ja"
			, ScoreManager.I.GetScore (),Environment.NewLine);
		UniTwitter.Share (msg);
	}

	public void Retry(){
		isFirst = false;
		Hide ();
		GameManager.I.RetryGame ();
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
