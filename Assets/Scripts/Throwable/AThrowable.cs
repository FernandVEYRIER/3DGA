using System.Collections;
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
                transform.position = _attachedTransform.position;
            }

            if (Input.GetKeyDown(KeyCode.N))
                Throw(Vector3.zero);
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
        /// Throws the object, with its current force.
        /// </summary>
        public virtual void Throw()
        {
            _isGrabbed = false;
            _rigidBody.AddForce(_throwForce);
            _attachedTransform = null;
        }

        /// <summary>
        /// Throws the object with a parabola trajectory,
        /// aiming to the given target.
        /// </summary>
        /// <param name="target"></param>
        public virtual void Throw(Vector3 target)
        {
            _isGrabbed = false;
            _attachedTransform = null;
            float firingAngle = 45f, gravity = Mathf.Abs(Physics.gravity.y);

            // Calculate distance to target
            float target_Distance = Vector3.Distance(transform.position, target);
            // Calculate the velocity needed to throw the object to the target at specified angle.
            float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

            // Extract the X  Y componenent of the velocity
            float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

            // Calculate flight time.
            float flightDuration = target_Distance / Vx;

            // Rotate projectile to face the target.
            transform.rotation = Quaternion.LookRotation(target - transform.position);

            var fwd = transform.forward;
            fwd.x = 0;
            fwd.y = Vy;
            fwd.z = Vx;
            fwd = transform.TransformDirection(fwd);

            _rigidBody.velocity = fwd;
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