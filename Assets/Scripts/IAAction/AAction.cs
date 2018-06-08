using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAction : IAction
{
    public ActionEnum.Action Action { get { return action; } }

    protected ActionEnum.Action action;
    protected ADrunkAI AI;
    protected ElementManager elementManager;

    public virtual void Initialize(ADrunkAI ai)
    {
        AI = ai;
        elementManager = GameObject.Find("ElementManager").GetComponent<ElementManager>();
    }

    public virtual float GetPourcentage(float humor, float alcool)
    {
        return AI.Comportement.GetPourcentage(action, humor, alcool);
    }

    public virtual void DoAction()
    {

    }
}
