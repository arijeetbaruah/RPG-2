using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class HealOverTime : BaseStatusBehaviour
    {
        [SerializeReference]
        public MathFormula.MathExpression damagePerStack;
        
        public override void Execute(StatusContext context)
        {
            float damage = damagePerStack.Evaluate(context.owner);
            context.owner.Heal(damage);
        }
    }
}
