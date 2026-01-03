using RPG.StatusEffects;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = MENU_NAME + nameof(ApplyStatusEffect))]
    public class ApplyStatusEffect : BaseAbilityEffect
    {
        [SerializeField]
        private StatusEffect[] statusEffects;
        
        public override void Apply(AbilityContext context)
        {
            if (statusEffects == null || statusEffects.Length == 0)
            {
                return;
            }
            
            foreach (var statusEffect in statusEffects)
            {
                if (statusEffect == null)
                {
                    continue;
                }
                
                context.target.ApplyStatusEffects(statusEffect);
            }
        }
    }
}
