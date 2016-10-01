using UnityEngine;
using System.Collections;

public class StageManager : SingletonBehaviour<StageManager> {

    private float height;

    private float cameyBfore;
    private float cameyAfter;

	[SerializeField] public float[] speed;

    private int level = 0;
    private int speedlevel = 0;
    private int num = 0;

    private bool flag = false;

    private float distcount = 0;
    [SerializeField] public int interval = 0;

    protected override void Initialize() {
        height = 8;  // 8 : blocksize

        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i < 0)
            {   
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

    public void Retry()
    {
        //現在のブロックをすべて削除
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        for (int i=0; i<blocks.Length; i++)
        {
            Destroy(blocks[i]);
        }
        //現レベルのブロックを生成
        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i < 0)
            { 
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

        distcount = 0;

    }

    IEnumerator MoveCamera()
    {
		while (GameManager.I.IsPlaying())
        {
            //カメラの移動
            Camera.main.transform.position = new Vector3(0.0f, Camera.main.transform.position.y + speed[speedlevel]*0.1f, -10.0f);
            if (Camera.main.transform.position.y - cameyBfore >= height)
            {
                num = (int)(Random.Range(0.0f, 10.0f));
                Instantiate(Resources.Load("Prefabs/StageBlocks/stage" + level.ToString() + "/" + level.ToString() + num.ToString()),
                    new Vector2(0.0f, Camera.main.transform.position.y + 2.0f * height),
                    Quaternion.identity);
                cameyBfore = Camera.main.transform.position.y;
                distcount++;
                //Floorのバグ対策
                if (flag)
                {
                    Instantiate(Resources.Load("Prefabs/StageBlocks/WoodFloor"),
                        new Vector2(0.0f, Camera.main.transform.position.y + 2.0f * height),
                        Quaternion.identity);
                    flag = false;
                }
                else
                {
                    flag = true;
                }

            }

            if (distcount == interval)
            {  
                //難易度段階が6段,スピードレベルは5段
                distcount = 0;
                if(level < 6)
                {
                    level++;
                }
                else
                {
                    if(speedlevel<5) speedlevel++;
                }
            }

            yield return new WaitForSeconds(0.02f);
        }

		yield break;
    }
}
