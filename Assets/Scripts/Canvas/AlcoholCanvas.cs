using System;
using UnityEngine;

namespace Assets.Scripts.Canvas
{
    public abstract class AlcoholCanvas : MonoBehaviour
    {
        [SerializeField] protected GameObject _canvas;
        protected Camera _camera;
        [SerializeField] protected AnimationCurve _fadeCurve;
        [SerializeField] protected float DistanceForDisplay;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _camera = Camera.main.GetComponent<Camera>();
            _canvasGroup = _canvas.GetComponent<CanvasGroup>();
        }

        virtual protected void Update()
        {
            FaceCamera();
            UpdateState();
        }

        private void UpdateState()
        {
            var distance = Vector3.Distance(_canvas.transform.position, _camera.transform.position);
            if (distance > DistanceForDisplay)
            {
                _canvas.SetActive(false);
                return;
            }
            _canvas.SetActive(true);
            var posInScreen = _camera.WorldToViewportPoint(_canvas.transform.position);
            posInScreen.z = 0;
            //Debug.Log("Position in screen => " + posInScreen);
            //Debug.Log("Distance => " + _fadeCurve.Evaluate(Vector3.Distance(posInScreen, new Vector3(0.5f, 0.5f, 0))));
            _canvasGroup.alpha = _fadeCurve.Evaluate(Vector3.Distance(posInScreen, new Vector3(0.5f, 0.5f, 0)));
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