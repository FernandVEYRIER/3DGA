using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePileSpawner : MonoBehaviour {

    [SerializeField] GameObject bottlePrefab;

    public GameObject GetBottle()
    {
        return Instantiate(bottlePrefab, gameObject.transform.position, bottlePrefab.transform.rotation);
    }
}
