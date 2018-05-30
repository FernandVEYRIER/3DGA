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

    private void StopCoroutines()
    {
        AI.StopAllCoroutines();
    }

    public void GetBottle()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void Hide()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("hide", true);
        AI.StartCoroutine(HideAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    private IEnumerator HideAnimation(float time)
    {
        Debug.Log("waiting for : " + time);
        yield return new WaitForSeconds(time);
        AI.AnimationDone();
        yield return 0;
    }

    public float Leave()
    {
        AI.AnimationDone();
        return (0);
    }

    public void Sit()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void ThrowBottle()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("throw", true);
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
        // les secondes c'est yolo
        second = Mathf.Abs(rotation.eulerAngles.y - myRotation.eulerAngles.y);
        second = Mathf.Abs(360 + rotation.eulerAngles.y - myRotation.eulerAngles.y) < second ? Mathf.Abs(360 + rotation.eulerAngles.y - myRotation.eulerAngles.y) : second;
        second = Mathf.Abs(rotation.eulerAngles.y - (360 + myRotation.eulerAngles.y)) < second ? Mathf.Abs(rotation.eulerAngles.y - (360 + myRotation.eulerAngles.y)) : second;
        second /= 200;
        while (time < second)
        {
            AI.gameObject.transform.rotation = Quaternion.Slerp(myRotation, rotation, time / second);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
        AI.ThrowThisBottle(GameObject.FindGameObjectWithTag("Player").transform.position);
        AI.AnimationDone();
        yield return 0;
    }

    public void Walk()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("walking", true);
        return;
    }

    public void Kick()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void Slip()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void Stun()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void Dance()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("dancing", true);
        AI.StartCoroutine(DanceAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    private IEnumerator DanceAnimation(float time)
    {
        Debug.Log("dancing for : " + time);
        yield return new WaitForSeconds(time);
        AI.AnimationDone();
        yield return 0;
    }

    public void Drink()
    {
        StopCoroutines();
        AI.AnimationDone();
    }

    public void Dart()
    {
        StopCoroutines();
        AI.StartCoroutine(DartAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    private IEnumerator DartAnimation(float time)
    {
        Debug.Log("playing dart for : " + time);
        yield return new WaitForSeconds(time);
        AI.AnimationDone();
        yield return 0;
    }

    public void Fire()
    {
        StopCoroutines();
        AI.AnimationDone();
    }
}
