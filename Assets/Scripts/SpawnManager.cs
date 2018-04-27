using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public GameObject GameManager;

	public GameObject[] nbSpawner;

	public GameObject enemyBehaviour;

	public bool[] enemyHere;
	// Use this for initialization
	void Start () {
		enemyHere = new bool[nbSpawner.Length];

		for (int i = 0; i < enemyHere.Length; i++) {
			enemyHere [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < enemyHere.Length; i++) {
			if (!enemyHere[i]) {
				enemyHere[i] = true;
				spawnFoe (i);
			}
		}

	}

	void spawnFoe(int wichSpawner)
	{
		var tmp = Instantiate (enemyBehaviour, new Vector3 (
			nbSpawner [wichSpawner].transform.position.x,
			nbSpawner [wichSpawner].transform.position.y + 1,
			nbSpawner [wichSpawner].transform.position.z),
			Quaternion.identity);
		tmp.GetComponent<EnemyMove> ().indexEnemy = wichSpawner;
	}
}
