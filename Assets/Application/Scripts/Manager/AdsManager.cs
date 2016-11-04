using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : SingletonBehaviour<AdsManager> {

	private const string ADS_SHOW_TIMES_KEY = "adsShowTimesKey";
	private int adsShowTimes = 0;

	protected override void Initialize (){
		base.Initialize ();
		adsShowTimes = PlayerPrefs.GetInt (ADS_SHOW_TIMES_KEY, 0);
	}

	public void ShowRewardedAd(){
		adsShowTimes++;
		adsShowTimes %= 3;
		PlayerPrefs.SetInt (ADS_SHOW_TIMES_KEY, adsShowTimes);
		if (Advertisement.IsReady ("rewardedVideo") && adsShowTimes == 0) {
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} else {
			GameManager.I.RetryGame ();
		}
	}

	private void HandleShowResult(ShowResult result){
		switch (result){
		case ShowResult.Finished:
			GameManager.I.RetryGame ();
			break;
		case ShowResult.Skipped:
			GameManager.I.RetryGame ();
			break;
		case ShowResult.Failed:
			GameManager.I.RetryGame ();
			break;
		}
	}
}
