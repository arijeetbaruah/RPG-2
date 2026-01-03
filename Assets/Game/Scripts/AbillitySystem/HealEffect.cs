using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "HealEffect", menuName = MENU_NAME + nameof(HealEffect))]
    public class HealEffect : BaseAbilityEffect
    {
        [SerializeField, SerializeReference]
        protected MathFormula.MathExpression _damageFormula;
        
        public MathFormula.MathExpression DamageFormula => _damageFormula;
        
        public override void Apply(AbilityContext context)
        {
            if (context.ability is TargetingAbility targetingAbility)
            {
                float dmg = _damageFormula.Evaluate(context.user, targetingAbility);
                context.target.Heal(dmg);
            }
            else
            {
                Debug.LogError($"{context.ability.name} is not a TargetingAbility");
            }
        }
    }
}
