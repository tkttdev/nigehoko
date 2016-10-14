using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour{

	void Start(){
		if (!SystemsComponent.IsExist ()) {
			GameObject sys = Resources.Load("Prefabs/Systems") as GameObject;
			Instantiate (sys);
		}
	}
}
