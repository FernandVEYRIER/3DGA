using Assets.Scripts.Throwable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
    public class GlassCanvas : AlcoholCanvas
    {
        [SerializeField] private Text _powerUpText;
        [SerializeField] private Text[] _alcoholText;
        [SerializeField] private GlassThrowable _glass;

        override protected void Update()
        {
            base.Update();
            _powerUpText.text = _glass.GetEffectName();
            var itemList = _glass.GetLiquidList();
            for (int i = 0; i < _alcoholText.Length; ++i)
            {
                if (i < itemList.Count)
                {
                    _alcoholText[i].gameObject.SetActive(true);
                    _alcoholText[i].text = itemList[i].Percentage.ToString() + " " + itemList[i].Liquid.ToString();
                }
                else
                {
                    _alcoholText[i].gameObject.SetActive(false);
                }
            }
        }
    }
}