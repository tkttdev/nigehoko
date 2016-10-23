using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryDialog : DialogBase {

	private int time = 3;
	private Text countText;
	private Text retryText;

	[SerializeField] private GameObject retryButton;
	[SerializeField] private GameObject backgroundImage;
	[SerializeField] private Text startCountText;

	private Coroutine countCorutine;

	protected override void Start () {
		base.Start ();
		countText = GameObject.Find ("CountText").GetComponent<Text> ();
		retryButton= GameObject.FindGameObjectWithTag ("RetryButton");
		backgroundImage = GameObject.FindGameObjectWithTag ("RetryDialogBackground");
		startCountText = GameObject.Find ("StartCountText").GetComponent<Text> ();
		retryText = GameObject.Find ("RetryText").GetComponent<Text> ();
		startCountText.gameObject.SetActive (false);
		time = 3;
		SetComponentsInactive ();
	}

	public override void Show() {
		base.Show ();
		SetComponentsActive ();
		countCorutine = StartCoroutine (Count ());
	}

	public override void Hide () {
		base.Hide ();
		SetComponentsInactive ();
	}

	private void SetComponentsInactive(){
		retryButton.SetActive (false);
		backgroundImage.SetActive (false);
		retryText.gameObject.SetActive (false);
		countText.gameObject.SetActive (false);
	}

	private void SetComponentsActive(){
		retryButton.SetActive (true);
		backgroundImage.SetActive (true);
		retryText.gameObject.SetActive (true);
		countText.gameObject.SetActive (true);
	}

	public void Retry(){
		AdsManager.I.ShowRewardedAd ();
		SetComponentsInactive ();
	}

	IEnumerator Count(){
		countText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		time--;
		countText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		time--;
		countText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		Hide ();
        UIManager.I.gameOverDialog.Show();
		yield break;
	}

	public void StartCorutineCount(){
		StartCoroutine (StartCount ());
	}

	IEnumerator StartCount(){
		startCountText.gameObject.SetActive (true);
		startCountText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		time--;
		startCountText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		time--;
		startCountText.text = time.ToString ();
		yield return new WaitForSeconds (1.0f);
		Hide ();
		GameManager.I.SetStatePlaying ();
		yield break;
	}

}
