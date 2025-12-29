using System.Collections.Generic;
using RPG.ConfigServices;
using UnityEngine;

namespace RPG.Core.Character
{
    [CreateAssetMenu(fileName = "New DerivedStatsLibrary", menuName = "Game/Stats/DerivedStatsLibrary")]
    public class DerivedStatsLibrary : BaseConfig
    {
        public IReadOnlyList<DerivedStats> Stats => _stats;
        
        [SerializeField] private List<DerivedStats> _stats = new();
    }
}
