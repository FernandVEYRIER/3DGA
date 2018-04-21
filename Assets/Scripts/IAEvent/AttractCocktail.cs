using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCocktail : AEvent
{
    private SphereCollider sphere;

    private void Start()
    {
        sphere = gameObject.GetComponent<SphereCollider>();
    }

    protected override void Action(ADrunkAI ai)
    {
        base.Action(ai);
        ai.SetCollisionCB(getCocktail);
        ai.SetTriggerExitCB(CocktailDisappear);
    }

    private void getCocktail(GameObject col, ADrunkAI ai)
    {
        print("colision with the cocktail");
        if (col == gameObject)
        {
            print("ia getting cocktail, disable the sphere trigger");
            sphere.radius = 0;
            ai.StopWalking();
        }
    }

     private void CocktailDisappear(GameObject col, ADrunkAI ai)
    {
        print("trigger cocktail disappear");
        if (col == gameObject)
        {
            print("cocktail desapear, ai change his mind");
            ai.ActionDone();
        }
    }
}
