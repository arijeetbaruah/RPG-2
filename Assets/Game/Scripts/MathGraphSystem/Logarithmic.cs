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
            if (baseValue == null || powerValue == null)
            {
                Debug.LogWarning("Logarithmic: Base or Power is null");
                return 0;
            }
            
            var baseVal = baseValue.Evaluate(0, args);
            var pwrVal = powerValue.Evaluate(0, args);
            
            // Validate base: must be > 0 and != 1
            if (baseVal <= 0f || Mathf.Approximately(baseVal, 1f))
                return 0f;
            
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
            float minValue = min.Evaluate(0, args);
            float maxValue = max.Evaluate(0, args);
            return Mathf.Clamp(f, minValue, maxValue);
        }
    }
}
