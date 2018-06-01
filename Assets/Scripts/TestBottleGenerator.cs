using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBottleGenerator : MonoBehaviour {

    public GameObject bottle;
    private float t;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t >= 3)
        {
            GameObject obj;
            obj = Instantiate(bottle, transform.position, transform.rotation);
            obj.name = "EmptyBottle";
            t = 0;
        }
	}
}
