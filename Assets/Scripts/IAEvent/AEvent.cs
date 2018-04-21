﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEvent : MonoBehaviour {

    public enum CollisionType
    {
        CollionEnter,
        CollisionStay,
        CollisionExit,
        TriggerEnter,
        TriggerStay,
        TriggerExit
    };

    [SerializeField] CollisionType collision;
    public CollisionType Collision { get { return collision; } }

    [SerializeField] AnimationCurve humor;
    [SerializeField] AnimationCurve alcool;

    [SerializeField]
    ActionEnum.Action[] actions;

    [SerializeField]
    GameObject[] goOfAction;
    
    virtual public void SetAIAction(ADrunkAI ai, float aiHumor, float aiAlcool)
    {
        if (((float)(UnityEngine.Random.Range(0, 101)) / 100.0f) < ((humor.Evaluate(aiHumor) + alcool.Evaluate(aiAlcool)) / 2))
        {
            print("event proc, modification of the actions !!");
            Action(ai);
        }
    }

    virtual protected void Action(ADrunkAI ai)
    {
        ai.ActionDone();
        for (int i = 0; i < actions.Length; i++)
        {
            ai.AddAction(actions[i], goOfAction[i]);
        }
    }
}
