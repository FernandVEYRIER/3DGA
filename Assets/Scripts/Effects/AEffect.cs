using System;
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

        /// <summary>
        /// The recipe to make this effect.
        /// </summary>
        public Recipe Recipe { get { return _recipe; } }

        /// <summary>
        /// Activate the effect capacities.
        /// </summary>
        public virtual void Activate(AEvent ev) { }
    }
}