﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditManager : SingletonBehaviour<CreditManager> {

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("Title");
		}
	}

}
