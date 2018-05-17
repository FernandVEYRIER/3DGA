using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using com.Collections;
using Assets.Scripts.Throwable;

public abstract class ADrunkAI : MonoBehaviour, IDrunkAI {

    [System.Serializable()]
    public class LocalDictionary : SerializableDictionaryBase<ActionEnum.Action, float>
    {}

    //just for debug
    [SerializeField]
    private ActionEnum.Action actiondebug;
    //serialised for debug nothing else

    [SerializeField]
    protected Animator animator;
    public Animator AIanimator { get { return animator; } }

    [SerializeField]
    private GameObject destination;
    public bool forceActionDone;

    [SerializeField]
    protected string[] ActionClassName;
    [SerializeField]
    protected string AnimationClassName;
    [SerializeField] 
    [Range(0.0f, 1.0f)]
    protected float humor, alcool;
    [SerializeField]
    protected GameObject hand, AiNavDirection;
    [SerializeField()]
    LocalDictionary alcoolPerAction, humorPerAction;

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
    protected AIComportement comportement;
    public AIComportement Comportement { get { return comportement; } }

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
        comportement = gameObject.GetComponent<AIComportement>();

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
        actionMethode.Add(ActionEnum.Action.Dance, Dance);
        actionMethode.Add(ActionEnum.Action.Drink, Drink);
        actionMethode.Add(ActionEnum.Action.Dart, Dart);
        actionMethode.Add(ActionEnum.Action.Fire, Fire);

        walking = false;
        anim = false;
        bottle = null;
        destination = null;
        forceActionDone = false;

        animator.SetFloat("alcool", alcool);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (forceActionDone)
            ActionDone();

        if (walking && (destination == null || destination.transform.position.x != nav.destination.x || destination.transform.position.z != nav.destination.z))
            ActionDone();
        if (!walking && !anim && actionList.Count != 0)
        {
            actiondebug = actionList[0].type; //to delete, juste for debug
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
        actiondebug = ActionEnum.Action.Nothing; //to delete, juste for debug
        StopWalking();
        actionCB = null;
        anim = false;
        ResetCallback();
        actionList.Clear();
    }

    public void StopWalking()
    {
        actiondebug = ActionEnum.Action.Nothing; //to delete, juste for debug
        destination = null;
        nav.SetDestination(gameObject.transform.position);
        walking = false;
        if (actionList.Count > 0 && actionList[0].type == ActionEnum.Action.Walk)
            AnimationDone();
    }

    public void AnimationDone()
    {
        if (actionList.Count > 0)
            actionList.RemoveAt(0);
        anim = false;
        animator.SetBool("Action", false);
        animator.SetBool("walking", false);
        animator.SetBool("throw", false);
        animator.SetBool("pickingUp", false);
        animator.SetBool("drinking", false);
        animator.SetBool("dancing", false);
        animator.SetBool("hide", false);
    }

    //return true if AI got bottle
    public bool HaveBottle()
    {
        return bottle != null ? true : false;
    }

    public void ThrowThisBottle(Vector3 pos)
    {
        if (bottle == null)
            return;
        bottle.GetComponent<AThrowable>().Throw(pos);
        bottle = null;
    }

    //every modification of alcool is made here
    public void GetDrunk(float amount)
    {
        alcool += amount;
        alcool = alcool < 0 ? 0 : alcool > 1 ? 1 : alcool;
        animator.SetFloat("alcool", alcool);
    }

    //every modification of humor is made here
    public void GetHappy(float amount)
    {
        humor += amount;
        humor = humor < 0 ? 0 : humor > 1 ? 1 : humor;
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

    public void forceDirection(Vector3 position)
    {
        ActionDone();
        AiNavDirection.transform.parent = null;
        AiNavDirection.transform.position = position;
        AddAction(ActionEnum.Action.Walk, AiNavDirection);
    }

    protected void SetDirection()
    {
        if (actionList[0].go == null)
            ActionDone();
        else
        {
            nav.SetDestination(actionList[0].go.transform.position);
            destination = actionList[0].go;
            actionBase(ActionEnum.Action.Walk);
            walking = true;
            animations.Walk();
        }
    }

    protected void SelectAction()
    {
        int actionToPlay = -1;

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
        if (actionToPlay != -1)
            actions[actionToPlay].DoAction();
    }
    #endregion usefull action

    //all action of the AI
    #region Action

    protected void actionBase(ActionEnum.Action action)
    {
        anim = true;
        if (alcoolPerAction.ContainsKey(action))
            GetDrunk(alcoolPerAction[action]);
        if (humorPerAction.ContainsKey(action))
            GetHappy(humorPerAction[action]);
    }

    //get the bootle in the actionList
    protected void GetBottle()
    {
        if (actionList[0].go.GetComponent<AThrowable>() == null)
        {
            Destroy(actionList[0].go);
            AnimationDone();
        }
        else
        {
            actionBase(ActionEnum.Action.GetBottle);
            bottle = actionList[0].go;
            bottle.GetComponent<AThrowable>().Grab(hand.transform);
            bottle.transform.rotation = hand.transform.rotation;
            bottle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            bottle.GetComponent<Rigidbody>().isKinematic = true;
            bottle.tag = "Untagged";
            animations.GetBottle();
        }
    }

    //throw the bottle hold by the AI
    protected void ThrowBottle()
    {
        if (bottle == null)
            AnimationDone();
        else
        {
            actionBase(ActionEnum.Action.ThrowBottle);
            bottle.GetComponent<Rigidbody>().isKinematic = false;
            if (bottle.GetComponent<AEvent>() != null)
                bottle.GetComponent<AEvent>().Enable = true;
            animations.ThrowBottle();
        }
    }

    //hide the AI behind an object
    protected void Hide()
    {
        actionBase(ActionEnum.Action.Hide);
        animations.Hide();
    }

    //sit the AI on a chair
    protected void Sit()
    {
        actionBase(ActionEnum.Action.Sit);
        animations.Sit();
    }

    //make the AI leave
    protected void Leave()
    {
        actionBase(ActionEnum.Action.Leave);
        Destroy(gameObject, animations.Leave());
    }

    //kick an other AI
    protected void Kick()
    {
        actionBase(ActionEnum.Action.Kick);
        print("kicking someone hahaha !");
        animations.Kick();
    }

    //slip on the floor
    protected void Slip()
    {
        actionBase(ActionEnum.Action.Slip);
        animations.Slip();
    }

    //get Stun by bottle or a kick
    protected void Stun()
    {
        actionBase(ActionEnum.Action.Stun);
        animations.Stun();
    }

    //Ia gonna dance
    protected void Dance()
    {
        actionBase(ActionEnum.Action.Dance);
        animations.Dance();
    }

    //IA gonna drink is bottle
    protected void Drink()
    {
        actionBase(ActionEnum.Action.Drink);
        animations.Drink();
    }

    protected void Dart()
    {
        actionBase(ActionEnum.Action.Dart);
        animations.Dart();
    }

    protected void Fire()
    {
        actionBase(ActionEnum.Action.Fire);

    }
    #endregion Action
}
