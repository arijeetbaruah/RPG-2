using System.Collections.Generic;
using RPG.Abilities;
using RPG.Core.Character;
using UnityEngine;
using UnityEngine.Localization;

namespace RPG.AbilitySystem
{
    //[CreateAssetMenu(fileName = "BaseAbility", menuName = "Game/BaseAbility")]
    public abstract class BaseAbility : ScriptableObject
    {
        public const string MENU_NAME = "Game/Ability System/Ability/";
        
        [SerializeField] private string abilityId;
        [SerializeField] private LocalizedString abilityName;
        [SerializeField] private LocalizedString abilityDescription;
        
        [Header("Mastery")]
        [SerializeField] private List<int> masteryThresholds = new List<int>() { 0 };
        
        [Header("Cost")]
        [SerializeField] private DerivedStats statUsed;
        [SerializeField] private int cost;
        
        [SerializeField] private float cooldown;
        
        [SerializeField] private RequirementSystem.Requirements requirements;
        [SerializeField] private List<BaseAbilityEffect> _abilityEffects = new ();
        
        public string AbilityId => abilityId;
        public string AbilityName => abilityName?.GetLocalizedString() ?? string.Empty;
        public LocalizedString AbilityDescription => abilityDescription;
        public IReadOnlyList<int> MasteryThresholds => masteryThresholds; 
        public IReadOnlyList<BaseAbilityEffect> AbilityEffects => _abilityEffects;
        public DerivedStats StatUsed => statUsed;
        public int Cost => cost;
        public float Cooldown => cooldown;
        public RequirementSystem.Requirements Requirements => requirements;
    }
}
