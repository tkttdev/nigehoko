using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;
	[SerializeField] private GameObject retryButton;
		
	[SerializeField] private Text resultScoreText;
	[SerializeField] private Text bestScoreText;
	[SerializeField] private Text countText;

	[SerializeField] private Text rankText;

	[SerializeField] private GameObject backgroundImage;

	[SerializeField] private bool isFirst = true;

	protected override void Start () {

		restartButton = GameObject.FindWithTag ("RestartButton");
		exitButton = GameObject.FindWithTag ("ExitButton");
		retryButton = GameObject.Find ("RetryButton");

		rankText = GameObject.Find ("RankText").GetComponent<Text> ();
		resultScoreText = GameObject.Find ("ResultScoreText").GetComponent<Text> ();
		bestScoreText = GameObject.Find ("BestScoreText").GetComponent<Text> ();
		countText = GameObject.Find ("CountText").GetComponent<Text> ();

		backgroundImage = GameObject.FindGameObjectWithTag ("GameOverDialogBackground");

		SetComponentsInactive ();

		isFirst = true;

		base.Start ();
	}

	public override void Show() {
		base.Show ();
		SetComponentsActive ();
		countText.enabled = false;
		resultScoreText.text = string.Format ("{0} cm 生きのびた!", ScoreManager.I.GetScore ());
		bestScoreText.text = string.Format ("BEST : {0} cm", ScoreManager.I.GetHighScore ());
		if (isFirst) {
			isFirst = false;
		} else {
			restartButton.transform.localPosition = new Vector3 (restartButton.transform.localPosition.x, 160, restartButton.transform.localPosition.z);
			exitButton.transform.localPosition = new Vector3 (exitButton.transform.localPosition.x, 160, exitButton.transform.localPosition.z);
			retryButton.SetActive (false);
		}
	}

	private void SetComponentsInactive(){
		restartButton.SetActive (false);
		exitButton.SetActive (false);
		backgroundImage.SetActive (false);
		retryButton.SetActive (false);
	}

	private void SetComponentsActive(){
		restartButton.SetActive (true);
		exitButton.SetActive (true);
		backgroundImage.SetActive (true);
		retryButton.SetActive (true);
	}

	public void Restart(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoGame ();
	}

	public void Exit(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoTitle ();
	}

	public void Retry(){
		this.Hide ();
		SetComponentsInactive ();
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
