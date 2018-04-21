using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAction : IAction {

    protected PourcentageMaker pourcentage;
    protected ADrunkAI AI;
    protected ElementManager elementManager;

    public virtual void Initialize(ADrunkAI ai)
    {
        AI = ai;
        elementManager = GameObject.Find("ElementManager").GetComponent<ElementManager>();
    }

    public virtual float GetPourcentage(float humor, float alcool)
    {
        if (pourcentage != null)
            return pourcentage.GetPourcentage(humor, alcool);
        return 0.0f;
    }

    public virtual void DoAction()
    {

    }
}
