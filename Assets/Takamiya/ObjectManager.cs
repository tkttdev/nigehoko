using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : SingletonBehaviour<ObjectManager> {

	public GameObject player;

	protected override void Initialize () {
		Debug.Log ("init");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
