using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Character
{
    [CreateAssetMenu(fileName = "Derived Stats", menuName = "Game/Stats/DerivedStats")]
    public class DerivedStats : ScriptableObject
    {
        public IReadOnlyList<StatModifier> statModifiers;
        
        [SerializeField] private List<StatModifier> statsModifiers = new();
        
        [System.Serializable]
        public struct StatModifier
        {
            public Stat stat;
            public int value;
            public EOperatorType operatorType;
        }
    }

    public enum EOperatorType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }
}
