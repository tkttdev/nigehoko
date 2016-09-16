using UnityEngine;
using System.Collections;

public class Block_Manager : MonoBehaviour {

    private int time;

	// Use this for initialization
	void Start () {
        time = 0;
        StartCoroutine(dest());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator dest()
    {
        while (true)
        {
            time++;
            if (time == 12)
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(12.0f);
        }
    }
}
