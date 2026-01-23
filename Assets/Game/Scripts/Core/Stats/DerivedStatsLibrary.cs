using System.Collections.Generic;
using RPG.ConfigServices;
using UnityEngine;

namespace RPG.Core.Character
{
    [CreateAssetMenu(fileName = "New DerivedStatsLibrary", menuName = "Game/Stats/DerivedStatsLibrary")]
    public class DerivedStatsLibrary : BaseConfig
    {
        public IReadOnlyList<DerivedStats> Stats => _stats;
        public DerivedStats SpeedStats => _speedStats;
        
        [SerializeField] private List<DerivedStats> _stats = new();

        [SerializeField] private DerivedStats _speedStats;
    }
}
