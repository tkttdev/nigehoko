using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public enum GAMESCENETYPE : int {
	TITLE = 0,
	GAME = 1,
	CREDIT = 2,
	HOWTO = 3,
	RANKING = 4,
}


public class AppSceneManager : SingletonBehaviour<AppSceneManager> {

	private int moveSceneNum;

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (this);
	}

	public void GoTitle(){
		FreeMemory ();
		SceneManager.LoadScene ((int)GAMESCENETYPE.TITLE);
	}

	public void GoGame(){
		FreeMemory ();
		SceneManager.LoadScene ((int)GAMESCENETYPE.GAME);
	}

	public void GoCredit(){
		FreeMemory ();
		SceneManager.LoadScene ((int)GAMESCENETYPE.CREDIT);
	}

	public void GoHowTo(){
		FreeMemory ();
		SceneManager.LoadScene ((int)GAMESCENETYPE.HOWTO);
	}

	public void GoRanking(){
		FreeMemory ();
		SceneManager.LoadScene ((int)GAMESCENETYPE.RANKING);
	}

	private void FreeMemory(){
		Resources.UnloadUnusedAssets ();
		GC.Collect ();
	}
}
