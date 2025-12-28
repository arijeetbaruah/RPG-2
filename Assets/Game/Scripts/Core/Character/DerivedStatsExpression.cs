using System.Linq;
using RPG.MathFormula;
using UnityEngine;

namespace RPG.Core.Character
{
    [System.Serializable]
    public class DerivedStatsExpression : MathExpression
    {
        [SerializeField] private DerivedStats _stat;
        
        public override float Evaluate(float f, params object[] args)
        {
            if (args.Any(a => a is Character))
            {
                Character character = (Character)args.First(a => a is Character);
                return character.GetDerivedStat(_stat);
            }
            
            return 0;
        }
    }
}
