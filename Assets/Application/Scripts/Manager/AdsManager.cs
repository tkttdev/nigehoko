using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : SingletonBehaviour<AdsManager> {

	string ADS_KEY = "adsKey";
	[SerializeField] private int adsTimes = 0;

	protected override void Initialize (){
		adsTimes = PlayerPrefs.GetInt(ADS_KEY, 0);
		base.Initialize ();
	}
	
	public void ShowAd() {
		adsTimes++;
		PlayerPrefs.SetInt (ADS_KEY, adsTimes);
		if (Advertisement.IsReady() && adsTimes % 3 == 0){
			Advertisement.Show();
		}
	}
}
