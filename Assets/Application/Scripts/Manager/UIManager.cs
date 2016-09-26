using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager> {

	[SerializeField] private Text tapText;

	[SerializeField] private Text scoreText;

	[SerializeField] private Sprite stopButtonSprite;
	[SerializeField] private Sprite startButtonSprite;

	[SerializeField] private Image menuButtonImage;


	protected override void Initialize () {
		tapText = GameObject.Find ("TapText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

		stopButtonSprite = Resources.Load<Sprite> ("Images/StopSkelton");
		startButtonSprite = Resources.Load<Sprite> ("Images/PlaySkelton");

		menuButtonImage = GameObject.FindGameObjectWithTag ("MenuButton").GetComponent<Image> ();
			
		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));
	}

	void Update(){
		if (ObjectManager.I.IsEledust()) {
			SetStartText (false);
		}
	}

	public void SetStartText(bool active) {
		if (active) {
			tapText.color = new Color (0, 0, 0, 1);
		} else {
			iTween.Stop (tapText.gameObject, "scale");
			tapText.color = new Color (0, 0, 0, 0);
		}
	}

	public void SetScoreText(int score){
		scoreText.text = string.Format ("{0} cm", score);
	}

	public void MenuButton(){
		if (GameManager.I.IsPlaying ()) {
			GameManager.I.SetStatePausing ();
			menuButtonImage.sprite = startButtonSprite;
			PauseDialog.I.Show ();
		} else if (GameManager.I.IsPausing ()) {
			GameManager.I.SetStatePlaying ();
			ObjectManager.I.player.GetComponent<PlayerComponent> ().AddForcePlayer ();
			menuButtonImage.sprite = stopButtonSprite;
			PauseDialog.I.Hide ();
		}
	}
}
