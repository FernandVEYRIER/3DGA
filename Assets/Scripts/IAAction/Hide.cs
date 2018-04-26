using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : AAction
{

    private GameObject hiddingPlace;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Hide;
    }

    public override void DoAction()
    {
        Debug.Log("action hide");
        hiddingPlace = elementManager.GetRandomHiddingSpot(AI.gameObject.transform.position);
        AI.AddAction(ActionEnum.Action.Walk, hiddingPlace);
        AI.SetTriggerCB(hide);
        AI.SetTriggerStayCB(hide);
    }

    private void hide(GameObject col, ADrunkAI ai)
    {
        if (col == hiddingPlace)
        {
            AI.SetTriggerCB(null);
            AI.SetTriggerStayCB(null);
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.Hide, null);
        }
    }
}
