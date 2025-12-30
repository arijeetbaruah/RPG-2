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

        /// <summary>
        /// Cache the Character component, initialize the derived-stats storage, and populate entries for HP, Mana, and Stamina with their initial values.
        /// </summary>
        private void Awake()
        {
            _character = GetComponent<Character>();

            _data = new Dictionary<DerivedStats, DerivedStatsData>();
            SetDerivedStats(_hpStats);
            SetDerivedStats(_manaStats);
            SetDerivedStats(_staminaStats);
        }

        /// <summary>
        /// Adjusts the character's current HP by the given amount and triggers death if HP reaches zero.
        /// </summary>
        /// <param name="delta">Amount to add to current HP; positive increases HP, negative decreases HP.</param>
        public void UpdateHP(float delta)
        {
            UpdateStats(_hpStats, delta);

            if (CurrentHP <= 0)
            {
                OnDeathTriggered.Invoke(_character);
            }
        }

        /// <summary>
        /// Adjusts the current value of the specified derived stat by a signed amount and notifies listeners of the new value.
        /// </summary>
        /// <param name="stats">The derived stat to modify (e.g., HP, Mana, Stamina).</param>
        /// <param name="delta">Amount to change the current value; positive increases, negative decreases. The resulting value is clamped between 0 and the stat's Max.</param>
        /// <remarks>
        /// If the stat is not registered, an error is logged and no change is made. After a successful update, the <see cref="OnStatChanged"/> event is invoked with the updated current value.
        /// </remarks>
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
            _data[stats] = derivedStats;
            
            OnStatChanged.Invoke(_character, stats, derivedStats.Current);
        }

        /// <summary>
        /// Retrieve the current value for the specified derived stat.
        /// </summary>
        /// <param name="stat">The derived stat to query.</param>
        /// <returns>The current value of the specified stat, or 0 if the stat is not found.</returns>
        private float GetCurrentValue(DerivedStats stat)
        {
            if (_data.TryGetValue(stat, out DerivedStatsData data))
            {
                return data.Current;
            }

            Debug.LogError($"Not able to find stat {stat}");
            return 0;
        }
        
        /// <summary>
        /// Retrieves the maximum value for the specified derived stat.
        /// </summary>
        /// <param name="stat">The derived stat whose maximum value to retrieve.</param>
        /// <returns>The maximum value for the stat, or 0 if the stat is not present.</returns>
        private float GetMaxValue(DerivedStats stat)
        {
            if (_data.TryGetValue(stat, out DerivedStatsData data))
            {
                return data.Max;
            }

            Debug.LogError($"Not able to find stat {stat}");
            return 0;
        }

        /// <summary>
        /// Adds an entry for the specified derived stat to the internal data dictionary and initializes its max and current values from the character's current stat value.
        /// </summary>
        /// <param name="newStats">The derived stat to initialize and store.</param>
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