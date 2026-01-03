using RPG.AbilitySystem;
using RPG.Core.Interfaces;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class BaseAbilityEffect : ScriptableObject
    {
        public const string MENU_NAME = "Game/Ability System/Ability Effects/";

        public abstract void Apply(AbilityContext context);
    }
    
    public class AbilityContext
    {
        public BaseAbility ability;
        
        public ICharacter user;
        public ICharacter target;
    }
}
