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
        
        /// <summary>
        /// Computes the logarithm of the evaluated power expression using the evaluated base expression.
        /// </summary>
        /// <param name="f">Ignored; this method evaluates its own sub-expressions instead of using the input value.</param>
        /// <param name="args">Ignored.</param>
        /// <summary>
        /// Computes the logarithm of the evaluated power using the evaluated base.
        /// </summary>
        /// <returns>The logarithm (base = evaluated base) of the evaluated power after applying a lower bound of 0.0001; returns 0 if either sub-expression is null, or if the evaluated base is less than or equal to 0 or approximately equal to 1.</returns>
        public override float Evaluate(params object[] args)
        {
            if (baseValue == null || powerValue == null)
            {
                Debug.LogWarning("Logarithmic: Base or Power is null");
                return 0;
            }
            
            var baseVal = baseValue.Evaluate(args);
            var pwrVal = powerValue.Evaluate(args);
            
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
        public MathExpression value;
        
        [SerializeReference]
        public MathExpression min;

        [SerializeReference]
        public MathExpression max;

        /// <summary>
        /// Clamps the input value between the evaluated minimum and maximum expressions.
        /// </summary>
        /// <param name="f">Value to be clamped.</param>
        /// <param name="args">Additional arguments; ignored by this implementation.</param>
        /// <summary>
        /// Clamps this expression's evaluated value to the range defined by its evaluated minimum and maximum.
        /// </summary>
        /// <param name="args">Arguments forwarded to the contained sub-expressions during their evaluation.</param>
        /// <returns>The evaluated value constrained to be greater than or equal to the evaluated minimum and less than or equal to the evaluated maximum.</returns>
        public override float Evaluate(params object[] args)
        {
            if (value == null || min == null || max == null)
            {
                Debug.LogWarning("Clamp: Base or Power is null");
                return 0;
            }
            
            float minValue = min.Evaluate(args);
            float maxValue = max.Evaluate(args);
            return Mathf.Clamp(value.Evaluate(args), minValue, maxValue);
        }
    }
}