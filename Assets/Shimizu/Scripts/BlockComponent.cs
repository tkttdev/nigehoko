using UnityEngine;
using System.Collections;

public class BlockComponent : MonoBehaviour {

    private int endtime = 24;
    private int time = 0;

    void Start () {
        time = 0;
        StartCoroutine(dest());
    }
	
	void Update () {
	
	}

    IEnumerator dest()
    {
        while (true)
        {
            time++;
            if (time == endtime)
            {
                if (GameManager.I.IsPlaying()) {
                    Destroy(this.gameObject);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
