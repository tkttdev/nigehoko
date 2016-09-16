using UnityEngine;
using System.Collections;

public class Camera_Manager : SingletonBehaviour<Camera_Manager> {

    private int diffy;
    public GameObject[] blockList;
    private float height;


    protected override void Initialize() {
        height = 8;  //blockList[0].transform.FindChild("floar").GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log(height);
        diffy = 0;

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
            diffy++;

            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (0.05f / 3.0f * height),-10);

           if (diffy == 60) //3sごとに２枚先のブロックを生成
           {
                Instantiate(blockList[((int)(Random.Range( 0.0f, (float)this.blockList.Length )))],
                    new Vector2(0.0f, transform.position.y + 2.0f * height), 
                    Quaternion.identity);
      
               diffy = 0;
           }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
