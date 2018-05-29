using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a glass that can be thrown.
    /// </summary>
    public class GlassThrowable : AThrowable
    {
        [SerializeField] private GameObject _liquid;
        private Renderer _liquidMat;
        private readonly LiquidContainer _container = new LiquidContainer();

        private void Start()
        {
            _liquid.SetActive(false);
            _liquid.transform.localScale = new Vector3(1, 0, 1);
            _liquidMat = _liquid.GetComponent<Renderer>();
            _liquidMat.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            _liquidMat.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _liquidMat.material.SetInt("_ZWrite", 0);
            _liquidMat.material.DisableKeyword("_ALPHATEST_ON");
            _liquidMat.material.DisableKeyword("_ALPHABLEND_ON");
            _liquidMat.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            _liquidMat.material.renderQueue = 3000;
            Debug.Log("liquid mat => " + _liquidMat.material.GetColor("_Color"));
        }

        protected override void OnObjectDestroy()
        {
            if (_hitEffect != null)
                Instantiate(_hitEffect, transform.position, Quaternion.identity);
            /*var effect = _container.GetGeneratedEffect();
            if (effect != null)
                effect.Activate(gameObject);*/
            Destroy(gameObject);
        }

        public void Fill(EffectFactory.LIQUID_TYPE liquid)
        {
            var fa = _container.FillAmount / 100f;
            _container.Fill(liquid);
            _liquid.SetActive(fa > 0);
            _liquid.transform.localScale = new Vector3(1, fa, 1);
            _liquidMat.material.color = _container.GetContainerColor();
            //_liquidMat.material.SetColor("_Color", _container.GetContainerColor());
        }

        override protected void Update()
        {
            base.Update();
            if (Input.GetKey(KeyCode.O))
                Fill(EffectFactory.LIQUID_TYPE.LimeJuice);
            if (Input.GetKey(KeyCode.P))
                Fill(EffectFactory.LIQUID_TYPE.Vodka);
            if (Input.GetKey(KeyCode.I))
                Fill(EffectFactory.LIQUID_TYPE.Water);
            if (Input.GetKeyDown(KeyCode.M))
            {
                var effect = _container.GetGeneratedEffect();
                if (effect != null)
                {
                    Debug.Log("Generated effect ==> " + effect.Recipe.Name);
                    effect.Activate(gameObject);
                }
                else
                    Debug.Log("No effect at the moment.");
            }
        }
    }
}