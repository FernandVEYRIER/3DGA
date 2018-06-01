using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class blurGestion : MonoBehaviour {

    public PostProcessingProfile ppp;

	// Use this for initialization
	void Start () {
        DepthOfFieldModel.Settings dof = ppp.depthOfField.settings;
        dof.focalLength = 0;
        ppp.depthOfField.settings = dof;
    }
	
	// Update is called once per frame
	void Update () {
        DepthOfFieldModel.Settings dof = ppp.depthOfField.settings;
        if (dof.focalLength < 300)
        {
            dof.focalLength += 10 * Time.deltaTime;
            if (dof.focalLength > 300)
                dof.focalLength = 300;
            ppp.depthOfField.settings = dof;
        }
	}
}
