using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class HealOverTime : BaseStatusBehaviour
    {
        [SerializeReference]
        public MathFormula.MathExpression damagePerStack;
        
        /// <summary>
        /// Applies healing to the status effect's owner using the configured per-stack expression.
        /// </summary>
        /// <param name="context">Status execution context containing the owner who will receive the healing.</param>
        public override void Execute(StatusContext context)
        {
            float damage = damagePerStack.Evaluate(context.owner);
            context.owner.Heal(damage);
        }
    }
}