using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AItester : MonoBehaviour {

    private GameObject drunkGuy;
    private NavMeshAgent ai;

    public GameObject destination;

	// Use this for initialization
	void Start () {
		ai = GetComponent<NavMeshAgent>();
        drunkGuy = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        ai.SetDestination(destination.transform.position);
    }
}
