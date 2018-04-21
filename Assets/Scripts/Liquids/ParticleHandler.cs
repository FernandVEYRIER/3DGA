using System;
using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Liquids
{
    public delegate void EventParticle(object sender, GameObject obj);

    /// <summary>
    /// Handles the particle behaviour to simulate liquids.
    /// </summary>
    public class ParticleHandler : MonoBehaviour
    {
        [Tooltip("The transform to watch for rotations")]
        [SerializeField] private Transform relativeTransform;

        private ParticleSystem _particles;
        [SerializeField]
        private float maxEmission = 40;

        private void Awake()
        {
            _particles = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Invoked when particles collide with something.
        /// </summary>
        public event EventParticle OnParticleCollided;

        private void OnParticleCollision(GameObject other)
        {
            if (OnParticleCollided != null)
                OnParticleCollided.Invoke(this, other);
        }

        private void Update()
        {
            var emi = _particles.emission;
            float amount = ComputeParticleEmission();
            emi.rateOverTime = new ParticleSystem.MinMaxCurve(amount);
        }

        /// <summary>
        /// Computes the particle count according to the current angle of the Relative Transform set.
        /// </summary>
        /// <returns></returns>
        private float ComputeParticleEmission()
        {
            var angle = Vector3.Dot(transform.forward, Vector3.down);
            return angle * maxEmission;
        }
    }
}