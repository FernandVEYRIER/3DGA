using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOBJ : MonoBehaviour
{

	public GameObject[] OBJ_ToRespawn;
	private GameObject[] OBJSave;
	
	// Use this for initialization
	void Start () {
		
		OBJSave = new GameObject[OBJ_ToRespawn.Length];
		
		for (int i = 0; i < OBJ_ToRespawn.Length; i++)
			OBJSave[i] = OBJ_ToRespawn[i];
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < OBJ_ToRespawn.Length; i++)
		{
			//if (OBJ_ToRespawn[i] == null)
				
		}
		            
	}
}
