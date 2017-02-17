using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeScene : MonoBehaviour {

	[SerializeField] private SpriteRenderer fadeSprite;
	System.Action fadeOut = null;

	public void FadeOut(System.Action _fadeOut){
		fadeOut = _fadeOut;
		gameObject.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		iTween.ValueTo (fadeSprite.gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 0.15f, "onupdate", "SetAlpha", "oncomplete", "OnCompleteFadeOut"));
	}

	public void FadeIn(){
		iTween.ValueTo (fadeSprite.gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 0.15f, "onupdate", "SetAlpha"));
	}

	private void SetAlpha(float alpha){
		fadeSprite.color = new Color (fadeSprite.color.r, fadeSprite.color.g, fadeSprite.color.b, alpha);
	}

	private void OnCompleteFadeOut(){
		fadeOut ();
		FadeIn ();
	}
}
