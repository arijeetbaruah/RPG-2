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
        /// <returns>The initial value `f` plus the sum of each element in `values` evaluated with an input of 0.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value += valueExpression.Evaluate(0);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Substraction : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        /// <summary>
        /// Subtracts the evaluated results of the contained expressions from the initial value.
        /// </summary>
        /// <param name="f">Initial value from which each contained expression's evaluated result is subtracted.</param>
        /// <returns>The resulting value after successive subtractions of each contained expression (each evaluated with 0 as its input).</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value -= valueExpression.Evaluate(0);
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
        /// <returns>The stored constant value `a`.</returns>
        public override float Evaluate(float f, params object[] args)
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
        /// <returns>The value of Euler's number (e) as a float.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            return (float) Math.E;
        }
    }
}