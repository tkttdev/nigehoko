using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplaceGameObject : EditorWindow
{
	private Object m_replaceObject = null;

	[MenuItem("Tools/GameObject/Replace GameObject")]
	public static void Open()
	{
		ReplaceGameObject window = EditorWindow.GetWindow<ReplaceGameObject>();
		window.Init();
		window.Show();
	}

	public void Init()
	{
		m_replaceObject = null;
	}

	void OnGUI()
	{
		GameObject replaceObj = EditorGUILayout.ObjectField(m_replaceObject, typeof(GameObject), true) as GameObject;
		if (replaceObj != null)
		{
			if (PrefabUtility.GetPrefabType(replaceObj) == PrefabType.Prefab)
			{
				m_replaceObject = replaceObj;
			}
		}
		GameObject[] selection = Selection.gameObjects;
		if ( (m_replaceObject != null) && (selection != null && selection.Length > 0) )
		{
			if (GUILayout.Button("replace"))
			{
				List<GameObject> delList = new List<GameObject>();
				foreach (GameObject selObj in selection)
				{
					GameObject newObj = PrefabUtility.InstantiatePrefab(m_replaceObject) as GameObject;
					if (newObj != null)
					{
						newObj.transform.parent = selObj.transform.parent;
						newObj.transform.position = selObj.transform.position;
						newObj.transform.rotation = selObj.transform.rotation;
						delList.Add(selObj);
					}
				}
				foreach (GameObject obj in delList) DestroyImmediate(obj);
			}
		}
	}
}
