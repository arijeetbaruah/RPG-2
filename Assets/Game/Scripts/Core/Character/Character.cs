using System.Collections.Generic;
using RPG.AbilitySystem;
using RPG.Core.Interfaces;
using RPG.StatusEffects;
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
        /// <summary>
        /// Gets the specified derived stat value for this character, including any stored adjustment.
        /// </summary>
        /// <param name="stats">The derived stat to retrieve.</param>
        /// <returns>The evaluated stat value including any stored adjustment; if no adjustment exists, returns the evaluated value.</returns>
        public float GetDerivedStat(DerivedStats stats)
        {
            _characterData.DerivedStats.TryGetValue(stats, out var value);
            return stats.Evaluate(_characterData) + value;
        }

        /// <summary>
        /// Applies the specified status effect to this character.
        /// </summary>
        /// <param name="effect">The status effect to apply to the character.</param>
        public void ApplyStatusEffects(IStatusEffect effect)
        {
            
        }

        /// <summary>
        /// Modify the specified derived stat by a given amount through the character's resource handler.
        /// </summary>
        /// <param name="stats">The derived stat to modify.</param>
        /// <param name="delta">Amount to change the stat by; positive to increase, negative to decrease.</param>
        public void UpdateStats(DerivedStats stats, float delta)
        {
            CharacterResourceHandler.UpdateStats(stats, delta);
        }

        /// <summary>
        /// Reduces the character's HP by the specified damage amount modified by vulnerabilities and resistances.
        /// </summary>
        /// <param name="dmg">Base damage to apply before modifiers.</param>
        /// <param name="damageType">Type of the incoming damage; if <see cref="DamageType.None"/>, the method returns without applying damage.</param>
        /// <remarks>
        /// If the character has the specified damage type in its Vulnerability flags, damage is doubled and <see cref="OnVulnerabilityTriggered"/> is invoked.
        /// If the character has the specified damage type in its Resistance flags, damage is halved and <see cref="OnResistanceTriggered"/> is invoked.
        /// Both modifiers can apply and combine multiplicatively. Final damage is applied via the character's <see cref="CharacterResourceHandler"/>.
        /// </remarks>
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
        
        /// <summary>
        /// Restores the character's hit points by the specified amount.
        /// </summary>
        /// <param name="dmg">Amount of hit points to restore; positive values increase HP.</param>
        public void Heal(float dmg)
        {
            CharacterResourceHandler.UpdateHP(dmg);
        }

        /// <summary>
        /// Determine whether the specified ability is unlocked for this character.
        /// </summary>
        /// <param name="ability">The ability to check.</param>
        /// <returns>`true` if the ability's requirements are satisfied or if the ability has no requirements; `false` if the ability is null or its requirements are not satisfied.</returns>
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