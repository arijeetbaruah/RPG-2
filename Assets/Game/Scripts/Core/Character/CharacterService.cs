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
            _characters.Add(character.CharacterData, character);
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
