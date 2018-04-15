using Assets.Scripts.Game;
using System.Collections.Generic;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Provides a container for liquids, to handle filling and effect generation from content.
    /// </summary>
    public class LiquidContainer
    {
        private readonly List<EffectFactory.LIQUID_TYPE> _containedLiquids = new List<EffectFactory.LIQUID_TYPE>();

        /// <summary>
        /// Fills the container with a certain liquid.
        /// </summary>
        /// <param name="liquid"></param>
        public void Fill(EffectFactory.LIQUID_TYPE liquid)
        {
            if (!_containedLiquids.Contains(liquid))
                _containedLiquids.Add(liquid);
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