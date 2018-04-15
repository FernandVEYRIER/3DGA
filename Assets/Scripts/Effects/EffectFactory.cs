using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Factory in charge of creating effects.
    /// </summary>
    [Serializable]
    public class EffectFactory
    {
        /// <summary>
        /// The different kinds of liquids usable.
        /// </summary>
        public enum LIQUID_TYPE { Water, Coke, Rum, Bacardi, Vodka, LimeJuice }

        [SerializeField] private List<AEffect> _effectPool = new List<AEffect>();

        /// <summary>
        /// Retrives the corresponding effect from a list of liquids.
        /// </summary>
        /// <param name="containedLiquids"></param>
        /// <returns></returns>
        public AEffect GetEffect(List<LIQUID_TYPE> containedLiquids)
        {
            return _effectPool.Find(x => x.Recipe.Matches(containedLiquids));
        }
    }
}