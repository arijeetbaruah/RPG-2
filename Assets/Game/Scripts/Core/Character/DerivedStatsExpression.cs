using System.Linq;
using RPG.MathFormula;
using UnityEngine;

namespace RPG.Core.Character
{
    [System.Serializable]
    public class DerivedStatsExpression : MathExpression
    {
        [SerializeField] private DerivedStats _stat;
        
        /// <summary>
        /// Evaluates and returns the configured derived stat for the first Character found in <paramref name="args"/>.
        /// </summary>
        /// <param name="f">Unused numeric input (ignored by this expression).</param>
        /// <param name="args">A set of objects among which a Character may be provided; the first Character encountered is used.</param>
        /// <returns>The value of the configured derived stat for the first Character in <paramref name="args"/>, or 0 if no Character is present.</returns>
        public override float Evaluate(params object[] args)
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