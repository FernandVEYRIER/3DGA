using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ADrunkAI : MonoBehaviour, IDrunkAI {

    [SerializeField]
    protected string[] ActionClassName;

    [SerializeField]
    protected string AnimationClassName;

    [SerializeField] 
    [Range(0.0f, 1.0f)]
    protected float humor;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    protected float alcool;

    protected NavMeshAgent nav;
    protected Action<GameObject> actionCB;
    protected Action<GameObject, ADrunkAI> actionCCB;
    protected Action<GameObject, ADrunkAI> actionCSCB;
    protected Action<GameObject, ADrunkAI> actionCECB;
    protected Action<GameObject, ADrunkAI> actionTCB;
    protected Action<GameObject, ADrunkAI> actionTSCB;
    protected Action<GameObject, ADrunkAI> actionTECB;
    protected AAnimation animations;
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
        actionList = new List<ActionEnum.ActionData>();
        actionMethode = new Dictionary<ActionEnum.Action, Action>();
        actions = new List<AAction>();

        animations = (AAnimation)Activator.CreateInstance(Type.GetType(AnimationClassName));
        animations.Initialize(this);

        foreach (string s in ActionClassName)
        {
            actions.Add((AAction)Activator.CreateInstance(Type.GetType(s)));
            actions[actions.Count - 1].Initialize(this);
        }

        actionMethode.Add(ActionEnum.Action.ActionDone, ActionDone);
        actionMethode.Add(ActionEnum.Action.Walk, SetDirection);
        actionMethode.Add(ActionEnum.Action.GetBottle, GetBottle);
        actionMethode.Add(ActionEnum.Action.ThrowBottle, ThrowBottle);
        actionMethode.Add(ActionEnum.Action.Hide, Hide);
        actionMethode.Add(ActionEnum.Action.Sit, Sit);
        actionMethode.Add(ActionEnum.Action.Leave, Leave);
        actionMethode.Add(ActionEnum.Action.Kick, Kick);
        actionMethode.Add(ActionEnum.Action.Slip, Slip);
        actionMethode.Add(ActionEnum.Action.Stun, Stun);


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

    public void SetCollisionCB(Action<GameObject, ADrunkAI> CB) { actionCCB = CB; }
    public void SetCollisionStayCB(Action<GameObject, ADrunkAI> CB) { actionCSCB = CB; }
    public void SetCollisionExitCB(Action<GameObject, ADrunkAI> CB) { actionCECB = CB; }

    public void SetTriggerCB(Action<GameObject, ADrunkAI> CB) { actionTCB = CB; }
    public void SetTriggerStayCB(Action<GameObject, ADrunkAI> CB) { actionTSCB = CB; }
    public void SetTriggerExitCB(Action<GameObject, ADrunkAI> CB) { actionTECB = CB; }


    public void AddAction(ActionEnum.Action newAction, GameObject go)
    {
        actionList.Add(new ActionEnum.ActionData(newAction, go));
    }

    public void ActionDone()
    {
        StopWalking();
        actionCB = null;
        anim = false;
        ResetCallback();
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
        if (actionCCB != null)
            actionCCB(collision.gameObject, this);
        if (collision.gameObject.GetComponent<AEvent>() != null &&
            collision.gameObject.GetComponent<AEvent>().Collision == AEvent.CollisionType.CollionEnter)
        {
            collision.gameObject.GetComponent<AEvent>().SetAIAction(this, humor, alcool);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (actionTCB != null)
            actionTCB(collision.gameObject, this);
        if (collision.gameObject.GetComponent<AEvent>() != null &&
            collision.gameObject.GetComponent<AEvent>().Collision == AEvent.CollisionType.TriggerEnter)
        {
            collision.gameObject.GetComponent<AEvent>().SetAIAction(this, humor, alcool);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (actionTSCB != null)
            actionTSCB(other.gameObject, this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (actionTECB != null)
            actionTECB(other.gameObject, this);
    }

    private void ResetCallback()
    {
         actionCB = null;
         actionCCB = null;
         actionCSCB = null;
         actionCECB = null;
         actionTCB = null;
         actionTSCB = null;
         actionTECB = null;
    }

    //internal usefull action of the AI
    #region usefull action

    protected void SetDirection()
    {
        nav.SetDestination(actionList[0].go.transform.position);
        walking = true;
        animations.Walk();
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

    public bool HaveBottle()
    {
       return bottle != null ? true : false;
    }

    protected void GetBottle()
    {
        anim = true;
        Destroy(actionList[0].go, 0.1f);
        animations.GetBottle();
    }

    protected void ThrowBottle()
    {
        anim = true;
        animations.ThrowBottle();
    }
    #endregion throw


    //hidding action
    #region hide

    //hide the AI behind an object
    protected void Hide()
    {
        anim = true;
        animations.Hide();
    }
    #endregion hide

    //siting action
    #region sit

    //sit the AI on a chair
    protected void Sit()
    {
        anim = true;
        animations.Sit();
    }
    #endregion sit

    //siting action
    #region leave

    //make the AI leave
    protected void Leave()
    {
        anim = true;
        Destroy(gameObject, animations.Leave());
    }
    #endregion leave

    //kick an other AI
    protected void Kick()
    {
        anim = true;
        print("kicking someone hahaha !");
        animations.Kick();
    }

    //slip on the floor
    protected void Slip()
    {
        anim = true;
        animations.Slip();
    }

    //get Stun by bottle or a kick
    protected void Stun()
    {
        anim = true;
        animations.Stun();
    }
}
