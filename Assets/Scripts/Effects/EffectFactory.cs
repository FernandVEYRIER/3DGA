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
        [SerializeField] private List<AEffect> _effectPool = new List<AEffect>();

        /// <summary>
        /// Retrives the corresponding effect from a list of liquids.
        /// </summary>
        /// <param name="containedLiquids"></param>
        /// <returns></returns>
        public AEffect GetEffect(List<Liquid> containedLiquids)
        {
            throw new NotImplementedException();
        }
    }
}