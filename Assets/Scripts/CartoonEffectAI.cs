using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonEffectAI : MonoBehaviour
{

	public GameObject CartoonEffect;

	public IEnumerator stopEffect()
	{
		yield return new WaitForSeconds(1f);
		CartoonEffect.SetActive(false);
		
	}
	
	public IEnumerator playEffect()
	{
		CartoonEffect.SetActive(true);
		StartCoroutine(stopEffect());
		yield return new WaitForSeconds(1f);
		
	}
}
