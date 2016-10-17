using UnityEngine;
using System.Collections;

public class SystemsComponent : SingletonBehaviour<SystemsComponent> {

	protected override void Initialize (){
		base.Initialize ();
		DontDestroyOnLoad (this);
		Debug.Log (instance);
	}
}
