﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Throwable;

public class GrabObject : MonoBehaviour {

	[SerializeField]
	private float force = 10000.0f, forceRelase = 10.0f;

	private SteamVR_TrackedObject trackedObj;

	private GameObject collidingObject; 
	private GameObject objectInHand;

	private BoxCollider _myBox;

	private bool objectDiff = false;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void SetCollidingObject(Collider col)
	{
		// 1
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}
		// 2
		collidingObject = col.gameObject;
		col.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		col.gameObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	// 2
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	// 3
	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void _GrabObject()
	{
		// 1
		objectInHand = collidingObject;
		collidingObject = null;
		// 2
		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

		if (objectInHand.GetComponent<AThrowable> () != null)
			objectInHand.GetComponent<AThrowable> ().PlayerGrab();
	}

	// 3
	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = force;
		fx.breakTorque = force;
		return fx;
	}

	private void ReleaseObject()
	{
		// 1
		if (GetComponent<FixedJoint>())
		{
			// 2
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			// 3
			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity * forceRelase;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity * forceRelase;

			if (objectInHand.GetComponent<AThrowable>() != null)
			{
				objectInHand.GetComponent<AThrowable>().PlayerThrow();
				//_myBox.enabled = true;
			}

			if (objectInHand.GetComponent<BottleStrickEvent>())
				objectInHand.GetComponent<BottleStrickEvent>().Enable = true;

			// Release here


		}
		// 4
		objectInHand = null;
	}

	void Update()
	{
		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject)
			{
				_GrabObject();

				if (objectInHand.GetComponent<BoxCollider>() == null)
				{
					objectDiff = true;
				}
				else
				{
					_myBox = objectInHand.GetComponent<BoxCollider>();
					_myBox.enabled = false;
				}
			}
		}

		// 2
		if (Controller.GetHairTriggerUp())
		{
			
			if (objectInHand)
			{
				ReleaseObject();
				if (objectDiff)
					objectDiff = false;
				else
					StartCoroutine(reactiveCollider());

			}
		}
	}

	//IEnumerator to Activate the collider

	IEnumerator reactiveCollider()
	{
		yield return  new WaitForSeconds(0.1f);
		_myBox.enabled = true;
	}
}
