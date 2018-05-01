using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : AAction
{
    Vector3 dancePosition;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Dance;
    }

    public override void DoAction()
    {
        Debug.Log("action Dance");
        dancePosition = elementManager.getPlaceToDance(AI.gameObject.transform.position);
        if (dancePosition == Vector3.up)
        {
            AI.AddAction(ActionEnum.Action.ActionDone, null);
            return;
        }
        AI.forceDirection(dancePosition);
        AI.SetTriggerStayCB(DoDance);
    }

    private void DoDance(GameObject col, ADrunkAI ai)
    {
        if (Mathf.Abs(AI.gameObject.transform.position.x - dancePosition.x) < 0.1 && Mathf.Abs(AI.gameObject.transform.position.z - dancePosition.z) < 0.1)
        {
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.Dance, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);
        }
    }
}
