using System.Collections.Generic;
using RPG.Core.Interfaces;
using UnityEngine;
using UnityEngine.Localization;

namespace RPG.StatusEffects
{
    [CreateAssetMenu(fileName = "StatusEffect", menuName = "Game/StatusEffect")]
    public class StatusEffect : ScriptableObject, IStatusEffect
    {
        [SerializeField] private LocalizedString statusId;
        [SerializeField] private LocalizedString displayName;
        
        [Header("Duration")]
        [SerializeField] private int baseDuration;
        
        [Header("Stacking")]
        [SerializeField] private StatusStackType stackType;
        [SerializeField] private int maxStacks;
        
        [Header("Timing")]
        [SerializeField] private StatusTiming tickTiming;
        
        [Header("Effects"), SerializeReference]
        [SerializeField] private BaseStatusBehaviour[] behaviours;
        
        public string StatusId => statusId.GetLocalizedString();
        public string DisplayName => displayName.GetLocalizedString();
        public int BaseDuration => baseDuration;
        public StatusStackType StackType => stackType;
        public int MaxStacks => maxStacks;
        public StatusTiming TickTiming => tickTiming;
        public IReadOnlyList<BaseStatusBehaviour> Behaviours => behaviours;
        
        public enum StatusStackType
        {
            None,       // Only one allowed
            Refresh,    // Reapply resets duration
            Stack       // Multiple stacks allowed
        }
        
        
        public enum StatusTiming
        {
            OnTurnStart,
            OnTurnEnd,
            OnAction,
            OnDamageTaken
        }
    }
}
