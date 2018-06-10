using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Effects;

public class ExplosionEvent : AEvent
{

    [SerializeField] private DrinkName drinkName;
    public DrinkName Drinks { get { return drinkName; } }

    private SphereCollider sphereColider;
    private float radius;

    public override void Setup(AEvent _event, Recipe recipe)
    {
        base.Setup(_event, recipe);
        drinkName = recipe.DrinkName;
        radius = 3; //yolo hard code
        foreach (var item in GetComponents<SphereCollider>())
        {
            if (item.isTrigger == true)
            {
                sphereColider = item;
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sphereColider)
            sphereColider.radius = radius;
    }

    protected override void Action(ADrunkAI ai)
    {
        print("bottle strick the IA !");
        base.Action(ai);
        ai.DrinkTypeEffect(drinkName);
    }
}
