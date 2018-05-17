using Assets.Scripts.Game;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Extensions;

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
        public bool IsEmpty { get { return FillAmount <= 0; } }

        /// <summary>
        /// The current filling amount of the container, from 0 to 100.
        /// </summary>
        public float FillAmount { get; private set; }


        private readonly List<Ingredient> _containedLiquids = new List<Ingredient>();

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
            if (FillAmount >= 100)
            {
                FillAmount = 100;
                return;
            }
            l.Percentage += fillStep;
            FillAmount += fillStep;
            Debug.Log("Fill percent of " + liquid + " = " + l.Percentage);
        }

        /// <summary>
        /// Returns the global color of the container according to the current contained liquids.
        /// </summary>
        /// <returns></returns>
        public Color GetContainerColor()
        {
            var col = Color.clear;
            var colList = new List<Color>();
            foreach (var liquid in _containedLiquids)
            {
                colList.Add(GameManager.Instance.Factory.GetLiquidColor(liquid.Liquid));
            }
            return Extension.CombineColors(colList);
        }

        /// <summary>
        /// Fills the container with the given liquid and the given amount.
        /// </summary>
        /// <param name="liquid"></param>
        /// <param name="amount">Should be between 0 and 100%</param>
        public void Fill(EffectFactory.LIQUID_TYPE liquid, float amount)
        {
            _containedLiquids.Add(new Ingredient { Liquid = liquid, Percentage = amount });
            FillAmount += amount;
        }

        /// <summary>
        /// Depletes the container from its content.
        /// </summary>
        public void Deplete()
        {
            FillAmount -= fillStep;
            if (FillAmount <= 0)
                FillAmount = 0;
        }

        /// <summary>
        /// Makes the container empty by removing every liquid in it.
        /// </summary>
        private void Empty()
        {
            FillAmount = 0;
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