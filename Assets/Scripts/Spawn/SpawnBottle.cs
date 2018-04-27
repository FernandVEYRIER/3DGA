using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBottle : MonoBehaviour {

    [SerializeField]
    GameObject[] spawn;
    [SerializeField]
    GameObject bottle;

    GameObject[] instantiated;

    // Use this for initialization
    private void Start()
    {
        instantiated = new GameObject[spawn.Length];
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < spawn.Length; i++)
        {
            if (instantiated[i] == null)
                instantiated[i] = Instantiate(bottle, spawn[i].transform.position, Quaternion.identity);
        }
	}
}
