using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTextComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1.15, "y", 1.15, "loopType", iTween.LoopType.pingPong, "easeType", iTween.EaseType.linear));
	}

}
