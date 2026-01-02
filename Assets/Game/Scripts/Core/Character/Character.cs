using System.Collections.Generic;
using RPG.Abilities;
using RPG.AbilitySystem;
using UnityEngine;

namespace RPG.Core.Character
{
    
    [RequireComponent(typeof(CharacterResourceHandler))]
    public class Character : MonoBehaviour, ICharacter
    {
        public static event System.Action<Character> OnResistanceTriggered = delegate { }; 
        public static event System.Action<Character> OnVulnerabilityTriggered = delegate { }; 
        
        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;
        private Dictionary<BaseAbility, int> _abilities = new();
        
        public CharacterResourceHandler CharacterResourceHandler { get; private set; }
        public IReadOnlyDictionary<BaseAbility, int> Abilities => _abilities;

        /// <summary>
        /// Initializes the CharacterResourceHandler property by retrieving the CharacterResourceHandler component from the same GameObject.
        /// </summary>
        private void Awake()
        {
            CharacterResourceHandler = GetComponent<CharacterResourceHandler>();
        }

        public void AddAbility(BaseAbility ability)
        {
            if (!_abilities.TryAdd(ability, 0))
            {
                Debug.LogWarning($"Ability {ability.name} already exists");
            }
        }

        public void AbilityLevelUp(BaseAbility ability)
        {
            if (!_abilities.ContainsKey(ability))
            {
                Debug.LogError($"Ability {ability.name} does not exist");
                return;
            }
            
            _abilities[ability]++;
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
            
            if (CharacterData.Vulnerability.HasFlag(damageType))
            {
                dmgMul *= 2;
                OnVulnerabilityTriggered.Invoke(this);
            }
            
            if (CharacterData.Resistance.HasFlag(damageType))
            {
                dmgMul /= 2;
                OnResistanceTriggered.Invoke(this);
            }
            
            CharacterResourceHandler.UpdateHP(dmg * dmgMul);
        }

        public bool IsAbilityUnlocked(BaseAbility ability)
        {
            if (ability == null)
            {
                Debug.LogError($"Ability is null");
                return false;
            }

            if (ability.Requirements == null)
            {
                Debug.LogError($"Ability {ability.name} does not have requirements");
                return true;
            }
            
            return ability.Requirements.EvaluateRequirements(this, ability);
        }
    }
}