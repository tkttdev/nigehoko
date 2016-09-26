using UnityEngine;
using System.Collections;

public class MovingBlockComponent : MonoBehaviour {

    private int dir;

    [SerializeField] public float time = 8.0f;

    void Start () {
        if (this.gameObject.transform.position.x < 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        while (true)
        {
            if (this.gameObject.transform.position.y - Camera.main.transform.position.y <= 0.5f) {
                if (dir == -1)
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("z", -180.0f, "time", time));
                    iTween.MoveTo(this.gameObject, iTween.Hash("x", 9.0f, "time", time));
                    //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //dir = 1;
                }
                else
                {
                    iTween.RotateTo(this.gameObject, iTween.Hash("z", 180.0f, "time", time));
                    iTween.MoveTo(this.gameObject, iTween.Hash("x", -9.0f, "time", time));
                    //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    //dir = -1;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}

