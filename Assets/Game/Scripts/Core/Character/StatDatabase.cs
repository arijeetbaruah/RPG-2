using System.Collections.Generic;
using System.Linq;
using RPG.ConfigServices;
using UnityEditor;
using UnityEngine;

namespace RPG.Core.Character
{
    [CreateAssetMenu(fileName = "StatDatabase", menuName = "Game/Stats/StatDatabase")]
    public class StatDatabase : BaseConfig
    {
        public IReadOnlyList<StatData> Stats => _stats;
        
        [SerializeField] private StatData[] _stats;
        
        #if UNITY_EDITOR
        public static StatDatabase GetDatabaseEditor()
        {
            return UnityEditor.AssetDatabase.FindAssets($"t:{nameof(StatDatabase)}")
                .Select(guid => UnityEditor.AssetDatabase.GUIDToAssetPath(guid))
                .Select(path => AssetDatabase.LoadAssetAtPath<StatDatabase>(path)).FirstOrDefault();
        }
        #endif
    }
}
