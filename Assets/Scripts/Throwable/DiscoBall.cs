using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    public class DiscoBall : AThrowable
    {
        [SerializeField] Light[] spotLight;

        [SerializeField] float heightToReach;
        [SerializeField] Vector3 scaleToReach;

        [SerializeField] float scalingSpeed, movingSpeed, rotationSpeed;
        [SerializeField] float lifeTimer;

        [SerializeField] SphereCollider triggerSphere;
        [SerializeField] float radius;

        private Rigidbody rigidbody;
        private Quaternion originRotation;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected override void OnObjectDestroy()
        {
            triggerSphere.radius = 1;
            Destroy(gameObject, 0.1f);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (HasBeenThrown)
                LetsDisco();
        }

        private void LetsDisco()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            transform.rotation = originRotation;
            rigidbody.isKinematic = true;
            Invoke("OnObjectDestroy", lifeTimer);
            StartCoroutine(MovingAnimation());
        }

        private IEnumerator MovingAnimation()
        {
            Vector3 beginningPosition = transform.position;
            Vector3 destination = new Vector3(transform.position.x, heightToReach, transform.position.z);
            float distance = Vector3.Distance(transform.position, new Vector3(transform.position.x, heightToReach, transform.position.z));
            float t = 0;
            bool scaling = false;
            while (gameObject.transform.position != destination)
            {
                t += movingSpeed * Time.deltaTime / distance;
                t = t > 1 ? 1 : t;
                Vector3 tmp = Vector3.Lerp(beginningPosition, destination, t);
                transform.position = tmp;
                if (scaling == false && t > 0.2)
                {
                    StartCoroutine(ScalingAnimation());
                    scaling = true;
                }
                yield return new WaitForFixedUpdate();
            }
            yield return 0;
        }

        private IEnumerator ScalingAnimation()
        {
            Vector3 beginningPosition = transform.localScale;
            float distance = scaleToReach.x - beginningPosition.x;
            float t = 0;
            while (gameObject.transform.localScale != scaleToReach)
            {
                t += scalingSpeed * Time.deltaTime / distance;
                t = t > 1 ? 1 : t;
                Vector3 tmp = Vector3.Lerp(beginningPosition, scaleToReach, t);
                transform.localScale = tmp;
                yield return new WaitForFixedUpdate();
            }
            foreach (var light in spotLight)
            {
                light.enabled = true;
            }
            triggerSphere.radius = radius;
            StartCoroutine(RotationAnimation());
            yield return 0;
        }

        private IEnumerator RotationAnimation()
        {
            while(true)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
