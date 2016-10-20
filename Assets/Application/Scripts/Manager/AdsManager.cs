﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : SingletonBehaviour<AdsManager> {

	public void ShowRewardedAd(){
		if (Advertisement.IsReady("rewardedVideo")){
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result){
		switch (result){
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			StageManager.I.Retry ();
			ObjectManager.I.player.GetComponent<PlayerComponent> ().RetryInitialize ();
			UIManager.I.gameOverDialog.StartRetryCount ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
