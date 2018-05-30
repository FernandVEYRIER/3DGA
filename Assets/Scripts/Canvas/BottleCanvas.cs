using Assets.Scripts.Throwable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
    public class BottleCanvas : MonoBehaviour
    {
        [SerializeField] private BottleThrowable _bottle;
        [SerializeField] private Text _bottleNameText;

        private void Update()
        {
            _bottleNameText.text = _bottle.GetLiquidName();
        }
    }
}