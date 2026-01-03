using RPG.Core.Character;
using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class ResourceModify : BaseStatusBehaviour
    {
        public DerivedStats resourceType;
        [SerializeReference]
        public MathFormula.MathExpression amountPerStack;
        
        /// <summary>
        /// Applies the configured resource modification to the context's owner for the current status instance.
        /// </summary>
        /// <param name="context">The status context containing the target owner and the status instance whose stacks determine the total modification.</param>
        public override void Execute(StatusContext context)
        {
            if (amountPerStack == null)
            {
                return;
            }
            context.owner.UpdateStats(resourceType, amountPerStack.Evaluate(context.owner) * context.instance.stacks);
        }
    }
}