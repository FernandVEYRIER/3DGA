using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : AAction
{
    public override void Initialize(ADrunkAI ai)
    {
        base.Initialize(ai);
        action = ActionEnum.Action.Dance;
    }

    public override void DoAction()
    {
        Debug.Log("action Dance");
        AI.AddAction(ActionEnum.Action.Dance, null);
        AI.AddAction(ActionEnum.Action.ActionDone, null);
    }
}
