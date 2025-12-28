using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Logarithmic : MathExpression
    {
        private const float min = 0.0001f;
        
        [SerializeReference]
        public MathExpression baseValue;
        
        [SerializeReference]
        public MathExpression powerValue;
        
        public override float Evaluate(float f, params object[] args)
        {
            var baseVal = baseValue.Evaluate(0);
            var pwrVal = powerValue.Evaluate(0);
            return Mathf.Log(Mathf.Max(pwrVal, min), baseVal);
        }
    }
    
    [System.Serializable]
    public class Clamp : MathExpression
    {
        [SerializeReference]
        public MathExpression min;

        [SerializeReference]
        public MathExpression max;

        public override float Evaluate(float f, params object[] args)
        {
            float minValue = min.Evaluate(0);
            float maxValue = max.Evaluate(0);
            return Mathf.Clamp(f, minValue, maxValue);
        }
    }
}
