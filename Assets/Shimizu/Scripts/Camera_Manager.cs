using UnityEngine;
using System.Collections;

public class Camera_Manager : MonoBehaviour {

    private int diffy;
    public GameObject[] blockList;
    private float height;

    

    void Start () {
        height = blockList[0].transform.FindChild("floar").GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log(height);
        diffy = 0;

        for (int i = (-1)*(int)height; i <= 2*height; i += (int)height)
        {
            Instantiate(blockList[((int)(Random.Range(0.0f, (float)this.blockList.Length)))],
                   new Vector2(0.0f, transform.position.y + i),
                   Quaternion.Euler(0, 0, 0));
        }

        StartCoroutine(moveCamera());

    }
	

	void Update () {
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * (1.0f / 3.0f * height);
    }

    IEnumerator moveCamera()
    {
        while (true)
        {
            //GetComponent<Rigidbody2D>().velocity = transform.up.normalized * ( 0.1f / 3.0f * height);
            //gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + (0.05f / 3.0f * height));
            
           //diffy++;

           //if (diffy == 60) //3sごとに２枚先のブロックを生成
           //{
                Instantiate(blockList[((int)(Random.Range( 0.0f, (float)this.blockList.Length )))],
                    new Vector2(0.0f, transform.position.y + 2.0f * height), 
                    Quaternion.Euler(0, 0, 0));
      
              //  diffy = 0;
            //}
            yield return new WaitForSeconds(3.0f);
        }
    }
}
