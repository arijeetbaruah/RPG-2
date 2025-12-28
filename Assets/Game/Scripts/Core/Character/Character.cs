using UnityEngine;

namespace RPG.Core.Character
{
    public class Character : MonoBehaviour
    {
        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;

        /// <summary>
        /// Computes the specified derived stat for this character.
        /// </summary>
        /// <param name="stats">Evaluator that computes a derived stat using this character's CharacterData.</param>
        /// <returns>The computed derived stat value as a float.</returns>
        public float GetDerivedStat(DerivedStats stats)
        {
            return stats.Evaluate(_characterData);
        }
    }
}