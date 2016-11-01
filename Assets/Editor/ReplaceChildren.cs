using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplaceChildren : EditorWindow {
	const string WINDOW_WIDTH_SIZE_KEY = "WindowWidthSize";
	const string WINDOW_HEIGHT_SIZE_KEY = "WindowHeightSize";

	[MenuItem("Custom Tools/Prefab/ReplaceChildren")]
	public static void Open()
	{
		GetWindow<ReplaceChildren> ();
	}

	void OnEnable ()
	{
		var width = EditorPrefs.GetFloat (WINDOW_WIDTH_SIZE_KEY, 600);
		var height = EditorPrefs.GetFloat (WINDOW_HEIGHT_SIZE_KEY, 400);
		position = new Rect (position.x, position.y, width, height);
	}

	void OnGUI(){
		EditorGUILayout.ObjectField (null, typeof(Object), false);
	}

	void OnDisable ()
	{
		EditorPrefs.SetFloat (WINDOW_WIDTH_SIZE_KEY, position.width);
		EditorPrefs.SetFloat (WINDOW_HEIGHT_SIZE_KEY, position.height);
	}

	void Display(){
		GUILayout.Button ("Replace");
	}
}
