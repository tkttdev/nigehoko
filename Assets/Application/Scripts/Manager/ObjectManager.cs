using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : SingletonBehaviour<ObjectManager> {

	private GameObject player;
	private GameObject eleDust;

	[SerializeField] List<GameObject> eleDustList = new List<GameObject>();
	[SerializeField] private int limitEleDustNum = 3;
	[SerializeField] private int currentActiveEledustNum = 2;

	[SerializeField] private int eleDustNum = 0;

	private bool firstTouch = false;

	protected override void Initialize () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	public void ActiveEleDust(Vector3 pos){
		if (!firstTouch) {
			firstTouch = true;
			GameManager.I.SetStatePlaying ();
		}

		if (eleDustNum < limitEleDustNum) {
			GameObject obj = Instantiate (Resources.Load ("Prefabs/EleDust") as GameObject, pos, Quaternion.identity) as GameObject;
			eleDustList.Add (obj);
			if (eleDustNum > 0) {
				eleDustList [eleDustNum - 1].SetActive (false);
			}
			eleDustNum++;
		} else {
			eleDustList [currentActiveEledustNum].SetActive (false);
			currentActiveEledustNum++;
			currentActiveEledustNum %= limitEleDustNum;

			eleDustList [currentActiveEledustNum].SetActive (true);
			eleDustList [currentActiveEledustNum].transform.position = pos;
		}
	}

	public void InactiveEleDusts(){
		foreach (GameObject obj in eleDustList) {
			obj.SetActive (false);
		}
	}
}
