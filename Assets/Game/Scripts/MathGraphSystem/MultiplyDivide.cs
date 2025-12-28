using System.Linq;
using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Multiply : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value *= valueExpression.Evaluate(0, args);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Divide : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value /= valueExpression.Evaluate(0, args);
            }
            
            return value;
        }
    }
}
