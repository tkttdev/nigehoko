using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : SingletonBehaviour<ObjectManager> {

	[SerializeField] private GameObject player;

	protected override void Initialize () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
}
