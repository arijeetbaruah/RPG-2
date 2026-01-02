using System.Collections.Generic;
using RPG.Abilities;
using RPG.AbilitySystem;
using UnityEngine;

namespace RPG.Core.Character
{
    
    [RequireComponent(typeof(CharacterResourceHandler))]
    public class Character : MonoBehaviour
    {
        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;
        private List<AbilityMastery> _abilities;
        
        public CharacterResourceHandler CharacterResourceHandler { get; private set; }
        public IReadOnlyList<AbilityMastery> Abilities => _abilities;

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

        public void TakeDamage(float dmg, DamageType damageType)
        {
            if (damageType == DamageType.None)
            {
                Debug.LogWarning("DamageType is None.");
                return;
            }
            
            float dmgMul = 1;
            
            if (CharacterData.Vurnability.HasFlag(damageType))
            {
                dmgMul *= 2;
            }
            
            if (CharacterData.Resistance.HasFlag(damageType))
            {
                dmgMul /= 2;
            }
            
            CharacterResourceHandler.UpdateHP(dmg, dmgMul);
        }

        public bool IsAbilityUnlocked(BaseAbility ability)
        {
            return ability.Requirements.EvaluateRequirements(this, ability);
        }

        [System.Serializable]
        public struct AbilityMastery
        {
            public BaseAbility Ability;
            public int MasteryLevel;
        }
    }
}