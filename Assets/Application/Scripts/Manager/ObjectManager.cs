using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : SingletonBehaviour<ObjectManager> {

	public GameObject player;
	private GameObject eleDust;

	[SerializeField] List<GameObject> eleDustList = new List<GameObject>();
	private int limitEleDustNum = 5;
	private int currentActiveEledustIndex = 0;

	private bool isActiveEleDust = false;

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

		isActiveEleDust = true;
	}

	public void InactiveEleDust(){
		eleDustList [currentActiveEledustIndex].SetActive (false);
		isActiveEleDust = false;
	}

	/// <summary>
	/// Return true if eledust is active in scene.
	/// Else return false this func.
	/// </summary>
	/// <returns><c>true</c> if this instance is active eledust; otherwise, <c>false</c>.</returns>
	public bool IsActiveEledust(){
		return isActiveEleDust;
	}

	/// <summary>
	/// Return active eledust position;
	/// </summary>
	/// <returns>The eledust position.</returns>
	public Vector3 ActiveEledustPos(){
		return eleDustList [currentActiveEledustIndex].transform.position;
	}
}
