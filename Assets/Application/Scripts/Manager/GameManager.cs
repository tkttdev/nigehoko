﻿using UnityEngine;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager> {

	enum STATE {
		WAITING,
		PLAYING,
		PAUSING,
		END,
	};

	private STATE state;

	public AudioSource bgmAudioSource;

	protected override void Initialize () {
		state = STATE.WAITING;
		bgmAudioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void SetStateWaiting(){
		this.state = STATE.WAITING;
	}

	public void SetStatePlaying(){
		this.state = STATE.PLAYING;
		ScoreManager.I.StartAddScore ();
		StageManager.I.StartStage ();
	}

	public void SetStatePausing(){
		this.state = STATE.PAUSING;
		ScoreManager.I.StopAddScore ();
	}

	public void SetStateEnd(){
		this.state = STATE.END;
		ScoreManager.I.SetHighScore ();
		ObjectManager.I.InactiveEleDust ();
		ScoreManager.I.StopAddScore ();
		UIManager.I.gameOverDialog.Show ();
	}

	public bool IsPlaying(){
		if (this.state == STATE.PLAYING) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsPausing(){
		if (this.state == STATE.PAUSING) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsEnd(){
		if (this.state == STATE.END) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsWaiting(){
		if (this.state == STATE.WAITING) {
			return true;
		} else {
			return false;
		}
	}

	public void RetryGame(){
		StageManager.I.Retry ();
		ObjectManager.I.player.GetComponent<PlayerComponent> ().RetryInitialize ();
		UIManager.I.gameOverDialog.StartRetryCount ();
	}
}
