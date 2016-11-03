using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : SingletonBehaviour<UIManager> {

	[SerializeField] private Text tapText;

	[SerializeField] private Text scoreText;

	[SerializeField] private Text fpsText;

	[SerializeField] private Sprite stopButtonSprite;
	[SerializeField] private Sprite startButtonSprite;

	[SerializeField] private Image menuButtonImage;

	public GameOverDialog gameOverDialog;
	public PauseDialog pauseDialog;

	protected override void Initialize () {
		tapText = GameObject.Find ("TapText").GetComponent<Text>();
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
		fpsText = GameObject.Find ("FpsText").GetComponent<Text> ();

		stopButtonSprite = Resources.Load<Sprite> ("Images/StopSkelton");
		startButtonSprite = Resources.Load<Sprite> ("Images/PlaySkelton");

		gameOverDialog = GameObject.FindGameObjectWithTag ("GameOverDialog").GetComponent<GameOverDialog> ();
		pauseDialog = GameObject.FindGameObjectWithTag ("PauseDialog").GetComponent<PauseDialog> ();

		menuButtonImage = GameObject.FindGameObjectWithTag ("MenuButton").GetComponent<Image> ();
			
		iTween.ScaleFrom (tapText.gameObject, iTween.Hash (
			"x", 1.2f,
			"y", 1.2f,
			"looptype", iTween.LoopType.pingPong,
			"easetype", iTween.EaseType.linear
		));

		//#if UNITY_EDITOR
		fpsText.enabled = true;
		//#endif
	}

	int frameCount = 0;
	float prevTime = 0.0f;

	void Update(){
		if (ObjectManager.I.IsActiveEledust()) {
			SetStartText (false);
		}

		//#if UNITY_EDITOR
		++frameCount;
		float time = Time.realtimeSinceStartup - prevTime;

		if (time >= 0.5f) {
			fpsText.text = "FPS : " + frameCount / time;

			frameCount = 0;
			prevTime = Time.realtimeSinceStartup;
		}
		//#else
		//fpsText.enabled = false;
		//#endif

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
			UIManager.I.pauseDialog.Show ();
			GameManager.I.bgmAudioSource.Pause ();
		} else if (GameManager.I.IsPausing ()) {
			GameManager.I.SetStatePlaying ();
			ObjectManager.I.player.GetComponent<PlayerComponent> ().AddForcePlayer ();
			menuButtonImage.sprite = stopButtonSprite;
			UIManager.I.pauseDialog.Hide ();
			GameManager.I.bgmAudioSource.UnPause ();
		}
	}
}
