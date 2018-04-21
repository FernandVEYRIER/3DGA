using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDrunk : ADrunkAI {

	// Use this for initialization
	protected override void  Start () {
        base.Start();
        print("basic drunk start");
        foreach (string s in ActionClassName)
        {
            actions.Add((AAction)Activator.CreateInstance(Type.GetType(s)));
            actions[actions.Count - 1].Initialize(this);
        }
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}
}
