using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleEvent : AEvent {

    public override void SetAIAction(ADrunkAI ai, float aiHumor, float aiAlcool)
    {
        if (!ai.HaveBottle())
            base.SetAIAction(ai, aiHumor, aiAlcool);
    }
}
