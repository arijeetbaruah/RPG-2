using System;
using RPG.AbilitySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.Abilities
{
    
    [CreateAssetMenu(fileName = "new TargetingAbility", menuName = MENU_NAME + nameof(TargetingAbility))]
    public class TargetingAbility : BaseAbility
    {
        [EnumToggleButtons, OnValueChanged(nameof(OnValueChanged))]
        [SerializeField] private AbilityTargetGroup _targetGroup;
        [EnumToggleButtons, OnValueChanged(nameof(OnValueChanged))]
        [SerializeField] private AbilityTargetScope _targetScope;
        
        public AbilityTargetGroup TargetGroup => _targetGroup;
        public AbilityTargetScope TargetScope => _targetScope;
        
        public enum AbilityTargetGroup: byte
        {
            Self,
            Enemy,
            Ally
        }
        
        public enum AbilityTargetScope : byte
        {
            Single,
            All
        }

        private void OnValueChanged()
        {
            if (_targetGroup == AbilityTargetGroup.Self && _targetScope != AbilityTargetScope.Single)
            {
                _targetScope = AbilityTargetScope.Single;
            }
        }
    }
}
