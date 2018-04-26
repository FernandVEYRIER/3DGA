
using System;
using UnityEngine;



public interface IDrunkAI {

    void GetDrunk(float amount);
    void GetHappy(float amount);

    void SetActionCB(Action<GameObject> CB);

    void SetCollisionCB(Action<GameObject, ADrunkAI> CB);
    void SetCollisionStayCB(Action<GameObject, ADrunkAI> CB);
    void SetCollisionExitCB(Action<GameObject, ADrunkAI> CB);

    void SetTriggerCB(Action<GameObject, ADrunkAI> CB);
    void SetTriggerStayCB(Action<GameObject, ADrunkAI> CB);
    void SetTriggerExitCB(Action<GameObject, ADrunkAI> CB);

    void AddAction(ActionEnum.Action newAction, GameObject go);

    void AnimationDone();

    void ActionDone();

    void StopWalking();
}
