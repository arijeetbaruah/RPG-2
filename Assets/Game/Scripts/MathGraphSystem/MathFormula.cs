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

        public float Evaluate(params object[] args)
        {
            float result = 0f;

            foreach (MathExpression expression in _expressions)
            {
                result = expression.Evaluate(result, args);
            }
            
            return result;
        }

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
        public abstract float Evaluate(float f, params object[] args);
    }
}
