using UnityEngine;
using System.Collections;

public class BlockComponent : MonoBehaviour {

    public int time;

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
            if (time == 16)
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
