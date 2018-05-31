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
            Vector3 v = Camera.main.GetComponent<Camera>().transform.position - _canvas.transform.position;
            v.x = v.z = 0.0f;
            _canvas.transform.LookAt(Camera.main.GetComponent<Camera>().transform.position - v);
            _canvas.transform.Rotate(0, 180, 0);
        }
    }
}