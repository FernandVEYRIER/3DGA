using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a glass that can be thrown.
    /// </summary>
    public class GlassThrowable : AThrowable
    {
        [SerializeField] private GameObject _liquid;

        private readonly LiquidContainer _container = new LiquidContainer();

        private void Start()
        {
            _liquid.SetActive(false);
            _liquid.transform.localScale = new Vector3(1, 0, 1);
        }

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            var effect = _container.GetGeneratedEffect();
            if (effect != null)
                effect.Activate();
            Destroy(gameObject);
        }

        public void Fill(EffectFactory.LIQUID_TYPE liquid)
        {
            _container.Fill(liquid);
            _liquid.SetActive(true);
            _liquid.transform.localScale = new Vector3(1, _container.FillAmount / 100f, 1);
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