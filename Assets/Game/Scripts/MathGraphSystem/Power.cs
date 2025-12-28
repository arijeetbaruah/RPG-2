using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Power : MathExpression
    {
        [SerializeReference]
        public MathExpression baseValue;

        [SerializeReference]
        public MathExpression exponent;
        
        public override float Evaluate(float f, params object[] args)
        {
            float b = baseValue.Evaluate(f);
            float e = exponent.Evaluate(0);
            return Mathf.Pow(b, e);
        }
    }
    
    [System.Serializable]
    public class Exponential : MathExpression
    {
        [SerializeReference]
        public MathExpression power;
        
        public override float Evaluate(float f, params object[] args)
        {
            float finalPower = power.Evaluate(f);
            
            return Mathf.Exp(finalPower);
        }
    }
}
