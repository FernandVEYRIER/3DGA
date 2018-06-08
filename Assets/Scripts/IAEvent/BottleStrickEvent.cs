using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStrickEvent : AEvent {

    [SerializeField] private DrinkName drinkName;
    public DrinkName Drinks { get { return drinkName; } }

    public override void Setup(AEvent _event, Recipe recipe)
    {
        base.Setup(_event, recipe);
        drinkName = recipe.DrinkName;
    }

    protected override void Action(ADrunkAI ai)
    {
        print("bottle strick the IA !");
        base.Action(ai);
        ai.DrinkTypeEffect(drinkName);
    }
}
