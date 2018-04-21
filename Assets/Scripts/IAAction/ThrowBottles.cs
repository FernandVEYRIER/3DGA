using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBottles : AAction {

    private GameObject bottle;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        pourcentage = GameObject.Find("throwBottles").GetComponent<PourcentageMaker>();
    }

    public override void DoAction()
    {
        Debug.Log("action throw bottle");
        if (!AI.HaveBottle())
        {
            bottle = elementManager.getNearestThrowableObject(AI.gameObject.transform.position);
            if (bottle == null)
                AI.AddAction(ActionEnum.Action.ActionDone, null);
            else
            {
                AI.AddAction(ActionEnum.Action.Walk, bottle);
                AI.SetCollisionCB(getBottle);
            }
        }
        else
            ThrowThisBottle();
    }

    private void getBottle(GameObject col, ADrunkAI ai)
    {
        if (bottle == col)
        {
            Debug.Log("getBottle callback");
            AI.SetCollisionCB(null);
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.GetBottle, col);
            ThrowThisBottle();
        }
    }

    private void ThrowThisBottle()
    {
        AI.AddAction(ActionEnum.Action.ThrowBottle, null);
        AI.AddAction(ActionEnum.Action.ActionDone, null);
    }
}
