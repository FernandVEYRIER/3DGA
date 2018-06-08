using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Abstract class representing effects. (Not marked abstract to allow unity to
    /// serialize it).
    /// </summary>
    [Serializable]
    public class AEffect : MonoBehaviour
    {
        [Tooltip("The recipe to create this effect")]
        [SerializeField] private Recipe _recipe;
        [SerializeField] private AEvent _event;
        [SerializeField] private List<ActionEnum.Action> actions;
        [SerializeField] private List<GameObject> goOfActions;

        /// <summary>
        /// The recipe to make this effect.
        /// </summary>
        public Recipe Recipe { get { return _recipe; } }

        /// <summary>
        /// Activate the effect capacities.
        /// </summary>
        public virtual AEvent Activate(GameObject go)
        {
            go.AddComponent(_event.GetType());
            AEvent tmp = go.GetComponent<AEvent>();

            if (tmp == null)
                return null;
            tmp.Setup(_event, _recipe);
            if (actions != null && actions.Count > 0)
            {
                tmp.ResetActions();
                for (int i = 0; i < actions.Count; i++)
                {
                    tmp.AddAction(actions[i], goOfActions[i]);
                }
            }
            return tmp;
        }
    }
}