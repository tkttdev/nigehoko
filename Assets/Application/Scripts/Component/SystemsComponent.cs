using UnityEngine;
using System.Collections;

public class SystemsComponent : MonoBehaviour {

	private void Start(){
		DontDestroyOnLoad (this);
	}
}
