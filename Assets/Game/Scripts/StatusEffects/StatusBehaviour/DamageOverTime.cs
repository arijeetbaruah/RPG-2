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
