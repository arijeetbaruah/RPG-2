using System.Collections.Generic;
using RPG.Services;
using UnityEngine;

namespace RPG.Core.Character
{
    public class CharacterService : IService
    {
        private Dictionary<CharacterData, Character> _characters = new ();

        public void AddCharacter(Character character)
        {
            if (character == null || character.CharacterData == null)
            {
                Debug.LogError("Cannot add null character or character with null CharacterData");
                return;
            }
            
            if (!_characters.TryAdd(character.CharacterData, character))
            {
                Debug.LogError($"Character {character.CharacterData.Name} already added");
            }
        }

        public bool TryGetCharacter(CharacterData character, out Character characterData)
        {
            return _characters.TryGetValue(character, out characterData);
        }

        public void RemoveCharacter(CharacterData character)
        {
            if (character == null)
            {
                Debug.LogError("Cannot add null CharacterData");
                return;
            }
            
            _characters.Remove(character);
        }
        
        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public void OnDestroy()
        {
        }
    }
}
