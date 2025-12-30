using System;
using System.Linq;
using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Addition : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        /// <summary>
        /// Produces the sum of the initial value and the evaluated results of all contained expressions.
        /// </summary>
        /// <param name="f">Initial value used as the starting accumulator.</param>
        /// <param name="args">Additional evaluation arguments (not used by this implementation).</param>
        /// <summary>
        /// Calculates the sum of all child expressions evaluated with the provided arguments.
        /// </summary>
        /// <param name="args">Arguments forwarded to each child expression's Evaluate call.</param>
        /// <returns>The sum of the evaluated values of all child expressions.</returns>
        public override float Evaluate(params object[] args)
        {
            float value = 0;
            foreach (MathExpression valueExpression in values)
            {
                value += valueExpression.Evaluate(args);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Subtraction : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        /// <summary>
        /// Subtracts the evaluated results of the contained expressions from the initial value.
        /// </summary>
        /// <param name="f">Initial value from which each contained expression's evaluated result is subtracted.</param>
        /// <summary>
        /// Computes the cumulative result of subtracting each contained expression (each evaluated with 0 as its first argument) from zero.
        /// </summary>
        /// <returns>The value after successive subtractions: zero minus the evaluation of each contained expression (each called with 0 as its first argument and the provided args).</returns>
        public override float Evaluate(params object[] args)
        {
            float value = 0;
            foreach (MathExpression valueExpression in values)
            {
                value -= valueExpression.Evaluate(0, args);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Constant : MathExpression
    {
        public float a;
        
        /// <summary>
        /// Evaluate and return the stored constant value.
        /// </summary>
        /// <summary>
        /// Returns the stored constant value.
        /// </summary>
        /// <param name="args">Ignored.</param>
        /// <returns>The stored constant value `a`.</returns>
        public override float Evaluate(params object[] args)
        {
            return a;
        }
    }
    
    [System.Serializable]
    public class EulerConstant : MathExpression
    {
        /// <summary>
        /// Return Euler's number (e) as a single-precision float.
        /// </summary>
        /// <param name="f">Ignored input value.</param>
        /// <param name="args">Ignored additional arguments.</param>
        /// <summary>
        /// Gets Euler's number e.
        /// </summary>
        /// <returns>Euler's number e as a single-precision float.</returns>
        public override float Evaluate(params object[] args)
        {
            return (float) Math.E;
        }
    }
}