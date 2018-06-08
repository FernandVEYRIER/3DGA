using Assets.Scripts.Effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicDrunk : ADrunkAI {

    [SerializeField] Image image;

    [SerializeField] float askingTimeMin, askingTimeMax, askingTimeDuration;
    [SerializeField] DrinkSpriteDictionary drinksSelection;

	// Use this for initialization
	protected override void  Start () {
        base.Start();
        print("basic drunk start");
        AskForDrink();
        image.enabled = false;
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    private void AskForDrink()
    {
        image.enabled = false;
        Invoke("AskDrink", UnityEngine.Random.Range(askingTimeMin, askingTimeMax));
    }

    private void AskDrink()
    {
        DrinkWanted = DrinkName.KAMIKAZE;//(DrinkName)UnityEngine.Random.Range(0, drinksSelection.Count);
        image.enabled = true;
        image.sprite = drinksSelection[DrinkWanted];
        Invoke("AskForDrink", askingTimeDuration);
    }
}
