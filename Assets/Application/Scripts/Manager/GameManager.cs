using UnityEngine;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager> {

	enum STATE {
		WAITING,
		PLAYING,
		PAUSING,
		END,
	};

	private STATE state;

	protected override void Initialize () {
		state = STATE.WAITING;
	}

	private void GameProgress(){
		//return null;
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
	}

	public void SetStateEnd(){
		this.state = STATE.END;
		GameoverDialog.I.Show ();
		ObjectManager.I.InactiveEleDusts ();
	}
}
