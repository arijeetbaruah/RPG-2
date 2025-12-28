using UnityEngine;

namespace RPG.Core.Character
{
    public class Character : MonoBehaviour
    {
        public CharacterData CharacterData => _characterData;
        
        [SerializeField] private CharacterData _characterData;

        public float GetDerivedStat(DerivedStats stats)
        {
            return stats.Evaluate(_characterData);
        }
    }
}
