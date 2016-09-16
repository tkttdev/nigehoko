using UnityEngine;
using System.Collections;

public class Camera_Manager : SingletonBehaviour<Camera_Manager> {

    public GameObject[] blockList;
    private float height;

    private float cameyBfore;
    private float cameyAfter;

    public float speed = 5.0f; 

    protected override void Initialize() {
        height = 8;  // 8 : blocksize

        for (int i = (-1) * (int)height; i <= 2 * height; i += (int)height)
        {
            Instantiate(blockList[((int)(Random.Range(0.0f, (float)this.blockList.Length)))],
                   new Vector2(0.0f, transform.position.y + i),
                   Quaternion.Euler(0, 0, 0));
        }

        StartCoroutine(moveCamera());

    }

	void Update () {

    }

    IEnumerator moveCamera()
    {
        while (true)
        {
            //カメラの移動
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (0.05f / 3.0f * height)*(2.0f / 5.0f),-10);

            if (gameObject.transform.position.y - cameyBfore >= height)
            {
                Instantiate(blockList[((int)(Random.Range(0.0f, (float)this.blockList.Length)))],
                    new Vector2(0.0f, transform.position.y + 2.0f * height),
                    Quaternion.identity);
                cameyBfore = gameObject.transform.position.y;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}
