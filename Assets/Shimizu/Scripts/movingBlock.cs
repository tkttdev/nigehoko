using UnityEngine;
using System.Collections;

public class MovingBlock : MonoBehaviour {

    private int dir;
    public float beforePosx;

    public float speed = 8.0f;

	void Start () {
        dir = 1;
        beforePosx = this.transform.position.x;
        StartCoroutine(move());
    }

	void Update () {
	
	}

    IEnumerator move()
    {
        while (true)
        {
            //GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed * dir;
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + speed * dir * 0.1f, this.gameObject.transform.position.y);
            if (Mathf.Abs(beforePosx - this.transform.position.x) >= 8.0f)
            {
                dir *= (-1);
                beforePosx = this.transform.position.x;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}

