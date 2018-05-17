using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour {

    private Vector3 position;

	// Use this for initialization
	void Start () {
        position = gameObject.transform.localPosition;
	}

    private void LateUpdate()
    {
        gameObject.transform.localPosition = new Vector3(position.x, gameObject.transform.localPosition.y, position.z);
    }
}
