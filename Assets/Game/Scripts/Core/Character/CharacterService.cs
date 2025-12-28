using System.Collections.Generic;
using RPG.Services;
using UnityEngine;

namespace RPG.Core.Character
{
    public class CharacterService : IService
    {
        public Dictionary<CharacterData, Character> _characters = new ();

        /// <summary>
        /// Adds a character to the service's character registry using its CharacterData as the key.
        /// </summary>
        /// <param name="character">The Character to add; its CharacterData will be used as the dictionary key.</param>
        public void AddCharacter(Character character)
        {
            _characters.Add(character.CharacterData, character);
        }

        /// <summary>
        /// Attempts to retrieve the Character associated with the specified CharacterData key.
        /// </summary>
        /// <param name="character">The CharacterData key to look up.</param>
        /// <param name="characterData">When this method returns, contains the Character associated with the key, or null if not found.</param>
        /// <returns>`true` if a matching Character was found; `false` otherwise.</returns>
        public bool TryGetCharacter(CharacterData character, out Character characterData)
        {
            return _characters.TryGetValue(character, out characterData);
        }

        /// <summary>
        /// Removes the character associated with the given CharacterData from the service.
        /// </summary>
        /// <param name="character">The character whose entry (keyed by its CharacterData) will be removed.</param>
        public void RemoveCharacter(Character character)
        {
            _characters.Remove(character.CharacterData);
        }
        
        /// <summary>
        /// Initializes the character service.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Per-frame update hook for the service to perform periodic work.
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Called when the service is destroyed; currently has an empty implementation and performs no cleanup.
        /// </summary>
        public void OnDestroy()
        {
        }
    }
}