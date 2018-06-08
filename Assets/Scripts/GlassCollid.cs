using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCollid : MonoBehaviour
{

	public RespawnGlass getAttribues;
	public int id_glass;
	
	public void reAttributesScript()
	{
		getAttribues = GameObject.Find("RespanwGlassManager").GetComponent<RespawnGlass>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("Floor"))
		{
			getAttribues.RespawnGlassOnRightplace(id_glass);
			Destroy(this.gameObject);
		}
	}
}
