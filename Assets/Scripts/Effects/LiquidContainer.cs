using Assets.Scripts.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Provides a container for liquids, to handle filling and effect generation from content.
    /// </summary>
    public class LiquidContainer
    {
        private readonly List<Ingredient> _containedLiquids = new List<Ingredient>();
        private float fillPercentage;
        private float fillStep = 0.25f;

        /// <summary>
        /// Fills the container with a certain liquid.
        /// Water cleans all the liquids.
        /// </summary>
        /// <param name="liquid"></param>
        public void Fill(EffectFactory.LIQUID_TYPE liquid)
        {
            if (liquid == EffectFactory.LIQUID_TYPE.Water)
            {
                Empty();
                return;
            }
            var l = _containedLiquids.Find(x => x.Liquid == liquid);
            if (l == null)
            {
                l = new Ingredient { Liquid = liquid };
                _containedLiquids.Add(l);
            }
            Debug.Log("Fill percentage => " + l.Percentage);
            if (fillPercentage >= 100)
            {
                fillPercentage = 100;
                return;
            }
            l.Percentage += fillStep;
            fillPercentage += fillStep;
        }

        /// <summary>
        /// Makes the container empty by removing every liquid in it.
        /// </summary>
        private void Empty()
        {
            fillPercentage = 0;
            _containedLiquids.Clear();
        }

        /// <summary>
        /// Gets the effect generated according to the current content of the container.
        /// </summary>
        /// <returns></returns>
        public AEffect GetGeneratedEffect()
        {
            return GameManager.Instance.Factory.GetEffect(_containedLiquids);
        }
    }
}