using System.Collections.Generic;
using UnityEngine;

namespace RPG.ConfigServices
{
    [CreateAssetMenu(fileName = "ConfigDatabase", menuName = "Game/Config/ConfigDatabase")]
    public class ConfigDatabase : ScriptableObject
    {
        [SerializeField] private List<BaseConfig> configs = new ();
        
        public IReadOnlyList<BaseConfig> Configs => configs;
    }
}
