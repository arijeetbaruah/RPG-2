using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace RPG.MathFormula
{
    [CreateAssetMenu(fileName = "Math Formula", menuName = "Maths/Math Formula")]
    public class MathFormula : ScriptableObject
    {
        [SerializeField, SerializeReference, ListDrawerSettings(Expanded = true), OdinSerialize]
        private List<MathExpression> _expressions;

        /// <summary>
        /// Computes a final numeric value by applying each configured MathExpression in sequence to a running result using the provided arguments.
        /// </summary>
        /// <param name="args">Optional additional values passed to each MathExpression during evaluation.</param>
        /// <summary>
        /// Evaluates the formula by applying each contained MathExpression in sequence.
        /// </summary>
        /// <param name="args">Optional arguments forwarded to each MathExpression during evaluation.</param>
        /// <returns>The final float value produced after all expressions have been applied.</returns>
        public float Evaluate(params object[] args)
        {
            float result = 0f;

            foreach (MathExpression expression in _expressions)
            {
                result = expression.Evaluate(args);
            }
            
            return result;
        }

        /// <summary>
        /// Evaluates this MathFormula and logs the resulting value to the Unity console.
        /// </summary>
        /// <remarks>
        /// Invoked from the Unity editor context menu labeled "Evaluate".
        /// </remarks>
        [ContextMenu("Evaluate")]
        public void Test()
        {
            float val = Evaluate();
            Debug.Log(val);
        }
    }

    [System.Serializable]
    public abstract class MathExpression
    {
        /// <summary>
/// Transforms the incoming value using this expression, optionally influenced by additional arguments.
/// </summary>
/// <param name="f">The input value to transform (the running result passed into this expression).</param>
/// <param name="args">Optional expression-specific arguments that may affect evaluation.</param>
/// <summary>
/// Evaluates this math expression using the supplied optional arguments.
/// </summary>
/// <param name="args">Optional contextual values (for example variables or runtime inputs) consumed by the expression; implementations may ignore or interpret these values as needed.</param>
/// <returns>The float result produced by the expression.</returns>
public abstract float Evaluate(params object[] args);
    }
}