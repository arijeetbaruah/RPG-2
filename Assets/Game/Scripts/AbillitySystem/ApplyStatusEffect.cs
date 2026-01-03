using RPG.StatusEffects;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "ApplyStatusEffect", menuName = MENU_NAME + nameof(ApplyStatusEffect))]
    public class ApplyStatusEffect : BaseAbilityEffect
    {
        [SerializeField]
        private StatusEffect[] statusEffects;
        
        /// <summary>
        /// Applies the configured status effects to the ability context's target.
        /// </summary>
        /// <param name="context">The ability context whose target will receive the status effects.</param>
        /// <remarks>If the internal statusEffects array is null or empty, no action is performed. Null entries inside the array are skipped.</remarks>
        public override void Apply(AbilityContext context)
        {
            if (statusEffects == null || statusEffects.Length == 0 || context.target == null)
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