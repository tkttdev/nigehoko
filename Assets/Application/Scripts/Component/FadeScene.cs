using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeScene : MonoBehaviour {

	[SerializeField] private Canvas canvas;
	[SerializeField] private Image image;

	// Use this for initialization
	void Start () {
		SceneManager.sceneLoaded += SceneLoadedInit;
	}

	// Update is called once per frame
	void Update () {

	}

	public void SceneLoadedInit(Scene scene, LoadSceneMode sceneMode){

	}
}
