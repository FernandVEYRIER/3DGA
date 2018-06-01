using Assets.Scripts.Throwable;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas
{
    [System.Serializable]
    public class AlcoholInfo
    {
        public GameObject RootObj;
        public Text TextAlcohol;
        public Text TextAlcoholPercentage;
    }

    public class GlassCanvas : AlcoholCanvas
    {
        [SerializeField] private Text _powerUpText;
        [SerializeField] private AlcoholInfo[] _alcoholObj;
        [SerializeField] private GlassThrowable _glass;

        override protected void Update()
        {
            base.Update();
            if (_glass.IsGrabbed || _glass.HasBeenThrown)
            {
                _canvas.SetActive(false);
                return;
            }
            _powerUpText.text = _glass.GetEffectName();
            var itemList = _glass.GetLiquidList();
            for (int i = 0; i < _alcoholObj.Length; ++i)
            {
                if (i < itemList.Count)
                {
                    _alcoholObj[i].RootObj.SetActive(true);
                    _alcoholObj[i].TextAlcohol.text = itemList[i].Liquid.ToString();
                    _alcoholObj[i].TextAlcoholPercentage.text = itemList[i].Percentage.ToString("0.") + "%";
                }
                else
                {
                    _alcoholObj[i].RootObj.SetActive(false);
                }
            }
        }
    }
}