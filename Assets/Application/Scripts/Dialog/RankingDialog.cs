using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingDialog : DialogBase {

	private GameObject backgroundImage;
	private GameObject postButton;
	private GameObject cancelButton;

	private Text resultScoreText;

	private Button _postButton;

	private InputField inputField;

	public bool isPost = false;


	protected override void Start (){
		isPost = false;

		backgroundImage = GameObject.FindGameObjectWithTag ("RankingDialogBackground");
		postButton = gameObject.transform.FindChild ("PostButton").gameObject;
		cancelButton = gameObject.transform.FindChild ("CancelButton").gameObject;
		resultScoreText = gameObject.transform.FindChild ("ResultScoreText").gameObject.GetComponent<Text> (); 

		_postButton = postButton.GetComponent<Button> ();

		inputField = gameObject.transform.FindChild ("InputField").gameObject.GetComponent<InputField> ();

		SetComponentsInactive ();
		base.Start ();
	}

	private void Update(){
		if (inputField.text.Length != 0) {
			_postButton.interactable = true;
		} else {
			_postButton.interactable = false;
		}
	}

	public override void Show (){
		base.Show ();
		SetComponentsActive ();
		resultScoreText.text = ScoreManager.I.GetScore ().ToString();
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
		RankingManager.I.PostScore (inputField.text);
		Quit ();
	}
}
