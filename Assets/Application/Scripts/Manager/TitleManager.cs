using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : SingletonBehaviour<TitleManager> {

	[SerializeField] private AudioClip startSE;
	[SerializeField] private AudioSource audioSource;
	private bool isTouch = false;

	protected override void Initialize (){
		audioSource = gameObject.GetComponent<AudioSource> ();
		startSE = Resources.Load ("SE/decision01") as AudioClip;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !isTouch) {
			isTouch = true;
			audioSource.clip = startSE;
			audioSource.Play ();
			Invoke ("MoveScene", 0.4f);
		}
	}

	void MoveScene(){
		SceneManager.LoadScene ("Game");
	}

	public void MoveCredit(){
		SceneManager.LoadScene ("Credit");
	}
}
