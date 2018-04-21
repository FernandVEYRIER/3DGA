using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ADrunkAI : MonoBehaviour, IDrunkAI {

    [SerializeField]
    protected string[] ActionClassName;

    [SerializeField] 
    [Range(0.0f, 1.0f)]
    protected float humor;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    protected float alcool;

    protected NavMeshAgent nav;
    protected Action<GameObject> actionCB;
    protected Action<GameObject> actionCCB;
    protected Action<GameObject> actionCSCB;
    protected Action<GameObject> actionCECB;
    protected Action<GameObject> actionTCB;
    protected Action<GameObject> actionTSCB;
    protected Action<GameObject> actionTECB;
    protected List<AAction> actions;
    protected List<ActionEnum.ActionData> actionList;
    protected Dictionary<ActionEnum.Action, Action> actionMethode;

    protected GameObject bottle;
    protected bool walking;
    protected bool anim;

    // Use this for initialization
    protected virtual void Start() {
        print("ADrunk start");
        nav = GetComponent<NavMeshAgent>();
        actions = new List<AAction>();
        actionList = new List<ActionEnum.ActionData>();
        actionMethode = new Dictionary<ActionEnum.Action, Action>();

        actionMethode.Add(ActionEnum.Action.ActionDone, ActionDone);
        actionMethode.Add(ActionEnum.Action.Walk, SetDirection);
        actionMethode.Add(ActionEnum.Action.GetBottle, GetBottle);
        actionMethode.Add(ActionEnum.Action.ThrowBottle, ThrowBottle);
        actionMethode.Add(ActionEnum.Action.Hide, Hide);
        actionMethode.Add(ActionEnum.Action.Sit, Sit);

        walking = false;
        anim = false;
        bottle = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!walking && !anim && actionList.Count != 0)
        {
            actionMethode[actionList[0].type]();
        }
        if (actionList.Count == 0)
            SelectAction();
    }

    public void SetActionCB(Action<GameObject> CB) { actionCB = CB; }

    public void SetCollisionCB(Action<GameObject> CB) { actionCCB = CB; }
    public void SetCollisionStayCB(Action<GameObject> CB) { actionCSCB = CB; }
    public void SetCollisionExitCB(Action<GameObject> CB) { actionCECB = CB; }

    public void SetTriggerCB(Action<GameObject> CB) { actionTCB = CB; }
    public void SetTriggerStayCB(Action<GameObject> CB) { actionTSCB = CB; }
    public void SetTriggerExitCB(Action<GameObject> CB) { actionTECB = CB; }


    public void AddAction(ActionEnum.Action newAction, GameObject go)
    {
        actionList.Add(new ActionEnum.ActionData(newAction, go));
    }

    public void ActionDone()
    {
        StopWalking();
        actionCB = null;
        actionList.Clear();
    }

    public void StopWalking()
    {
        nav.SetDestination(gameObject.transform.position);
        walking = false;
        if (actionList[0].type == ActionEnum.Action.Walk)
            AnimationDone();
    }

    public void AnimationDone()
    {
        actionList.RemoveAt(0);
        anim = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("colision");
        if (actionCCB != null)
            actionCCB(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("trigger");
        if (actionTCB != null)
            actionTCB(collision.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (actionTSCB != null)
            actionTSCB(other.gameObject);
    }

    //internal usefull action of the AI
    #region usefull action

    protected void SetDirection()
    {
        nav.SetDestination(actionList[0].go.transform.position);
        walking = true;
    }

    protected void SelectAction()
    {
        int actionToPlay = 0;

        for (int i = 0; i < actions.Count; i++)
        {
            float biggestValue = 0;
            float value = (float)(UnityEngine.Random.Range(0, 101)) / 100.0f;
            print("random value: " + value + "chance was: " + actions[i].GetPourcentage(humor, alcool));
            if (value < actions[i].GetPourcentage(humor, alcool))
            {
                if (biggestValue < value /*+ actions[i].GetPourcentage(humor, alcool)*/)
                {
                    biggestValue = value /*+ actions[i].GetPourcentage(humor, alcool)*/;
                    actionToPlay = i;
                }
            }
        }
        actions[actionToPlay].DoAction();
    }
    #endregion usefull action

    //throwing bootle action
    #region throw

    //return true if bottle
    public bool HaveBottle()
    {
       return bottle != null ? true : false;
    }

    //get the bottle on the floor
    protected void GetBottle()
    {
        Destroy(actionList[0].go);
        AnimationDone();
    }

    //throw the bottle
    protected void ThrowBottle()
    {
        AnimationDone();
    }
    #endregion throw


    //hidding action
    #region hide

    //hide the AI behind an object
    protected void Hide()
    {
        AnimationDone();
    }
    #endregion hide

    //siting action
    #region sit

    //sit the AI on a chair
    protected void Sit()
    {
        AnimationDone();
    }
    #endregion sit
}
