using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryDialog : DialogBase {

	private int time = 3;
	private Text countText;

	[SerializeField] private GameObject retryButton;
	[SerializeField] private GameObject backgroundImage;

	private Coroutine countCorutine;

	protected override void Start () {
		base.Start ();
		countText = GameObject.Find ("CountText").GetComponent<Text> ();
		retryButton= GameObject.FindGameObjectWithTag ("RetryButton");
		backgroundImage = GameObject.FindGameObjectWithTag ("RetryDialogBackground");
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
	}

	private void SetComponentsActive(){
		retryButton.SetActive (true);
		backgroundImage.SetActive (true);
	}

	public void Retry(){
		AdsManager.I.ShowRewardedAd ();
		Hide ();
		StopCoroutine (countCorutine);
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
		UIManager.I.gameOverDialog.Show ();
		yield break;
	}
}
