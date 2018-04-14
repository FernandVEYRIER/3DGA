using Assets.Scripts.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class LiquidContainer : MonoBehaviour
    {
        private List<Liquid> _containedLiquids = new List<Liquid>();

        public void Fill(Liquid liquid)
        {

        }

        public AEffect GetGeneratedEffect()
        {
            return GameManager.Instance.Factory.GetEffect(_containedLiquids);
        }
    }
}