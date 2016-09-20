using UnityEngine;
using System.Collections;

public class StageManager : SingletonBehaviour<StageManager> {

    private float height;

    private float cameyBfore;
    private float cameyAfter;

	[SerializeField] public float speed = 1.0f;

    private int level = 0;
    private int num = 0;

    private float distcount = 0;
    [SerializeField] public int interval = 0;

    protected override void Initialize() {
        height = 8;  // 8 : blocksize

        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i <= 0)
            {   //初期位置設定
                string num = ((int)(Random.Range(0.0f, 10.0f))).ToString();
                Instantiate(Resources.Load("Prefabs/StageBlocks/stage" + "0" + "/" + "0" + "0"),
                new Vector2(0.0f, Camera.main.transform.position.y + i),
                Quaternion.Euler(0, 0, 0));
            }
            else
            {
                num = (int)(Random.Range(0.0f, 10.0f));
                Instantiate(Resources.Load("Prefabs/StageBlocks/stage" + level.ToString() + "/" + level.ToString() + num.ToString()),
                new Vector2(0.0f, Camera.main.transform.position.y + i),
                Quaternion.Euler(0, 0, 0));
            }
        }
    }

	public void StartStage(){
		StartCoroutine (MoveCamera());
	}

    IEnumerator MoveCamera()
    {
        while (true)
        {
            //カメラの移動
            Camera.main.transform.position = new Vector3(0.0f, Camera.main.transform.position.y + speed*0.1f, -10.0f);
            Debug.Log(Camera.main.transform.position.y);
            if (Camera.main.transform.position.y - cameyBfore >= height)
            {
                num = (int)(Random.Range(0.0f, 10.0f));
                Instantiate(Resources.Load("Prefabs/StageBlocks/stage" + level.ToString() + "/" + level.ToString() + num.ToString()),
                    new Vector2(0.0f, Camera.main.transform.position.y + 2.0f * height),
                    Quaternion.identity);
                cameyBfore = Camera.main.transform.position.y;
                distcount++;
            }

            if (distcount == interval)
            {   //難易度段階が５段
                distcount = 0;
                level++;
                if (level > 5)
                {
                    level = 5;
                }
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
