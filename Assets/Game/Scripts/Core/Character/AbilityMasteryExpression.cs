using System.Linq;
using RPG.AbilitySystem;
using RPG.Core.Character;
using RPG.MathFormula;
using UnityEngine;

namespace RPG.Abilities
{
    [System.Serializable]
    public class AbilityMasteryExpression : MathExpression
    {
        public override float Evaluate(params object[] args)
        {
            if (args.Any(a => a is BaseAbility) && args.OfType<Character>().Any())
            {
                BaseAbility ability = (BaseAbility)args.FirstOrDefault(a => a is BaseAbility);
                Character character = (Character)args.FirstOrDefault(a => a is Character);

                if (character.Abilities.Any(a => a.Ability == ability))
                {
                    var mastery = character.Abilities.FirstOrDefault(a => a.Ability == ability);
                    return ability.MasteryThresholds[Mathf.Clamp(mastery.MasteryLevel, 0, ability.MasteryThresholds.Count)];
                }
                
                return ability.MasteryThresholds[0];
            }

            return 0;
        }
    }
}
