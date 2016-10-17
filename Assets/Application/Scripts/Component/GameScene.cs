using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour{

	void Start(){
		if (GameObject.Find("Systems") == null) {
			GameObject sys = Resources.Load("Prefabs/Systems") as GameObject;
			sys.transform.name = "Systems";
			Instantiate (sys);
		}
	}
}
