using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a bottle that can be thrown.
    /// </summary>
    public class BottleThrowable : AThrowable
    {
        [SerializeField] private Liquid _liquid;

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}