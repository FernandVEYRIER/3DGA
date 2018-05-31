using Assets.Scripts.Throwable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAnimation : IAnimation
{
    protected ADrunkAI AI;
    protected Animator animator;

    public void Initialize(ADrunkAI ai)
    {
        AI = ai;
        animator = AI.gameObject.GetComponent<Animator>();
    }

    private void StopCoroutines()
    {
        AI.StopAllCoroutines();
    }

    public void GetBottle(GameObject bottle)
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("pickingUp", true);
        AI.StartCoroutine(GetBottleAnimation(bottle));
    }

    protected virtual IEnumerator GetBottleAnimation(GameObject bottle)
    {
        Debug.Log("start picking up");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("PickingUp"))
            yield return new WaitForFixedUpdate();
//        AI.AIanimator.SetBool("pickingUp", false);
        yield return new WaitForSeconds(0.5f);
        bottle.GetComponent<Rigidbody>().isKinematic = true;
        AI.Bottle = bottle;
        bottle.GetComponent<AThrowable>().Grab(AI.Hand.transform);
        bottle.transform.rotation = AI.Hand.transform.rotation;
        bottle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.5f);

        /*while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Default"))
        {
            yield return new WaitForFixedUpdate();
        }*/
        AI.AnimationDone();
        yield return 0;
    }

    public void Hide()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("hide", true);
        AI.StartCoroutine(HideAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    protected virtual IEnumerator HideAnimation(float time)
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

    protected virtual IEnumerator ThrowBotlleAnimation(Vector3 pos)
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
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Throw"))
            yield return new WaitForFixedUpdate();
        AI.AIanimator.SetBool("throw", false);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Default"))
            yield return new WaitForFixedUpdate();
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
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("kick", true);
        AI.StartCoroutine(KickAnimation());
    }

    protected virtual IEnumerator KickAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Kick"))
            yield return new WaitForFixedUpdate();
        AI.AIanimator.SetBool("kick", false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AI.AnimationDone();
        yield return 0;
    }

    public void Slip()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("slip", true);
        AI.StartCoroutine(SlipAnimation());
    }

    protected virtual IEnumerator SlipAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Slip"))
            yield return new WaitForFixedUpdate();
        AI.AIanimator.SetBool("slip", false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.1f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Slip"))
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AI.AnimationDone();
        yield return 0;
    }

    public void Stun()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("stun", true);
        AI.StartCoroutine(StunAnimation());
    }

    protected virtual IEnumerator StunAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Stun"))
            yield return new WaitForFixedUpdate();
        AI.AIanimator.SetBool("stun", false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.1f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Stun"))
            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AI.AnimationDone();
        yield return 0;
    }

    public void Dance()
    {
        StopCoroutines();
        AI.AIanimator.SetInteger("danceNbr", UnityEngine.Random.Range(0, 4));
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("dancing", true);
        AI.StartCoroutine(DanceAnimation());
    }

    protected virtual IEnumerator DanceAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Dance"))
            yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AI.AnimationDone();
        yield return 0;
    }

    public void Drink()
    {
        StopCoroutines();
        AI.AIanimator.SetBool("Action", true);
        AI.AIanimator.SetBool("drinking", true);
        AI.StartCoroutine(DrinkAnimation());
    }

    protected virtual IEnumerator DrinkAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Drinking"))
            yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AI.AnimationDone();
        yield return 0;
    }

    public void Dart()
    {
        StopCoroutines();
        AI.StartCoroutine(DartAnimation(UnityEngine.Random.Range(0.0f, 5.0f)));
    }

    protected virtual IEnumerator DartAnimation(float time)
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
