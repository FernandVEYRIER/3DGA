using Assets.Scripts.Throwable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
    public class GlassCanvas : MonoBehaviour
    {
        [SerializeField] private Text _powerUpText;
        [SerializeField] private GameObject _alcoholTextPrefab;
        [SerializeField] private RectTransform _alcoholTextContainer;
        [SerializeField] private GlassThrowable _glass;

        private void Update()
        {
            _powerUpText.text = _glass.GetEffectName();
        }
    }
}