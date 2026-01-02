using System.Collections.Generic;
using UnityEngine;

namespace RPG.MathFormula
{
    [System.Serializable]
    public class Max : MathExpression
    {
        [SerializeReference]
        public MathExpression[] expressions;
        
        public override float Evaluate(params object[] args)
        {
            if (expressions == null || expressions.Length == 0)
            {
                return 0f; // or throw ArgumentException based on requirements
            }
            
            List<float> result = new List<float>();
            foreach (var expression in expressions)
            {
                if (expression == null)
                {
                    Debug.LogWarning("Null expression in Max.Evaluate, skipping.");
                    continue;
                }
                result.Add(expression.Evaluate(args));
            }

            if (result.Count == 0)
            {
                return 0f;
            }
            return Mathf.Max(result.ToArray());
        }
    }
    
    [System.Serializable]
    public class Min : MathExpression
    {
        [SerializeReference]
        public MathExpression[] expressions;
        
        public override float Evaluate(params object[] args)
        {
            if (expressions == null || expressions.Length == 0)
            {
                return 0f; // or throw ArgumentException based on requirements
            }
            
            List<float> result = new List<float>();
            foreach (var expression in expressions)
            {
                if (expression == null)
                {
                    Debug.LogWarning("Null expression in Max.Evaluate, skipping.");
                    continue;
                }
                result.Add(expression.Evaluate(args));
            }
            
            if (result.Count == 0)
            {
                return 0f;
            }
            return Mathf.Min(result.ToArray());
        }
    }
}
