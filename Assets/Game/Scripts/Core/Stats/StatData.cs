using System;
using UnityEngine;
using UnityEngine.Localization;

namespace RPG.Core.Character
{
    [System.Serializable]
    public struct StatData
    {
        [field:SerializeField] public string StatId { get; private set; }
        [field:SerializeField] public LocalizedString StatName { get; private set; }
        [field:SerializeField] public LocalizedSprite StatIcon { get; private set; }
    }

    [System.Serializable]
    public class Stat : IEquatable<StatData>
    {
        [field:SerializeField] public string StatId { get; private set; }

        public static bool operator ==(Stat s, StatData sd) => s.Equals(sd);
        public static bool operator !=(Stat s, StatData sd) => !s.Equals(sd);

        public Stat()
        {
        }
        
        public Stat(StatData data)
        {
            StatId = data.StatId;
        }
        
        protected bool Equals(Stat other)
        {
            return StatId == other.StatId;
        }

        public bool Equals(StatData other)
        {
            return other.StatId == StatId;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Stat)obj);
        }

        public override int GetHashCode()
        {
            return (StatId != null ? StatId.GetHashCode() : 0);
        }
        
        public override string ToString() => StatId;
    }
}
