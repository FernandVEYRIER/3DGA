using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGlass : MonoBehaviour
{
	public GameObject glassOriginal;
	
	public Transform[] transformToRespawn;

	public void RespawnGlassOnRightplace(int id)
	{
		var tmp = Instantiate(glassOriginal, transformToRespawn[id].position, Quaternion.identity);
		tmp.GetComponent<GlassCollid>().reAttributesScript();
		tmp.GetComponent<GlassCollid>().id_glass = id;
	}
}
