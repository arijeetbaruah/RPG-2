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
        
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
            foreach (MathExpression valueExpression in values)
            {
                value += valueExpression.Evaluate(0, args);
            }
            
            return value;
        }
    }
    
    [System.Serializable]
    public class Subtraction : MathExpression
    {
        [SerializeReference]
        public MathExpression[] values;
        
        public override float Evaluate(float f, params object[] args)
        {
            float value = f;
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
        
        public override float Evaluate(float f, params object[] args)
        {
            return a;
        }
    }
    
    [System.Serializable]
    public class EulerConstant : MathExpression
    {
        public override float Evaluate(float f, params object[] args)
        {
            return (float) Math.E;
        }
    }
}
