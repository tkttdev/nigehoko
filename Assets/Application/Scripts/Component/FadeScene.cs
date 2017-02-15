using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeScene : MonoBehaviour {

	[SerializeField] private SpriteRenderer fadeSprite;

	public void FadeIn(){
		iTween.ValueTo (fadeSprite.gameObject, iTween.Hash ("from", 0.0f, "to", 1.0f, "time", 0.25f, "onupdate", "SetAlpha", "oncomplete", "FadeOut"));
	}

	public void FadeOut(){
		iTween.ValueTo (fadeSprite.gameObject, iTween.Hash ("from", 1.0f, "to", 0.0f, "time", 0.25f, "onupdate", "SetAlpha"));
	}

	private void SetAlpha(float alpha){
		fadeSprite.color = new Color (fadeSprite.color.r, fadeSprite.color.g, fadeSprite.color.b, alpha);
	}
}
