using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace RPG.RequirementSystem
{
    [System.Serializable]
    public abstract class BaseRequirement
    {
        public abstract bool CheckRequirements(params object[] objects);
    }

    [System.Serializable]
    public class Requirements
    {
        [SerializeField, SerializeReference, OdinSerialize]
        private List<BaseRequirement> BaseRequirements;

        public bool EvaluateRequirements(params object[] objects)
        {
            foreach (var requirement in BaseRequirements)
            {
                if (!requirement.CheckRequirements(objects))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
