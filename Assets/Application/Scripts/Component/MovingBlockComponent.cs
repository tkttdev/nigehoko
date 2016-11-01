using UnityEngine;
using System.Collections;

public class MovingBlockComponent : MonoBehaviour {

    private int dir;

    [SerializeField] public float time = 8.0f;

	private float rotateZ = 0;
	private Rigidbody2D ballRigidbody2D;

    void Start () {
        if (this.gameObject.transform.position.x < 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
		rotateZ = 0;
		ballRigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
    }

	void Update(){
		if (this.gameObject.transform.position.y - Camera.main.transform.position.y <= 0.5f && GameManager.I.IsPlaying ()) {
			if (ballRigidbody2D.velocity.x == 0) {
				if (dir == -1) {
					ballRigidbody2D.AddForce (new Vector2 (1000000, 0));
				} else {
					ballRigidbody2D.AddForce (new Vector2 (-1000000, 0));
				}
			}

			if (dir == -1) {
				rotateZ -= 50 * Time.deltaTime;
			} else {
				rotateZ += 50 * Time.deltaTime;
			}
			gameObject.transform.rotation = Quaternion.Euler (0, 0, rotateZ);
		} else if (!GameManager.I.IsPlaying ()) {
			if (ballRigidbody2D.velocity.x != 0) {
				ballRigidbody2D.velocity = Vector2.zero;
			}
		}
	}
}

