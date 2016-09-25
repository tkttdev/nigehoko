using UnityEngine;
using System.Collections;

public class BlockComponent : MonoBehaviour {

    private int endtime = 24;
    private int time = 0;

    [SerializeField] public int cost = 3;

    void Start () {
        StartCoroutine(dest());
    }
	
	void Update () {
	
	}

    IEnumerator dest()
    {
        while (true)
        {
            if (Camera.main.transform.position.y -  this.gameObject.transform.position.y >= 16.0f)
            {
                Destroy(this.gameObject);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
