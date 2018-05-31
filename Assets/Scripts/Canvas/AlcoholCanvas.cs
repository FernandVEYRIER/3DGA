using UnityEngine;

namespace Assets.Scripts.Canvas
{
    public abstract class AlcoholCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;

        virtual protected void Update()
        {
            FaceCamera();
        }

        private void FaceCamera()
        {
            Vector3 v = Camera.main.GetComponent<Camera>().transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(Camera.main.GetComponent<Camera>().transform.position - v);
            transform.Rotate(0, 180, 0);
        }
    }
}