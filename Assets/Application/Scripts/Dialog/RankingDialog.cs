using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDialog : DialogBase {

	private GameObject backgroundImage;
	private GameObject postButton;
	private GameObject cancelButton;

	private Button _postButton;

	private InputField inputField;

	public bool isPost = false;

	private string nickname;

	protected override void Start (){
		isPost = false;
		backgroundImage = GameObject.FindGameObjectWithTag ("RankingDialogBackground");

		postButton = gameObject.transform.FindChild ("PostButton").gameObject;
		cancelButton = gameObject.transform.FindChild ("CancelButton").gameObject;

		_postButton = postButton.GetComponent<Button> ();

		inputField = gameObject.transform.FindChild ("InputField").gameObject.GetComponent<InputField> ();

		SetComponentsInactive ();
		base.Start ();
	}

	private void Update(){
		nickname = inputField.text;
		if (nickname.Length != 0) {
			_postButton.interactable = true;
		} else {
			_postButton.interactable = false;
		}
	}

	public override void Show (){
		base.Show ();
		SetComponentsActive ();
	}

	public override void Hide (){
		base.Hide ();
		SetComponentsInactive ();
	}

	private void SetComponentsActive(){
		postButton.SetActive (true);
		cancelButton.SetActive (true);
		backgroundImage.SetActive (true);
	}

	private void SetComponentsInactive(){
		postButton.SetActive (false);
		cancelButton.SetActive (false);
		backgroundImage.SetActive (false);
	}

	public void Quit(){
		Hide ();
		UIManager.I.gameOverDialog.Show ();
	}

	public void Post(){
		isPost = true;
		StartCoroutine (PostScore ());
		Quit ();
	}

	IEnumerator PostScore(){
		string url = "http://rundustfinderssrv.gq/postranking.php";
		WWW result = new WWW (url);

		yield return result;
		Debug.Log (result.text);
		yield break;
	}
}
