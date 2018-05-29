using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEvent : MonoBehaviour {

    public bool Enable = true;

    [SerializeField]
    [Range(-1.0f, 1.0f)]
    public float amountOfAlcool, amountOfJoyness = 0;

    public enum CollisionType
    {
        CollionEnter,
        CollisionStay,
        CollisionExit,
        TriggerEnter,
        TriggerStay,
        TriggerExit
    };

    [SerializeField] CollisionType collision;

    [SerializeField] AnimationCurve humor;
    [SerializeField] AnimationCurve alcool;

    [SerializeField] List<ActionEnum.Action> actions = new List<ActionEnum.Action>();
    [SerializeField] List<GameObject> goOfAction = new List<GameObject>();

    public CollisionType Collision { get { return collision; } }
    public AnimationCurve Humor { get { return humor; } }
    public AnimationCurve Alcool { get { return alcool; } }
    public List<ActionEnum.Action> Actions { get { return actions; } }
    public List<GameObject> GoOfAction { get { return goOfAction; } }

    public void ResetActions()
    {
        actions.Clear();
        goOfAction.Clear();
    }

    public void AddAction(ActionEnum.Action action, GameObject go = null)
    {
        actions.Add(action);
        goOfAction.Add(go);
    }

    virtual public void Setup(AEvent _event)
    {
        amountOfAlcool = _event.amountOfAlcool;
        amountOfJoyness = _event.amountOfJoyness;

        collision = _event.Collision;
        humor = _event.Humor;
        alcool = _event.Alcool;
        actions = _event.Actions;
        goOfAction = _event.GoOfAction;
    }
    
    virtual public void SetAIAction(ADrunkAI ai, float aiHumor, float aiAlcool)
    {
        if (Enable)
        {
            if (((float)(UnityEngine.Random.Range(0, 101)) / 100.0f) < ((humor.Evaluate(aiHumor) + alcool.Evaluate(aiAlcool)) / 2))
            {
                print("event proc, modification of the actions !!");
                ai.GetDrunk(amountOfAlcool);
                ai.GetHappy(amountOfJoyness);
                Action(ai);
            }
        }
    }

    virtual protected void Action(ADrunkAI ai)
    {
        ai.ActionDone();
        for (int i = 0; i < actions.Count; i++)
        {
            ai.AddAction(actions[i], goOfAction[i]);
        }
    }
}
