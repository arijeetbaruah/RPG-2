using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "DamageAbilityEffect", menuName = MENU_NAME + nameof(DamageAbilityEffect))]
    public class DamageAbilityEffect : BaseAbilityEffect
    {
        [SerializeField, SerializeReference]
        protected MathFormula.MathExpression _damageFormula;
        
        [EnumToggleButtons, SerializeField]
        protected DamageType _damageType;
        
        public DamageType DamageType => _damageType;
        public MathFormula.MathExpression DamageFormula => _damageFormula;
        
        public override void Apply(AbilityContext context)
        {
            if (context.ability is TargetingAbility damageAbility)
            {
                float dmg = _damageFormula.Evaluate(context.user, damageAbility);
                context.target.TakeDamage(dmg, _damageType);
            }
            else
            {
                Debug.LogError($"{context.ability.name} is not a DamageAbility");
            }
        }
    }
}
