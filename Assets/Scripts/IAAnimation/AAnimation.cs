using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAnimation : IAnimation
{
    protected ADrunkAI AI;

    public void Initialize(ADrunkAI ai)
    {
        AI = ai;
    }

    public void GetBottle()
    {
        AI.AnimationDone();
    }

    public void Hide()
    {
        AI.AnimationDone();
    }

    public float Leave()
    {
        AI.AnimationDone();
        return (0);
    }

    public void Sit()
    {
        AI.AnimationDone();
    }

    public void ThrowBottle()
    {
        AI.AnimationDone();
    }

    public void Walk()
    {
        return;
    }

    public void Kick()
    {
        AI.AnimationDone();
    }

    public void Slip()
    {
        AI.AnimationDone();
    }

    public void Stun()
    {
        AI.AnimationDone();
    }
}
