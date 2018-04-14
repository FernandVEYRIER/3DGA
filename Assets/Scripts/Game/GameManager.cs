using Assets.Scripts.Effects;
using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Manager for the game instance.
    /// </summary>
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private EffectFactory _factory = new EffectFactory();

        /// <summary>
        /// Static instance of the game manager.
        /// </summary>
        public static GameManager Instance { get; private set; }

        public EffectFactory Factory { get { return _factory; } }

        private void Awake()
        {
            Instance = this;
        }
    }
}