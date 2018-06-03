using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStrickEvent : AEvent {

    protected override void Action(ADrunkAI ai)
    {
        print("bottle strick the IA !");
        base.Action(ai);
        //Destroy(gameObject);
    }
}
