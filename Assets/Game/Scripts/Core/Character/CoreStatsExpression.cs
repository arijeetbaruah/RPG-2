using System.Linq;
using RPG.MathFormula;

namespace RPG.Core.Character
{
    [System.Serializable]
    public class CoreStatsExpression : MathExpression
    {
        public Stat statData;
        
        public override float Evaluate(float f, params object[] args)
        {
            if (args.Any(a => a is Character))
            {
                Character character = (Character)args.First(a => a is Character);
                
                return character.CharacterData.CoreStats[statData];
            }
            
            if  (args.Any(a => a is CharacterData))
            {
                CharacterData characterData = (CharacterData)args.First(a => a is CharacterData);

                return characterData.CoreStats[statData];
            }
            
            return 0;
        }
    }
}
