using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Throwable;
using Assets.Scripts.Effects;
using UnityEngine.Events;
using UnityEditor.Animations;

public abstract class ADrunkAI : MonoBehaviour, IDrunkAI {

    public enum IAState
    {
        INTERACTEABLE,
        UNINTERACTEABLE
    }


    //just for debug
    [SerializeField]
    private ActionEnum.Action currentAction;
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
    public GameObject Hand { get { return hand; } }
    [SerializeField()]
    ActionFloatDictionary alcoolPerAction, humorPerAction;

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
    //protected CapsuleCollider collider;

    protected GameObject bottle;
    public GameObject Bottle { set { bottle = value; }}
    protected bool walking;
    protected bool anim;
    protected DrinkName DrinkWanted;
    protected bool forceLiving;

    private IAState state = IAState.INTERACTEABLE;
    public IAState State { get { return state; } }

    // Use this for initialization
    protected virtual void Start() {
        print("ADrunk start");
        nav = GetComponent<NavMeshAgent>();
        actionList = new List<ActionEnum.ActionData>();
        actionMethode = new Dictionary<ActionEnum.Action, Action>();
        actions = new List<AAction>();
        comportement = gameObject.GetComponent<AIComportement>();
        /*foreach(CapsuleCollider col in gameObject.GetComponents<CapsuleCollider>())
        {
            if (!col.isTrigger)
                collider = col;
        }*/ //maybe later, was use to disable the collider when AI fall

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
        actionMethode.Add(ActionEnum.Action.BottleStrick, BottleStrick);
        actionMethode.Add(ActionEnum.Action.Dance, Dance);
        actionMethode.Add(ActionEnum.Action.Drink, Drink);
        actionMethode.Add(ActionEnum.Action.Dart, Dart);
        actionMethode.Add(ActionEnum.Action.Fire, Fire);

        walking = false;
        anim = false;
        bottle = null;
        destination = null;
        forceActionDone = false;
        DrinkWanted = DrinkName.NOTHING;
        forceLiving = false;

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
            currentAction = actionList[0].type; //to delete, juste for debug
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
        currentAction = ActionEnum.Action.Nothing; //to delete, juste for debug
        StopWalking();
        actionCB = null;
        ResetCallback();
        actionList.Clear();
        AnimationDone();
        StopAllCoroutines();
    }

    public void StopWalking()
    {
        currentAction = ActionEnum.Action.Nothing; //to delete, juste for debug
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
        animator.SetBool("kick", false);
        animator.SetBool("stun", false);
        animator.SetBool("bottleStrick", false);
        animator.SetBool("slip", false);
        state = IAState.INTERACTEABLE;
    }

    //return true if AI got bottle
    public bool HaveBottle()
    {
        return bottle != null ? true : false;
    }

    public void ThrowThisBottle()
    {
        if (bottle == null || actionList[0].type != ActionEnum.Action.ThrowBottle)
            return;
        bottle.GetComponent<Rigidbody>().isKinematic = false;
        bottle.GetComponent<AThrowable>().Throw(GameObject.FindGameObjectWithTag("Player").transform.position);
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

    public void DrinkTypeEffect(DrinkName name)
    {
        if (name == DrinkName.NOTHING)
            return;
        else if (DrinkWanted == name)
            forceLiving = true;
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

        if (forceLiving == true)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].Action == ActionEnum.Action.Leave)
                {
                    actionToPlay = i;
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < actions.Count; i++)
            {
                float biggestValue = 0;
                float value = (float)(UnityEngine.Random.Range(0, 101)) / 100.0f;
                //print("random value: " + value + "chance was: " + actions[i].GetPourcentage(humor, alcool));
                if (value < actions[i].GetPourcentage(humor, alcool))
                {
                    if (biggestValue < value /*+ actions[i].GetPourcentage(humor, alcool)*/)
                    {
                        biggestValue = value /*+ actions[i].GetPourcentage(humor, alcool)*/;
                        actionToPlay = i;
                    }
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
        if (actionList[0].go.GetComponent<AThrowable>() == null && actionList[0].go.GetComponent<BottlePileSpawner>() == null)
        {
            Destroy(actionList[0].go);
            AnimationDone();
        }
        else
        {
            actionBase(ActionEnum.Action.GetBottle);
            if (!actionList[0].go.GetComponent<BottlePileSpawner>())
                animations.GetBottle(actionList[0].go);
            else
                animations.GetBottle(actionList[0].go.GetComponent<BottlePileSpawner>().GetBottle()); 
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
            state = IAState.UNINTERACTEABLE;
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
        GameObject.Find("ElementManager").GetComponent<ElementManager>().AIdie();
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
        state = IAState.UNINTERACTEABLE;
        animations.Slip();
    }

    //get Stun by bottle or a kick
    protected void Stun()
    {
        actionBase(ActionEnum.Action.Stun);
        animations.Stun();
    }

    protected void BottleStrick()
    {
        actionBase(ActionEnum.Action.BottleStrick);
        animations.BottleStrick();
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
