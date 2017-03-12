using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour {

	private string[] data;
	private string[] names = new string[15];
	private string[] scores = new string[15];

	private Text[] nameTexts;
	private Text[] scoreTexts;

	private void Start (){
		#if UNITY_EDITOR
		if(GameObject.Find("Systems") == null){
			GameObject obj = Resources.Load("Prefabs/Systems") as GameObject;
			Instantiate(obj);
		}
		#endif
		StartCoroutine (GetRanking ());
		nameTexts = GameObject.Find ("NameTextRoot").GetComponentsInChildren<Text> ();
		scoreTexts = GameObject.Find ("ScoreTextRoot").GetComponentsInChildren<Text> ();
	}

	private void ShowRanking(){
		int i = 0;
		foreach (string d in data) {
			if (data != null) {
				if (i % 2 == 0) {
					names [i / 2] = d;	
				} else {
					scores [i / 2] = d;
				}
			}
			i++;
		}

		i = 0;
		foreach (string n in names) {
			if (n != null && n.Length != 0) {
				nameTexts [i].text = names [i];
				scoreTexts [i].text = scores [i];
				i++;
			}
		}
	}
		
	private IEnumerator GetRanking(){
		string url = "http://160.16.218.67/getranking.php";
		WWW www = new WWW (url);

		yield return www;

		string dataString = www.text.Replace("\r","").Replace("\n","");

		data = dataString.Split ('/');

		ShowRanking ();

		yield break;
	}

	public void TitleButton(){
		SoundManager.I.ButtonSE ();
		AppSceneManager.I.GoScene (SceneType.TITLE);
	}
}
