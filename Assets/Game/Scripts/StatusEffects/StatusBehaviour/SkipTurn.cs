using UnityEngine;

namespace RPG.StatusEffects.Behaviour
{
    [System.Serializable]
    public class SkipTurn : BaseStatusBehaviour
    {
        /// <summary>
        /// Executes the skip-turn status effect for the given context; this implementation performs no actions.
        /// </summary>
        /// <param name="context">The status execution context containing the affected actor and related state.</param>
        public override void Execute(StatusContext context)
        {
        }
    }
}