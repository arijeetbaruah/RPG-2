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
        
        /// <summary>
        /// Computes the value of the base expression evaluated at <paramref name="f"/> raised to the power of the exponent expression evaluated at 0.
        /// </summary>
        /// <param name="f">Input passed to evaluate the base expression.</param>
        /// <param name="args">Additional arguments (not used).</param>
        /// <returns>The result of raising the evaluated base to the evaluated exponent.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float b = baseValue.Evaluate(f, args);
            float e = exponent.Evaluate(0, args);
            return Mathf.Pow(b, e);
        }
    }
    
    [System.Serializable]
    public class Exponential : MathExpression
    {
        [SerializeReference]
        public MathExpression power;
        
        /// <summary>
        /// Computes e raised to the value of the contained power expression evaluated at the given input.
        /// </summary>
        /// <param name="f">Input value passed to the inner power expression for evaluation.</param>
        /// <returns>Euler's number e raised to the inner power expression's evaluated value.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float finalPower = power.Evaluate(f, args);
            
            return Mathf.Exp(finalPower);
        }
    }
}