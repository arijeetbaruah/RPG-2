using RPG.Core.Interfaces;
using UnityEngine;

namespace RPG.StatusEffects
{
    [System.Serializable]
    public class DamageOverTime : BaseStatusBehaviour
    {
        [SerializeReference]
        public MathFormula.MathExpression damagePerStack;
        public DamageType damageType;
        
        /// <summary>
        /// Applies damage to the status effect's owner using the configured per-stack expression multiplied by the current stack count.
        /// </summary>
        /// <param name="context">Execution context containing the affected owner and the status instance (whose stack count is used to scale damage).</param>
        public override void Execute(StatusContext context)
        {
            if (damagePerStack == null)
            {
                Debug.LogError("DamageOverTime: damagePerStack is null");
                return;
            }
            
            float damage = damagePerStack.Evaluate(context.owner) * context.instance.stacks;
            context.owner.TakeDamage(damage, damageType);
        }
    }
}