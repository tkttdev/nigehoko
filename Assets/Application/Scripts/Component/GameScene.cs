using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour{

	void Start(){
		if (!AppSceneManager.IsExist()) {
			GameObject sys = Resources.Load("Prefabs/Systems") as GameObject;
			Instantiate (sys).name = "Systems";
		}
	}
}
