using System;
using RPG.AbilitySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.Abilities
{
    [Flags]
    public enum DamageType : ushort
    {
        None        = 0,
        Slashing    = 1 << 0,
        Piercing    = 1 << 1,
        Bludgeoning = 1 << 2,
        Fire        = 1 << 3,
        Cold        = 1 << 4,
        Thunder     = 1 << 5,
        Lightning   = 1 << 6,
        Radiant     = 1 << 7,
        Necrotic    = 1 << 8
    }
    
    [CreateAssetMenu(fileName = "BaseAbility", menuName = MENU_NAME + nameof(TargetingAbility))]
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
