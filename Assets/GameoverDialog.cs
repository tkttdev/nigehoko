using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverDialog : DialogBase {

	[SerializeField] private GameObject restartButton;
	[SerializeField] private GameObject exitButton;

	protected override void Start () {

		restartButton = GameObject.FindWithTag ("RestartButton");
		exitButton = GameObject.FindWithTag ("ExitButton");

		SetButtonsInactive ();

		base.Start ();
	}

	public override void Show() {
		base.Show ();
		SetButtonsActive ();
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
