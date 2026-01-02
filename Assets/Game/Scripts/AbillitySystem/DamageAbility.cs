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
    
    public class DamageAbility : BaseAbility
    {
        [SerializeField] protected MathFormula.MathFormula _damageFormula;
        
        [EnumToggleButtons, SerializeField]
        protected DamageType _damageType;
        
        public DamageType DamageType => _damageType;
        public MathFormula.MathFormula DamageFormula => _damageFormula;
    }
}
