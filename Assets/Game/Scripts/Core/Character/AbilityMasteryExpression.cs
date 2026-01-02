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

                if (character.Abilities.ContainsKey(ability))
                {
                    var mastery = character.Abilities[ability];
                    return ability.MasteryThresholds[Mathf.Clamp(mastery, 0, ability.MasteryThresholds.Count - 1)];
                }
                
                return ability.MasteryThresholds[0];
            }

            return 0;
        }
    }
}
