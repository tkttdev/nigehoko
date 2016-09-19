using UnityEngine;
using System.Collections;

public class BlockComponent : MonoBehaviour {

    private int endtime = 21;
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
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
