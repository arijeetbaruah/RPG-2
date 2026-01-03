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
        /// <summary>
/// Applies the specified amount of damage to the character, categorized by the given damage type.
/// </summary>
/// <param name="dmg">Amount of health to subtract from the character.</param>
/// <param name="damageType">Flag indicating the category or categories of damage being applied.</param>
void TakeDamage(float dmg, DamageType damageType);
        /// <summary>
/// Restores the character's hit points by the specified amount.
/// </summary>
/// <param name="dmg">The amount of hit points to restore.</param>
void Heal(float dmg);
        /// <summary>
/// Update the character's derived statistics using the provided stats and elapsed time.
/// </summary>
/// <param name="stats">DerivedStats values to apply to the character (e.g., recalculated attributes, resistances, or modifiers).</param>
/// <param name="delta">Elapsed time in seconds since the last update, used to advance time-dependent effects or timers.</param>
void UpdateStats(DerivedStats stats, float delta);
        /// <summary>
/// Applies the given status effect to this character.
/// </summary>
/// <param name="statusEffects">The status effect to apply to the character.</param>
void ApplyStatusEffects(IStatusEffect statusEffects);
    }
}