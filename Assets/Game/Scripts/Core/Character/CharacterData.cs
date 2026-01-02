using System.Collections.Generic;
using System.Linq;
using RPG.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace RPG.Core.Character
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "Game/Character/Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field:SerializeField] public string CharacterId { get; private set; }
        [field:SerializeField] public LocalizedString Name { get; private set; }

        [TitleGroup("Resistances")]
        [EnumToggleButtons, SerializeField, HideLabel]
        private DamageType _resistance;
        [TitleGroup("Vurnability")]
        [EnumToggleButtons, SerializeField, HideLabel]
        private DamageType _vurnability;
        [Space]
        
        [InfoBox("Duplicate Core Stats", InfoMessageType.Error, VisibleIf = nameof(OnCoreStatChanged))]
        [SerializeField] private List<CharacterCoreStatsData> _coreStats;

        [InfoBox("Duplicate Derived Stats", InfoMessageType.Error, VisibleIf = nameof(OnDerivedStatChanged))]
        [SerializeField] private List<CharacterDerivedStatsData> _derivedStats;
        
        private bool _duplicateCoreStats = false;

        public DamageType Resistance => _resistance;
        public DamageType Vurnability => _vurnability;
        
        public IReadOnlyDictionary<Stat, int> CoreStats => _coreStats.ToDictionary(s => s.statData, s => s.Value);
        public IReadOnlyDictionary<DerivedStats, int> DerivedStats => _derivedStats.ToDictionary(s => s.statData, s => s.Value);

        private bool OnCoreStatChanged()
        {
            return _coreStats.GroupBy(stat => stat.statData)
                .Any(s => s.Count() > 1);
        }

        private bool OnDerivedStatChanged()
        {
            return _derivedStats.GroupBy(stat => stat.statData)
                .Any(s => s.Count() > 1);
        }

        [OnInspectorInit]
        private void OnInit()
        {
            if (_coreStats == null || _coreStats.Count == 0)
            {
                #if UNITY_EDITOR
                var db = StatDatabase.GetDatabaseEditor();
                _coreStats = db.Stats.Select(s => new CharacterCoreStatsData() { statData = new Stat(s) }).ToList();
                #endif
            }
        }
        
        [System.Serializable]
        private struct CharacterDerivedStatsData
        {
            [FormerlySerializedAs("Stat")] [HideLabel, HorizontalGroup]
            public DerivedStats statData;
            [HideLabel, HorizontalGroup]
            public int Value;
        }
        
        [System.Serializable]
        private struct CharacterCoreStatsData
        {
            [FormerlySerializedAs("Stat")] [HideLabel, HorizontalGroup]
            public Stat statData;
            [HideLabel, HorizontalGroup]
            public int Value;
        }
    }
}
