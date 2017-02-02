using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType : int {
	TITLE = 0,
	GAME = 1,
	CREDIT = 2,
	HOWTO = 3,
	RANKING = 4,
}


public class AppSceneManager : SingletonBehaviour<AppSceneManager> {

	private int moveSceneNum;
	[SerializeField] private Canvas canvas;
	[SerializeField] private Image fadeImage;
	[SerializeField] private FadeScene fadeScene;

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (this);
	}

	public void GoScene(SceneType sceneType = SceneType.TITLE){
		
		SceneManager.LoadScene ((int)sceneType);	
	}
}
