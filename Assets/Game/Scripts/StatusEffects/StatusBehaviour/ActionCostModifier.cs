using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class ActionCostModifier : BaseStatusBehaviour
    {
        [SerializeReference]
        public MathFormula.MathExpression extraCostPerStack;
        
        public override void Execute(StatusContext context)
        {
        }
    }
}
