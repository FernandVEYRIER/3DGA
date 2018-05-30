using Assets.Scripts.Throwable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
    public class BottleCanvas : AlcoholCanvas
    {
        [SerializeField] private BottleThrowable _bottle;
        [SerializeField] private Text _bottleNameText;

        override protected void Update()
        {
            base.Update();
            _bottleNameText.text = _bottle.GetLiquidName();
        }
    }
}