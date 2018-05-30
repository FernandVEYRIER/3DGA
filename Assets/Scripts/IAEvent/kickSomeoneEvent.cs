using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickSomeoneEvent : AEvent {

    [SerializeField]
    ADrunkAI thisAI;

    protected override void Action(ADrunkAI otherai)
    {
        print("this Ai gonna take a kick");
        otherai.ActionDone();
        otherai.AddAction(ActionEnum.Action.Stun, null);
        base.Action(thisAI);
    }
}
