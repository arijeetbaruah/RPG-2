using RPG.AbilitySystem;
using RPG.Core.Interfaces;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class BaseAbilityEffect : ScriptableObject
    {
        public const string MENU_NAME = "Game/Ability System/Ability Effects/";

        /// <summary>
/// Applies this ability effect using the provided context.
/// </summary>
/// <param name="context">Context containing the ability, the character using it, and the target to which the effect will be applied.</param>
public abstract void Apply(AbilityContext context);
    }
    
    public class AbilityContext
    {
        public BaseAbility ability;
        
        public ICharacter user;
        public ICharacter target;
    }
}