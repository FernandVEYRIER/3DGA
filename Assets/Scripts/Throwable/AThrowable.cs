using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Abstract for object that can be thrown.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class AThrowable : MonoBehaviour
    {
        [SerializeField] protected GameObject _hitEffect;
        [SerializeField] protected List<string> _hitTags = new List<string>();

        protected Rigidbody _rigidBody;
        protected Transform _attachedTransform;

        private bool _isGrabbed;
        private Vector3 _previousPosition;
        private Vector3 _throwForce;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            if (_isGrabbed)
            {
                _throwForce = ComputeThrowingForce();
            }
        }

        /// <summary>
        /// Computes the theorical force that should be applied to
        /// the object if it was thrown.
        /// </summary>
        /// <returns></returns>
        protected Vector3 ComputeThrowingForce()
        {
            var currentForce = transform.position - _previousPosition;
            _previousPosition = transform.position;
            return currentForce;
        }

        /// <summary>
        /// Called when the object detects a collision.
        /// By default triggers OnObjectDestroy.
        /// </summary>
        /// <param name="collision"></param>
        virtual protected void OnCollisionEnter(Collision collision)
        {
            if (_hitTags.Contains(collision.transform.tag))
            {
                OnObjectDestroy();
            }
        }

        /// <summary>
        /// Called on the object destruction.
        /// </summary>
        protected abstract void OnObjectDestroy();

        /// <summary>
        /// Throws the object.
        /// </summary>
        public virtual void Throw()
        {
            _isGrabbed = false;
            _rigidBody.AddForce(_throwForce);
            _attachedTransform = null;
        }

        /// <summary>
        /// Grabs the object.
        /// </summary>
        /// <param name="parent">The object grabbing it.</param>
        public virtual void Grab(Transform parent)
        {
            _isGrabbed = true;
            _previousPosition = transform.position;
            _attachedTransform = parent;
        }
    }
}