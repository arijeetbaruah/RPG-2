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
        /// <returns>The logarithm of max(evaluated power, 0.0001) using the evaluated base.</returns>
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

        /// <summary>
        /// Clamps the input value between the evaluated minimum and maximum expressions.
        /// </summary>
        /// <param name="f">Value to be clamped.</param>
        /// <param name="args">Additional arguments; ignored by this implementation.</param>
        /// <returns>The input value constrained to be greater than or equal to the evaluated minimum and less than or equal to the evaluated maximum.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float minValue = min.Evaluate(0);
            float maxValue = max.Evaluate(0);
            return Mathf.Clamp(f, minValue, maxValue);
        }
    }
}