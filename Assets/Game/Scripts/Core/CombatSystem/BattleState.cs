using System.Collections.Generic;
using RPG.Core.Interfaces;
using UnityEngine;

namespace RPG.CombatSystem
{
    public class BattleState
    {
        public TurnManager turnManager;
        public List<ICharacter> allies;
        public List<ICharacter> enemies;
    }
}
