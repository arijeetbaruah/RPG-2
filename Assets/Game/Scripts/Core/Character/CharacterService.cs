using System.Collections.Generic;
using RPG.Services;
using UnityEngine;

namespace RPG.Core.Character
{
    public class CharacterService : IService
    {
        public Dictionary<CharacterData, Character> _characters = new ();

        public void AddCharacter(Character character)
        {
            if (!_characters.TryAdd(character.CharacterData, character))
            {
                Debug.LogError($"Character {character.CharacterData.Name} already added");
            }
        }

        public bool TryGetCharacter(CharacterData character, out Character characterData)
        {
            return _characters.TryGetValue(character, out characterData);
        }

        public void RemoveCharacter(Character character)
        {
            _characters.Remove(character.CharacterData);
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
