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
                float divisor = valueExpression.Evaluate(0);
                if (Mathf.Approximately(divisor, 0f))
                {
                    Debug.LogError("Division by zero attempted in Divide expression");
                    return 0;
                }
                
                value /= divisor;
            }
            
            return value;
        }
    }
}
