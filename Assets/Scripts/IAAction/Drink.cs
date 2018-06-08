using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : AAction
{
    private GameObject bottle;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Drink;
    }

    public override void DoAction()
    {
        Debug.Log("action Drink");
        if (!AI.HaveBottle())
        {
            bottle = elementManager.getNearestThrowableObject(AI.gameObject.transform.position);
            if (bottle == null)
                AI.AddAction(ActionEnum.Action.ActionDone, null);
            else
            {
                AI.AddAction(ActionEnum.Action.Walk, bottle);
                AI.SetTriggerCB(getBottleInPile);
                AI.SetCollisionCB(drink);
            }
        }
        else
        {
            AI.AddAction(ActionEnum.Action.Drink, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);
        }
    }

    private void drink(GameObject col, ADrunkAI ai)
    {
        if (col == bottle)
        {
            AI.SetCollisionCB(null);
            AI.SetTriggerCB(null);
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.GetBottle, col);
            AI.AddAction(ActionEnum.Action.Drink, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);
        }
    }

    private void getBottleInPile(GameObject col, ADrunkAI ai)
    {
        if (bottle == col)
        {
            Debug.Log("getBottlePile callback");
            AI.SetCollisionCB(null);
            AI.SetTriggerCB(null);
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.GetBottle, col);
            AI.AddAction(ActionEnum.Action.Drink, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);

        }
    }
}
