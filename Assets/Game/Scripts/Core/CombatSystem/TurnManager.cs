using System.Collections.Generic;
using System.Linq;
using RPG.ConfigServices;
using RPG.Core.Character;
using RPG.Core.Interfaces;
using RPG.Services;
using UnityEngine;

namespace RPG.CombatSystem
{
    public class TurnManager
    {
        private Queue<ICharacter> turnQueue = new();
        public ICharacter Current { get; private set; }
        
        public void StartBattle(List<ICharacter> participants)
        {
            DerivedStatsLibrary dsl = ServiceManager.Get<ConfigService>().GetConfig<DerivedStatsLibrary>();

            turnQueue = new Queue<ICharacter>(participants
                .OrderBy(c => c.GetDerivedStat(dsl.SpeedStats))
            );
            
            AdvanceTurn();
        }
        
        public void AdvanceTurn()
        {
            if (Current != null)
            {
                Current.OnTurnEnd();
            }
            
            Current = turnQueue.Dequeue();
            
            if (Current != null)
            {
                if (Current.IsDead)
                {
                    AdvanceTurn();
                    return;
                }
                
                turnQueue.Enqueue(Current);
                Current.OnTurnStart();
            }
        }
    }
}
