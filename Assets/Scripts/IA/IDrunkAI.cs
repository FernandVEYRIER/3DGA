
using System;
using UnityEngine;



public interface IDrunkAI {

    void SetActionCB(Action<GameObject> CB);

    void SetCollisionCB(Action<GameObject> CB);
    void SetCollisionStayCB(Action<GameObject> CB);
    void SetCollisionExitCB(Action<GameObject> CB);

    void SetTriggerCB(Action<GameObject> CB);
    void SetTriggerStayCB(Action<GameObject> CB);
    void SetTriggerExitCB(Action<GameObject> CB);

    void AddAction(ActionEnum.Action newAction, GameObject go);

    void AnimationDone();

    void ActionDone();

    void StopWalking();
}
