using UnityEngine;
using System.Collections;

public class movingBlock : MonoBehaviour {

    private int dir;
    private int time;

    public float speed = 8.0f;

	// Use this for initialization
	void Start () {
        dir = -1;
        time = 0;
        StartCoroutine(move());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator move()
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed * dir;

            time++;
            if (time == 10)
            {
                dir *= (-1);
                time = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

