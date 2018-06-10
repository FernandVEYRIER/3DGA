using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollid : MonoBehaviour {


	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("AI"))
			StartCoroutine(other.gameObject.GetComponent<CartoonEffectAI>().playEffect());
	}
}
