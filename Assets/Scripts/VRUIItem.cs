using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRUIItem : MonoBehaviour {

	private BoxCollider boxCollider;
	private RectTransform rectTransform;
	
	private void OnEnable()
	{
		ValidateCollider();
	}

	private void OnValidate()
	{
		ValidateCollider();
	}

	private void ValidateCollider()
	{
		Debug.Log("the name " + this.name);
		
		
		rectTransform = GetComponent<RectTransform>();

		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = gameObject.AddComponent<BoxCollider>();
		}

		boxCollider.size = rectTransform.sizeDelta;
	}
		/*if (this.name == "QuitButton")
			SceneManager.LoadScene("NewBarTry", LoadSceneMode.Single);
		else if (this.name == "RestartButton")
			Application.Quit();*/
		/*rectTransform = GetComponent<RectTransform>();

		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = gameObject.AddComponent<BoxCollider>();
		}

		boxCollider.size = rectTransform.sizeDelta;*/
	}
