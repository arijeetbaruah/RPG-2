using RPG.Core.Interfaces;

namespace RPG.StatusEffects
{
    [System.Serializable]
    public abstract class BaseStatusBehaviour
    {
        /// <summary>
/// Perform this status effect's behavior using the provided runtime context.
/// </summary>
/// <param name="context">Runtime context containing the character owner and the status instance to process.</param>
public abstract void Execute(StatusContext context);
    }
    
    [System.Serializable]
    public class StatusInstance
    {
        public StatusEffect status;
        public int remainingDuration;
        public int stacks;
    }
    
    public class StatusContext
    {
        public ICharacter owner;
        public StatusInstance instance;

        /// <summary>
        /// Creates a new StatusContext associating an owning character with a specific status instance.
        /// </summary>
        /// <param name="owner">The character that owns the status effect.</param>
        /// <param name="instance">The status effect instance containing state (status, remainingDuration, stacks).</param>
        public StatusContext(ICharacter owner, StatusInstance instance)
        {
            this.owner = owner;
            this.instance = instance;
        }
    }
}