using System.Linq;
using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Multiply : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        /// <summary>
        /// Multiplies the initial value by the evaluated results of each contained expression.
        /// </summary>
        /// <param name="f">Initial value to be multiplied by each sub-expression's result.</param>
        /// <param name="args">Additional arguments (ignored by this implementation).</param>
        /// <returns>The product of the initial value and each contained expression's evaluated result.</returns>
        /// <remarks>Each contained expression is evaluated by calling Evaluate(0).</remarks>
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value *= valueExpression.Evaluate(0);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Divide : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        /// <summary>
        /// Divides the initial value by each contained expression's evaluated result in order.
        /// </summary>
        /// <param name="f">Initial value to be divided.</param>
        /// <param name="args">Additional arguments are ignored.</param>
        /// <returns>The resulting value after sequentially dividing by each expression's evaluated result.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value /= valueExpression.Evaluate(0);
            }
            
            return value;
        }
    }
}