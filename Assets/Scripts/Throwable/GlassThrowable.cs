using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a glass that can be thrown.
    /// </summary>
    public class GlassThrowable : AThrowable
    {
        private readonly LiquidContainer _container = new LiquidContainer();

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            var effect = _container.GetGeneratedEffect();
            if (effect != null)
                effect.Activate();
            Destroy(gameObject);
        }

        private void Fill(EffectFactory.LIQUID_TYPE liquid)
        {
            _container.Fill(liquid);
        }

        override protected void Update()
        {
            base.Update();
            if (Input.GetKey(KeyCode.O))
                Fill(EffectFactory.LIQUID_TYPE.LimeJuice);
            if (Input.GetKey(KeyCode.P))
                Fill(EffectFactory.LIQUID_TYPE.Vodka);
            if (Input.GetKey(KeyCode.I))
                Fill(EffectFactory.LIQUID_TYPE.Water);
            if (Input.GetKeyDown(KeyCode.M))
            {
                var effect = _container.GetGeneratedEffect();
                if (effect != null)
                    Debug.Log("Generated effect ==> " + effect.Recipe.Name);
                else
                    Debug.Log("No effect at the moment.");
            }
        }
    }
}