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
        /// <summary>
        /// Computes the product of all contained math expressions evaluated with the provided arguments.
        /// </summary>
        /// <param name="args">Optional runtime arguments forwarded to each contained expression's Evaluate call.</param>
        /// <returns>The product of each contained expression's evaluated value; returns 1 if there are no contained expressions.</returns>
        /// <remarks>Each contained expression is evaluated by calling its Evaluate method with the same <paramref name="args"/>.</remarks>
        public override float Evaluate(params object[] args)
        {
            float value = 1;
            foreach (MathExpression valueExpression in values)
            {
                value *= valueExpression.Evaluate(args);
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
        /// <summary>
        /// Sequentially divides 1 by the evaluated result of each sub-expression in <c>values</c>.
        /// </summary>
        /// <param name="args">Optional runtime arguments forwarded to each sub-expression's <c>Evaluate</c> call.</param>
        /// <returns>The final quotient after processing all sub-expressions; returns 0 if any sub-expression evaluates to a value approximately equal to zero.</returns>
        public override float Evaluate(params object[] args)
        {
            float value = 1;
            foreach (MathExpression valueExpression in values)
            {
                float divisor = valueExpression.Evaluate(args);
                if (Mathf.Approximately(divisor, 0f))
                {
                    Debug.LogError("Division by zero attempted in Divide expression");
                    return 0;
                }
                
                value /= divisor;
            }
            
            return value;
        }
    }
}