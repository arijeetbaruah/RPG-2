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
