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
        [SerializeField, Min(1)] private int baseDuration = 1;
        
        [Header("Stacking")]
        [SerializeField] private StatusStackType stackType;
        [SerializeField, Min(1)] private int maxStacks = 1;
        
        [Header("Timing")]
        [SerializeField] private StatusTiming tickTiming;
        
        [Header("Effects"), SerializeReference]
        [SerializeField] private BaseStatusBehaviour[] behaviours;
        
        public string StatusId => statusId?.GetLocalizedString() ?? string.Empty;
        public string DisplayName => displayName?.GetLocalizedString() ?? string.Empty;
        public int BaseDuration => baseDuration;
        public StatusStackType StackType => stackType;
        public int MaxStacks => maxStacks;
        public StatusTiming TickTiming => tickTiming;
        public IReadOnlyList<BaseStatusBehaviour> Behaviours => behaviours ?? System.Array.Empty<BaseStatusBehaviour>();
        
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
