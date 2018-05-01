using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : AAction
{
    Vector3 dartPosition;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Dance;
    }

    public override void DoAction()
    {
        Debug.Log("action play Dart");
        dartPosition = elementManager.getPlaceToPlayDart(AI.gameObject.transform.position);
        if (dartPosition == Vector3.up)
        {
            AI.AddAction(ActionEnum.Action.ActionDone, null);
            return;
        }
        AI.forceDirection(dartPosition);
        AI.SetTriggerStayCB(PlayDart);
    }

    private void PlayDart(GameObject col, ADrunkAI ai)
    {
        if (Mathf.Abs(AI.gameObject.transform.position.x - dartPosition.x) < 0.1 && Mathf.Abs(AI.gameObject.transform.position.z - dartPosition.z) < 0.1)
        {
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.Dart, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);
        }
    }
}
