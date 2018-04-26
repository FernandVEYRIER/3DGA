using UnityEngine;
using System.Collections;

public class LeaveBar : AAction
{

    private GameObject exit;

    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Leave;
    }

    public override void DoAction()
    {
        Debug.Log("action leave");
        exit = elementManager.GetNearestLeavingExit(AI.gameObject.transform.position);
        AI.AddAction(ActionEnum.Action.Walk, exit);
        AI.SetTriggerCB(Exit);
    }

    private void Exit(GameObject col, ADrunkAI ai)
    {
        if (col == exit)
        {
            AI.SetTriggerCB(null);
            AI.SetTriggerStayCB(null);
            AI.StopWalking();
            AI.AddAction(ActionEnum.Action.Leave, null);
            AI.AddAction(ActionEnum.Action.ActionDone, null);
        }
    }
}
