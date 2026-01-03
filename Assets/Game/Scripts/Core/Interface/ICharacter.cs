using RPG.Core.Character;
using UnityEngine;

namespace RPG.Core.Interfaces
{
    [System.Flags]
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

    public interface IStatusEffect
    {
    }
    
    public interface ICharacter
    {
        void TakeDamage(float dmg, DamageType damageType);
        void Heal(float dmg);
        void UpdateStats(DerivedStats stats, float delta);
        void ApplyStatusEffects(IStatusEffect statusEffects);
        void AddAdditionalCostThisTurn(float additionalAPCost);
    }
}