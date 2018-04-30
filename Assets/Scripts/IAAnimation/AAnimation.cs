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
        AI.StartCoroutine(HideAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    private IEnumerator HideAnimation(float time)
    {
        Debug.Log("waiting for : " + time);
        yield return new WaitForSeconds(time);
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
        AI.StartCoroutine(ThrowBotlleAnimation(GameObject.FindGameObjectWithTag("Player").transform.position));
    }

    private IEnumerator ThrowBotlleAnimation(Vector3 pos)
    {
        float time = 0;
        float second = 0;

        Vector3 lookPos = pos - AI.gameObject.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        Quaternion myRotation = AI.gameObject.transform.rotation;
        second = rotation.y / 2;
        while (time < second)
        {
            AI.gameObject.transform.rotation = Quaternion.Slerp(myRotation, rotation, time / second);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        AI.ThrowThisBottle(GameObject.FindGameObjectWithTag("Player").transform.position);
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

    public void Dance()
    {
        AI.StartCoroutine(DanceAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    private IEnumerator DanceAnimation(float time)
    {
        Debug.Log("dancing for : " + time);
        yield return new WaitForSeconds(time);
        AI.AnimationDone();
    }

    public void Drink()
    {
        AI.AnimationDone();
    }
}
