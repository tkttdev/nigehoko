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
	[SerializeField] private FadeScene fadeScene;

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (this);
	}

	public void GoScene(SceneType _sceneType = SceneType.TITLE){
		fadeScene.FadeOut (()=>{
			SceneMove(_sceneType);
		});

	}

	public void SceneMove(SceneType _sceneType){
		SceneManager.LoadScene ((int)_sceneType);
	}
}
