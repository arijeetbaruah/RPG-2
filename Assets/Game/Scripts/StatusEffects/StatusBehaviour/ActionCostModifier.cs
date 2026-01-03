using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class ActionCostModifier : BaseStatusBehaviour
    {
        [SerializeReference]
        public MathFormula.MathExpression extraCostPerStack;
        
        /// <summary>
        /// Executes this status behaviour for the given context; currently this implementation performs no action.
        /// </summary>
        /// <param name="context">Status execution context providing target, source, and stack information.</param>
        public override void Execute(StatusContext context)
        {
        }
    }
}