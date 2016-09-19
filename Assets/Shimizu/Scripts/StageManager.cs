using UnityEngine;
using System.Collections;

public class StageManager : SingletonBehaviour<StageManager> {

    public GameObject[] blockList;
    private float height;

    private float cameyBfore;
    private float cameyAfter;

	[SerializeField] public float speed = 1.0f; 

    protected override void Initialize() {
        height = 8;  // 8 : blocksize

        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            if (i <= 0)
            {   //初期位置志望予防
                Instantiate(blockList[0],
                new Vector2(0.0f, Camera.main.transform.position.y + i),
                Quaternion.Euler(0, 0, 0));

            }
            else
            {
                Instantiate(blockList[((int)(Random.Range(0.0f, (float)this.blockList.Length)))],
                new Vector2(0.0f, Camera.main.transform.position.y + i),
                Quaternion.Euler(0, 0, 0));
            }
        }
    }

	public void StartStage(){
		StartCoroutine ("MoveCamera");
	}

    IEnumerator MoveCamera()
    {
        while (true)
        {
            //カメラの移動
            Camera.main.transform.position = new Vector3(0.0f, Camera.main.transform.position.y + speed*0.1f, -10.0f);

            if (gameObject.transform.position.y - cameyBfore >= height)
            {
                Instantiate(blockList[((int)(Random.Range(0.0f, (float)this.blockList.Length)))],
                    new Vector2(0.0f, Camera.main.transform.position.y + 2.0f * height),
                    Quaternion.identity);
                cameyBfore = Camera.main.transform.position.y;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
