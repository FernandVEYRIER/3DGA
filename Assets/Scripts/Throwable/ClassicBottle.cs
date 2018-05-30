using Assets.Scripts.Effects;
using Assets.Scripts.Liquids;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a bottle that can be thrown.
    /// </summary>
    public class ClassicBottle : AThrowable
    {
        private void Start()
        {
        }

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}