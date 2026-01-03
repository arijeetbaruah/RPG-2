using RPG.Core.Interfaces;

namespace RPG.StatusEffects
{
    [System.Serializable]
    public abstract class BaseStatusBehaviour
    {
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

        public StatusContext(ICharacter owner, StatusInstance instance)
        {
            if (owner == null)
                throw new System.ArgumentNullException(nameof(owner));
            if (instance == null)
                throw new System.ArgumentNullException(nameof(instance));
            
            this.owner = owner;
            this.instance = instance;
        }
    }
}
