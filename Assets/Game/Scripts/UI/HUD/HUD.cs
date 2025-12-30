using System;
using RPG.Core.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        [SerializeField] private Image _manaBar;
        [SerializeField] private Image _staminaBar;

        [SerializeField] private DerivedStats _hpStats;
        [SerializeField] private DerivedStats _manaStats;
        [SerializeField] private DerivedStats _staminaStats;
        
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _manaText;
        [SerializeField] private TextMeshProUGUI _staminaText;
        
        private Character _player;
        
        /// <summary>
        /// Initializes the HUD: locates the player Character, subscribes to stat-change notifications, and updates HP, Mana, and Stamina displays.
        /// </summary>
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            CharacterResourceHandler.OnStatChanged += OnStatChanged;
            
            OnStatChanged(_player, _hpStats, 0);
            OnStatChanged(_player, _manaStats, 0);
            OnStatChanged(_player, _staminaStats, 0);
        }

        /// <summary>
        /// Updates a UI bar and its label to reflect the current and maximum resource values.
        /// </summary>
        /// <param name="maxValue">The maximum value of the resource; treated as zero when equal to 0 to avoid division by zero.</param>
        /// <param name="currentValue">The current value of the resource to display.</param>
        /// <param name="image">The UI Image whose fillAmount will be set to the current-to-max ratio.</param>
        /// <param name="text">The TextMeshProUGUI element that will display the formatted "current/max" values.</param>
        private void UpdateUI(float maxValue, float currentValue, Image image, TextMeshProUGUI text)
        {
            float cent = maxValue == 0 ? 0 : currentValue / maxValue;
            image.fillAmount = cent;
            text.SetText($"{currentValue.ToString("00")}/{maxValue.ToString("00")}");
            
            Debug.Log($"{currentValue}/{maxValue}");
        }

        /// <summary>
        /// Responds to a character stat change and updates the corresponding HUD elements for the local player.
        /// </summary>
        /// <param name="character">The character whose stat changed.</param>
        /// <param name="stat">The specific derived stat that changed (HP, Mana, or Stamina).</param>
        /// <param name="value">The value reported by the change event (not used by this handler).</param>
        private void OnStatChanged(Character character, DerivedStats stat, float value)
        {
            if (character != _player)
            {
                return;
            }

            if (stat == _hpStats)
            {
                var maxHp = character.CharacterResourceHandler.MaxHP;
                var currentHp = character.CharacterResourceHandler.CurrentHP;
                
                UpdateUI(maxHp, currentHp, _hpBar, _hpText);
            }
            else if (stat == _manaStats)
            {
                var maxMana = character.CharacterResourceHandler.MaxMana;
                var currentMana = character.CharacterResourceHandler.CurrentMana;
                
                UpdateUI(maxMana, currentMana, _manaBar, _manaText);
            }
            else if (stat == _staminaStats)
            {
                var maxStamina = character.CharacterResourceHandler.MaxStamina;
                var currentStamina = character.CharacterResourceHandler.CurrentStamina;
                
                UpdateUI(maxStamina, currentStamina, _staminaBar, _staminaText);
            }
        }
    }
}