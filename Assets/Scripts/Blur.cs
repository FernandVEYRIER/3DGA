using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blur : MonoBehaviour {

    public Texture blur;
    public float alpha;

	// Use this for initialization
	void Start () {
        
    }


    // Update is called once per frame
    void Update () {
        
    }

    private void OnGUI()
    {
        Color temp;
        temp = GUI.color;
        temp.a = alpha;
        GUI.color = temp;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blur);
    }

}
