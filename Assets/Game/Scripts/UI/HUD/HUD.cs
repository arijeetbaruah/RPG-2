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
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            CharacterResourceHandler.OnStatChanged += OnStatChanged;
            
            OnStatChanged(_player, _hpStats, 0);
            OnStatChanged(_player, _manaStats, 0);
            OnStatChanged(_player, _staminaStats, 0);
        }

        private void UpdateUI(float maxValue, float currentValue, Image image, TextMeshProUGUI text)
        {
            float cent = maxValue == 0 ? 0 : currentValue / maxValue;
            image.fillAmount = cent;
            text.SetText($"{currentValue.ToString("00")}/{maxValue.ToString("00")}");
            
            Debug.Log($"{currentValue}/{maxValue}");
        }

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
