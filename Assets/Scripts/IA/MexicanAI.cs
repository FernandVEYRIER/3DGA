using Assets.Scripts.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MexicanAI : ADrunkAI
{

    [SerializeField] Image image;

    [SerializeField] float askingTimeMin, askingTimeMax, askingTimeDuration;
    [SerializeField] float numberOfTequila;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        print("basic drunk start");
        AskForDrink();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void DrinkTypeEffect(DrinkName name)
    {
        if (name == DrinkName.NOTHING)
            return;
        else if (DrinkWanted == name)
            numberOfTequila--;
        if(numberOfTequila == 0)
            forceLiving = true;
    }

    private void AskForDrink()
    {
        image.enabled = false;
        Invoke("AskDrink", UnityEngine.Random.Range(askingTimeMin, askingTimeMax));
    }

    private void AskDrink()
    {
        DrinkWanted = DrinkName.TEQUILA;
        image.enabled = true;
        Invoke("AskForDrink", askingTimeDuration);
    }
}
