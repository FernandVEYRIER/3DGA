using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollid : MonoBehaviour {



	public bool setTrigger(GameObject bottle)
	{
		//Get component Box collider

		var trig = bottle.GetComponent<BoxCollider>().isTrigger;

		if (trig)
		{
			Debug.Log("Trigger  false");
			trig = false;
		}
		else if (!trig)
		{
			Debug.Log("Trigger  true");
			trig = true;
		}

		return trig;
	}
}
