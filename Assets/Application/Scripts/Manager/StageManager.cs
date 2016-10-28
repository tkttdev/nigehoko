﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : SingletonBehaviour<StageManager> {

    private float height;

    private float cameyBfore;
    private float cameyAfter;

	public float speed = 0.5f;

    private int level = 0;
    private int speedlevel = 0;
    private int num = 0;

	public bool isDemo = false;
	public List<GameObject> demoBlocks = new List<GameObject> ();

	[SerializeField] private GameObject[] backgroud = new GameObject[6];

    private float distcount = 0;
    [SerializeField] public int interval = 0;

	[SerializeField] GameObject[] background0 = new GameObject[3];
	[SerializeField] GameObject[] background1 = new GameObject[3];

	[SerializeField] private int generatedStageNum = 0;
	[SerializeField] private int backgroundTypeIndex = 0;

	const string background0Path = "Prefabs/Background0/Background";
	const string background1Path = "Prefabs/Background1/Background";
	const string stageBlockPath = "Prefabs/StageBlocks/stage";

    protected override void Initialize() {
        height = 8;  // 8 : blocksize
		generatedStageNum = 0;
		backgroundTypeIndex = 0;
		cameyBfore = 0.0f;

		for (int i = 0; i < 3; i++) {
			background0 [i] = Resources.Load (background0Path + i.ToString ()) as GameObject;
			background1 [i] = Resources.Load (background1Path + i.ToString ()) as GameObject;
		}

        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i < 1)
            {   
				Instantiate(Resources.Load(stageBlockPath + "0/00"),
                    new Vector2(0.0f, Camera.main.transform.position.y + i),
                    Quaternion.Euler(0, 0, 0));
				if (generatedStageNum % 2 == 0) {
					Instantiate (background0 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + i), Quaternion.Euler (0, 0, 0));
				}else if (generatedStageNum % 2 == 1) {
					Instantiate (background1 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + i), Quaternion.Euler (0, 0, 0));
				}
				generatedStageNum++;
				
            }
            else
            {
                num = (int)(Random.Range(0.0f, 10.0f));
				Instantiate(Resources.Load(stageBlockPath + level.ToString() + "/" + level.ToString() + num.ToString()),
                    new Vector2(0.0f, Camera.main.transform.position.y + i),
                    Quaternion.Euler(0, 0, 0));
				if (generatedStageNum % 2 == 0) {
					Instantiate (background0 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + i), Quaternion.Euler (0, 0, 0));
				}else if (generatedStageNum % 2 == 1) {
					Instantiate (background1 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + i), Quaternion.Euler (0, 0, 0));
				}
				generatedStageNum++;
            }
        }
    }

	public void StartStage(){
		StartCoroutine (MoveCamera());
	}

    public void Retry()
    {
        //現在のブロックをすべて削除
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        for (int i=0; i<blocks.Length; i++)
        {
            Destroy(blocks[i]);
        }

        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
		cameyBfore = 0.0f;

        //現レベルのブロックを生成
        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i < 1)
            { 
				Instantiate(Resources.Load(stageBlockPath + "0/00"),
                    new Vector2(0.0f, Camera.main.transform.position.y + i),
                    Quaternion.Euler(0, 0, 0));
            }
            else
            {
                num = (int)(Random.Range(0.0f, 10.0f));
				Instantiate(Resources.Load(stageBlockPath + level.ToString() + "/" + level.ToString() + num.ToString()),
                    new Vector2(0.0f, Camera.main.transform.position.y + i),
                    Quaternion.Euler(0, 0, 0));
            }

			if (generatedStageNum % 2 == 0) {
				Instantiate (background0 [backgroundTypeIndex], new Vector2(0.0f, Camera.main.transform.position.y + i), Quaternion.identity);
			}else if (generatedStageNum % 2 == 1) {
				Instantiate (background1 [backgroundTypeIndex], new Vector2(0.0f, Camera.main.transform.position.y + i), Quaternion.identity);
			}
			generatedStageNum++;
        }

        distcount = 0;

    }

    IEnumerator MoveCamera()
    {
		while (GameManager.I.IsPlaying())
        {
            //カメラの移動
            Camera.main.transform.position = new Vector3(0.0f, Camera.main.transform.position.y + speed*0.1f, -10.0f);
			if (!isDemo) {
				if (Camera.main.transform.position.y - cameyBfore >= height) {
					num = (int)(Random.Range (0.0f, 10.0f));
					Instantiate (Resources.Load (stageBlockPath + level.ToString () + "/" + level.ToString () + num.ToString ()),
						new Vector2 (0.0f, Camera.main.transform.position.y + 2.0f * height),
						Quaternion.identity);

					if (generatedStageNum % 2 == 0) {
						Instantiate (background0 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + 2.0f * height), Quaternion.identity);
					}else if (generatedStageNum % 2 == 1) {
						Instantiate (background1 [backgroundTypeIndex], new Vector2 (0.0f, Camera.main.transform.position.y + 2.0f * height), Quaternion.identity);
					}

					if (backgroundTypeIndex < 2) {
						backgroundTypeIndex = ScoreManager.I.GetScore() / 1000;
					}

					generatedStageNum++;
					cameyBfore = Camera.main.transform.position.y;
					distcount++;


				}

				if (distcount == interval) {  
					//難易度段階が6段
					distcount = 0;
					if (level < 8) {
						level++;
					}
				}
			}

			if (isDemo) {
				if (Camera.main.transform.position.y - cameyBfore >= height) {
					GameObject obj = demoBlocks[Random.Range (0, demoBlocks.Count)];

					Instantiate (obj, new Vector2 (0.0f, Camera.main.transform.position.y + 2.0f * height), Quaternion.identity);
					cameyBfore = Camera.main.transform.position.y;

				}
			}

            yield return new WaitForSeconds(0.02f);
        }

		yield break;
    }
}
