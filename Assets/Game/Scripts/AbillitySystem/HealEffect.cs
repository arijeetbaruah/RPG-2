using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = MENU_NAME + nameof(HealEffect))]
    public class HealEffect : BaseAbilityEffect
    {
        [SerializeField, SerializeReference]
        protected MathFormula.MathExpression _damageFormula;
        
        public MathFormula.MathExpression DamageFormula => _damageFormula;
        
        /// <summary>
        /// Applies a healing effect to the context's target using the configured damage formula when the associated ability is a TargetingAbility.
        /// </summary>
        /// <param name="context">The ability context containing the user, ability, and target. If the ability is a TargetingAbility, the formula is evaluated with the user and targeting ability and the result is applied as healing to the target.</param>
        public override void Apply(AbilityContext context)
        {
            if (_damageFormula == null)
            {
                Debug.LogError($"HealEffect on {context.ability.name} has no formula configured");
                return;
            }
            
            if (context.ability is TargetingAbility targetingAbility)
            {
                float dmg = _damageFormula.Evaluate(context.user, targetingAbility);
                context.target?.Heal(dmg);
            }
            else
            {
                Debug.LogError($"{context.ability.name} is not a TargetingAbility");
            }
        }
    }
}