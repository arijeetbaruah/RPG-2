using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [System.Serializable]
    public class MultiEffect : BaseAbilityEffect
    {
       [SerializeReference, SerializeField]
       private List<BaseAbilityEffect> _effects = new ();
        
        public override void Apply(AbilityContext context)
        {
            if (_effects == null || _effects.Count == 0)
            {
                return;
            }

            foreach (var effect in _effects)
            {
                if (effect == null)
                {
                    continue;
                }
                
                effect.Apply(context);
            }
        }
    }
}