using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplaceGameObject : EditorWindow
{
	// 置換オブジェクト
	private Object m_replaceObject = null;

	/// <summary>
	/// ウィンドウを開く
	/// </summary>
	[MenuItem("Custom/Tools/Replace GameObject")]
	public static void Open()
	{
		ReplaceGameObject window = EditorWindow.GetWindow<ReplaceGameObject>();
		window.Init();
		window.Show();
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		m_replaceObject = null;
	}

	void OnGUI()
	{
		// 置換オブジェクト
		GameObject replaceObj = EditorGUILayout.ObjectField(m_replaceObject, typeof(GameObject), true) as GameObject;
		if (replaceObj != null)
		{
			if (PrefabUtility.GetPrefabType(replaceObj) == PrefabType.Prefab)
			{
				// プレハブがドラッグされた時だけ、置換オブジェクトを更新
				m_replaceObject = replaceObj;
			}
		}
		// 置換実行ボタン
		GameObject[] selection = Selection.gameObjects;
		if ( (m_replaceObject != null) &&                                       // 置換オブジェクトが設定されている
			(selection != null && selection.Length > 0) )  // ヒエラルキー上のアイテムが1つ以上選択されている
		{
			if (GUILayout.Button("replace"))
			{
				List<GameObject> delList = new List<GameObject>();
				foreach (GameObject selObj in selection)
				{
					// プレハブを複製して座標と姿勢をコピーする
					GameObject newObj = PrefabUtility.InstantiatePrefab(m_replaceObject) as GameObject;
					if (newObj != null)
					{
						newObj.transform.parent = selObj.transform.parent;
						newObj.transform.position = selObj.transform.position;
						newObj.transform.rotation = selObj.transform.rotation;
						delList.Add(selObj);
					}
				}
				// 元のオブジェクトをは全て削除
				foreach (GameObject obj in delList) DestroyImmediate(obj);
			}
		}
	}
}
