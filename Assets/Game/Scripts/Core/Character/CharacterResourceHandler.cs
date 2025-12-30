using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Character
{
    [RequireComponent(typeof(Character))]
    public class CharacterResourceHandler : MonoBehaviour
    {
        public static event Action<Character, DerivedStats, float> OnStatChanged = delegate { };
        public static event Action<Character> OnDeathTriggered = delegate { };
        
        [SerializeField] private DerivedStats _hpStats;
        [SerializeField] private DerivedStats _manaStats;
        [SerializeField] private DerivedStats _staminaStats;
        
        private Character _character;
        
        private Dictionary<DerivedStats, DerivedStatsData> _data;
        
        public float MaxHP => GetMaxValue(_hpStats);
        public float MaxMana => GetMaxValue(_manaStats);
        public float MaxStamina => GetMaxValue(_staminaStats);
        
        public float CurrentHP => GetCurrentValue(_hpStats);
        public float CurrentMana => GetCurrentValue(_manaStats);
        public float CurrentStamina => GetCurrentValue(_staminaStats);

        private void Awake()
        {
            _character = GetComponent<Character>();

            _data = new Dictionary<DerivedStats, DerivedStatsData>();
            SetDerivedStats(_hpStats);
            SetDerivedStats(_manaStats);
            SetDerivedStats(_staminaStats);
        }

        public void UpdateHP(float delta)
        {
            UpdateStats(_hpStats, delta);

            if (CurrentHP == 0)
            {
                OnDeathTriggered.Invoke(_character);
            }
        }

        public void UpdateStats(DerivedStats stats, float delta)
        {
            if (delta == 0)
            {
                return;
            }
            
            if (!_data.ContainsKey(stats))
            {
                Debug.LogError($"No stats found for {stats}");
                return;
            }
            
            var derivedStats = _data[stats];
            derivedStats.Current = Mathf.Clamp(derivedStats.Current + delta, 0, derivedStats.Max);
            
            OnStatChanged.Invoke(_character, stats, derivedStats.Current);
        }

        private float GetCurrentValue(DerivedStats stat)
        {
            if (_data.TryGetValue(stat, out DerivedStatsData data))
            {
                return data.Current;
            }

            Debug.LogError($"Not able to find stat {stat}");
            return 0;
        }
        
        private float GetMaxValue(DerivedStats stat)
        {
            if (_data.TryGetValue(stat, out DerivedStatsData data))
            {
                return data.Max;
            }

            Debug.LogError($"Not able to find stat {stat}");
            return 0;
        }

        private void SetDerivedStats(DerivedStats newStats)
        {
            float value = _character.GetDerivedStat(newStats);
            _data.Add(newStats, new DerivedStatsData()
            {
                Max = value,
                Current = value
            });
        }

        [System.Serializable]
        public struct DerivedStatsData
        {
            public float Max;
            public float Current;
        }
    }
}
