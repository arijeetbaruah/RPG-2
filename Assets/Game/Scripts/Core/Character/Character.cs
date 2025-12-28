using UnityEngine;

namespace RPG.Core.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterData characterData;

        public int GetDerivedStat(DerivedStats stats)
        {
            int val = 0;
            foreach (var modifier in stats.statModifiers)
            {
                var statValue = characterData.CoreStats[modifier.stat];

                switch (modifier.operatorType)
                {
                    case EOperatorType.Addition:
                        statValue +=  modifier.value;
                        break;
                    case EOperatorType.Subtraction:
                        statValue -=  modifier.value;
                        break;
                    case EOperatorType.Multiplication:
                        statValue *=  modifier.value;
                        break;
                    case EOperatorType.Division:
                        statValue /=  modifier.value;
                        break;
                }
                val += statValue;
            }

            if (!characterData.DerivedStats.TryGetValue(stats, out int derivedValue))
            {
                derivedValue = 0;
            }
            
            return val + derivedValue;
        }
    }
}
