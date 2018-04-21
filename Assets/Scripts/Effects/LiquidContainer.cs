using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Provides a container for liquids, to handle filling and effect generation from content.
    /// </summary>
    public class LiquidContainer
    {
        /// <summary>
        /// True if the container is empty, false otherwise.
        /// </summary>
        public bool IsEmpty { get { return fillPercentage <= 0; } }

        private readonly List<Ingredient> _containedLiquids = new List<Ingredient>();
        private float fillPercentage;
        [SerializeField]
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
        /// Fills the container with the given liquid and the given amount.
        /// </summary>
        /// <param name="liquid"></param>
        /// <param name="amount">Should be between 0 and 100%</param>
        public void Fill(EffectFactory.LIQUID_TYPE liquid, float amount)
        {
            _containedLiquids.Add(new Ingredient { Liquid = liquid, Percentage = amount });
            fillPercentage += amount;
        }

        /// <summary>
        /// Depletes the container from its content.
        /// </summary>
        public void Deplete()
        {
            fillPercentage -= fillStep;
            if (fillPercentage <= 0)
                fillPercentage = 0;
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