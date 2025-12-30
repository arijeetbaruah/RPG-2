using UnityEngine;

namespace RPG.Core.Character
{
    [RequireComponent(typeof(CharacterResourceHandler))]
    public class Character : MonoBehaviour
    {
        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;
        
        public CharacterResourceHandler CharacterResourceHandler { get; private set; }

        /// <summary>
        /// Initializes the CharacterResourceHandler property by retrieving the CharacterResourceHandler component from the same GameObject.
        /// </summary>
        private void Awake()
        {
            CharacterResourceHandler = GetComponent<CharacterResourceHandler>();
        }

        /// <summary>
        /// Computes the specified derived stat for this character.
        /// </summary>
        /// <param name="stats">Evaluator that computes a derived stat using this character's CharacterData.</param>
        /// <summary>
        /// Computes the value of the specified derived stat for this character.
        /// </summary>
        /// <param name="stats">The derived stat to compute.</param>
        /// <returns>The evaluated stat value plus any stored adjustment for that stat; if no stored adjustment exists, the evaluated value is returned.</returns>
        public float GetDerivedStat(DerivedStats stats)
        {
            _characterData.DerivedStats.TryGetValue(stats, out var value);
            return stats.Evaluate(_characterData) + value;
        }
    }
}