using UnityEngine;
using System.Collections;

public class SoundManager : SingletonBehaviour<SoundManager> {

	private AudioSource audioSource;

	//[SerializeField] private List<AudioClip> bgmList = new List<AudioClip>();
	//[SerializeField] private List<AudioClip> seList = new List<AudioClip>();

	private AudioClip buttonSE; 

	protected override void Initialize (){
		base.Initialize ();
		audioSource = gameObject.GetComponent<AudioSource> ();
		buttonSE = Resources.Load ("SE/Button") as AudioClip;
	}
	
	public void ButtonSE(){
		audioSource.clip = buttonSE;
		audioSource.Play ();
	}
}
