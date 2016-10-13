using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public enum GAMESCENETYPE : int {
	TITLE = 0,
	GAME = 1,
	CREDIT = 2,
}


public class AppSceneManager : SingletonBehaviour<AppSceneManager> {

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (gameObject);
	}

	public void GoTitle(){
		FreeMemory ();
		SceneManager.LoadScene ("Title");
	}

	public void GoGame(){
		FreeMemory ();
		SceneManager.LoadScene ("Game");
	}

	public void GoCredit(){
		FreeMemory ();
		SceneManager.LoadScene ("Credit");
	}

	private void FreeMemory(){
		Resources.UnloadUnusedAssets ();
		GC.Collect ();
	}
}
