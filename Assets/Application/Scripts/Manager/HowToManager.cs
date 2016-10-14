using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToManager : SingletonBehaviour<HowToManager> {

	private Text tapText;

	protected override void Initialize(){
		tapText = GameObject.Find ("TapText").GetComponent<Text> ();

		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));
	}

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			SoundManager.I.ButtonSE ();
			AppSceneManager.I.GoTitle ();
		}
	}
}
