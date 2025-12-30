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
        /// <summary>
        /// Computes the base expression raised to the exponent expression using the provided evaluation arguments.
        /// </summary>
        /// <param name="args">Arguments forwarded to both the base and exponent expressions when they are evaluated.</param>
        /// <returns>The value of the base expression raised to the power of the exponent expression.</returns>
        public override float Evaluate(params object[] args)
        {
            float b = baseValue.Evaluate(args);
            float e = exponent.Evaluate(args);
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
        /// <summary>
        /// Computes e raised to the value of the inner power expression.
        /// </summary>
        /// <param name="args">Arguments forwarded to the inner power expression's Evaluate method.</param>
        /// <returns>The value of e raised to the inner expression's evaluated value.</returns>
        public override float Evaluate(params object[] args)
        {
            float finalPower = power.Evaluate(args);
            
            return Mathf.Exp(finalPower);
        }
    }
}