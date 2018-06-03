using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickSomeoneEvent : AEvent {

    [SerializeField]
    ADrunkAI thisAI;

    public override void SetAIAction(ADrunkAI ai, float aiHumor, float aiAlcool)
    {
        if (Enable && ai.State == ADrunkAI.IAState.INTERACTEABLE && thisAI.State == ADrunkAI.IAState.INTERACTEABLE)
        {
            if (((float)(UnityEngine.Random.Range(0, 101)) / 100.0f) < ((humor.Evaluate(aiHumor) + alcool.Evaluate(aiAlcool)) / 2))
            {
                print("event proc, modification of the actions !!");
                ai.GetDrunk(amountOfAlcool);
                ai.GetHappy(amountOfJoyness);
                Action(ai);
            }
        }
    }

    protected override void Action(ADrunkAI otherai)
    {
        print("this Ai gonna take a kick");
        otherai.ActionDone();
        otherai.AddAction(ActionEnum.Action.Stun, null);
        base.Action(thisAI);
    }
}
