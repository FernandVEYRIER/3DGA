using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	public int indexEnemy;

	private bool goStrait;
	private bool goBack;

	// Use this for initialization
	void Start () {
		goStrait = true;
		goBack = false;
	}
	
	// Update is called once per frame
	/*void Update () {
		if (goStrait) {
			Vector3 tmp = this.transform.position;
			tmp.z += 0.05f;
			this.transform.position = tmp;
			if (this.transform.position.z >= 5) {
				goStrait = false;
				goBack = true;
			}
		}
		else if (goBack) {
			Vector3 tmp = this.transform.position;
			tmp.z -= 0.05f;
			this.transform.position = tmp;
			if (this.transform.position.z <= -9) {
				goStrait = true;
				goBack = false;
			}
		}
	}*/

	void OnCollisionEnter(Collision coll)
	{
		if (coll.transform.tag == "Bottle") {
			//GameObject.Find ("GameManager").GetComponent<SpawnManager> ().enemyHere [indexEnemy] = false;
			Destroy (this.gameObject);
		}
	}
}
