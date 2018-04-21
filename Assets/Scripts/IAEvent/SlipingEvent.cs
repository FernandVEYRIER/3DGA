using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipingEvent : AEvent {

    protected override void Action(ADrunkAI ai)
    {
        print("sliping on the floor !!!");
        base.Action(ai);
    }
}
