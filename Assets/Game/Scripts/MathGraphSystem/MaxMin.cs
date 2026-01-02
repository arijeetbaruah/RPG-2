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
            List<float> result = new List<float>();
            foreach (var expression in expressions)
            {
                result.Add(expression.Evaluate(args));
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
            List<float> result = new List<float>();
            foreach (var expression in expressions)
            {
                result.Add(expression.Evaluate(args));
            }
            
            return Mathf.Min(result.ToArray());
        }
    }
}
