using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : SingletonBehaviour<RankingManager> {
	private string name;

	protected override void Initialize (){
		base.Initialize ();
	}

	public void PostScore(string _name){
		name = _name;
		StartCoroutine (Post ());
	}

	IEnumerator Post(){
		string url = "http://rundustfinderssrv.gq/postranking.php";
		WWWForm wwwForm = new WWWForm ();
		wwwForm.AddField ("name", name);
		wwwForm.AddField ("score", ScoreManager.I.GetScore ().ToString ());
		WWW www = new WWW (url, wwwForm);
		yield return www;
		yield break;
	}
}
