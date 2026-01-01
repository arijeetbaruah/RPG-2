using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace RPG.Core.Character.Editor
{
    public class StatEditor : OdinValueDrawer<Stat>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var stat = this.ValueEntry.SmartValue;

            if (stat == null)
            {
                stat = new Stat();
                this.ValueEntry.SmartValue = stat;
            }
            
            //SirenixEditorGUI.BeBeginProperty(label);
            var options = StatDatabase.GetDatabaseEditor().Stats;
            var optionsStrId = options.Select(s => s.StatId).ToArray();
            int index = Mathf.Max(0, System.Array.IndexOf(optionsStrId, stat.StatId));
            if (index < 0) index = 0;

            int newIndex = SirenixEditorFields.Dropdown(label, index, options.Select(s => s.StatName.GetLocalizedString()).ToArray());

            if (newIndex != index)
            {
                SetStatId(stat, options[newIndex].StatId);
            }
            
            //SirenixEditorGUI.EndProperty();
        }
        
        private void SetStatId(Stat stat, string value)
        {
            var prop = typeof(Stat).GetProperty(nameof(Stat.StatId));
            prop.SetValue(stat, value);
        }
    }
}
