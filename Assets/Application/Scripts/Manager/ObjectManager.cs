using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : SingletonBehaviour<ObjectManager> {

	public GameObject player;
	private GameObject eleDust;

	[SerializeField] List<GameObject> eleDustList = new List<GameObject>();
	[SerializeField] private int limitEleDustNum = 5;
	[SerializeField] private int currentActiveEledustIndex = 0;

	private bool isEleDust = false;

	private bool firstTouch = false;

	protected override void Initialize () {
		player = GameObject.FindGameObjectWithTag ("Player");
		currentActiveEledustIndex = 0;
		GameObject obj;
		for (int i = 0; i < limitEleDustNum; i++) {
			obj = Instantiate (Resources.Load ("Prefabs/EleDust") as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
			eleDustList.Add (obj);
			obj.SetActive (false);
		}
	}


	public void ActiveEleDust(Vector3 pos){
		if (!firstTouch) {
			firstTouch = true;
			GameManager.I.SetStatePlaying ();
		}

		InactiveEleDust ();

		currentActiveEledustIndex++;
		currentActiveEledustIndex %= limitEleDustNum;

		eleDustList [currentActiveEledustIndex].SetActive (true);
		eleDustList [currentActiveEledustIndex].transform.position = pos;

		isEleDust = true;
	}

	public void InactiveEleDust(){
		eleDustList [currentActiveEledustIndex].SetActive (false);
		isEleDust = false;
	}

	public bool IsEledust(){
		return isEleDust;
	}

	/// <summary>
	/// Return active eledust position;
	/// </summary>
	/// <returns>The eledust position.</returns>
	public Vector3 ActiveEledustPos(){
		return eleDustList [currentActiveEledustIndex].transform.position;
	}
}
