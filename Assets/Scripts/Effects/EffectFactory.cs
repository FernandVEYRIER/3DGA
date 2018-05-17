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

        [Tooltip("The corresponding color for every liquid. Should be the same size as the LIQUID_TYPE enum.")]
        [SerializeField] private Color[] liquidColors;

        [SerializeField]
        private List<AEffect> _effectPool = new List<AEffect>();

        /// <summary>
        /// Retrives the corresponding effect from a list of liquids.
        /// </summary>
        /// <param name="containedLiquids"></param>
        /// <returns></returns>
        public AEffect GetEffect(List<Ingredient> containedLiquids)
        {
            return _effectPool.Find(x => x.Recipe.Matches(containedLiquids));
        }

        public Color GetLiquidColor(LIQUID_TYPE liquid)
        {
            if (liquidColors.Length > (int)liquid)
            {
                return liquidColors[(int)liquid];
            }
            return Color.magenta;
        }
    }
}