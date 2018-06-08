using System;
using Assets.Scripts.Effects;
using Assets.Scripts.Liquids;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    public class FillingBottle : AThrowable
    {

        [SerializeField] private EffectFactory.LIQUID_TYPE _liquid;
        [SerializeField] private ParticleHandler _particles;

        private readonly LiquidContainer _container = new LiquidContainer();

        private ParticleHandler _particleHandler;

        private Vector3 originPosition;
        private Quaternion originRotation;

        private void Start()
        {
            originPosition = gameObject.transform.position;
            originRotation = gameObject.transform.rotation;
            _container.Fill(_liquid, 100f);
            _particleHandler = GetComponentInChildren<ParticleHandler>();
            _particleHandler.OnParticleCollided += ParticleHandler_OnParticleCollided;
        }

        private void ParticleHandler_OnParticleCollided(object sender, GameObject obj)
        {
            var s = obj.GetComponent<GlassThrowable>();


            if (s != null && !_container.IsEmpty)
            {
                s.Fill(_liquid);
                _container.Deplete();
                //if (_container.IsEmpty)
                //_particles.Stop();
            }
        }

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            transform.position = originPosition;
            transform.rotation = originRotation;
        }

        public string GetLiquidName()
        {
            return _container.GetLiquids()[0].Liquid.ToString();
        }
    }
}
