using System.Linq;
using RPG.MathFormula;

namespace RPG.Core.Character
{
    [System.Serializable]
    public class CoreStatsExpression : MathExpression
    {
        public Stat statData;
        
        /// <summary>
        /// Resolves a core stat value from the provided Character or CharacterData arguments.
        /// </summary>
        /// <param name="f">Unused placeholder float parameter.</param>
        /// <param name="args">An array inspected for a Character or CharacterData; the first matching instance is used to look up the core stat identified by <c>statData</c>.</param>
        /// <returns>The value of the core stat named by <c>statData</c> from the first Character or CharacterData found in <paramref name="args"/>, or <c>0</c> if neither is present.</returns>
        public override float Evaluate(float f, params object[] args)
        {
            if (args.Any(a => a is Character))
            {
                Character character = (Character)args.First(a => a is Character);

                if (!character.CharacterData.CoreStats.TryGetValue(statData, out int stat))
                {
                    stat = 0;
                }

                return stat;
            }
            
            if  (args.Any(a => a is CharacterData))
            {
                CharacterData characterData = (CharacterData)args.First(a => a is CharacterData);

                if (!characterData.CoreStats.TryGetValue(statData, out int stat))
                {
                    stat = 0;
                }

                return stat;
            }
            
            return 0;
        }
    }
}