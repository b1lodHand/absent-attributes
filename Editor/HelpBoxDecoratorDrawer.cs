using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.absence.attributes.Editor
{
    [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
    public class HelpBoxDecoratorDrawer : DecoratorDrawer
    {
        HelpBoxAttribute helpBox => (attribute as HelpBoxAttribute);

        public override float GetHeight()
        {
            if (helpBox == null) return base.GetHeight();

            var helpBoxStyle = new GUIStyle(EditorStyles.helpBox);
            if (helpBoxStyle == null) return base.GetHeight();

            helpBoxStyle.wordWrap = true;

            return helpBox.initialized ? helpBox.height : GetMinBoxSize(helpBox.boxType);
        }

        public override void OnGUI(Rect position)
        {
            if(helpBox == null) return;

            var helpBoxStyle = new GUIStyle(EditorStyles.helpBox);
            helpBoxStyle.wordWrap = true;

            var calcHeight = Mathf.Max(GetMinBoxSize(helpBox.boxType),
                helpBoxStyle.CalcHeight(new GUIContent(helpBox.message), EditorGUIUtility.currentViewWidth) + 4);

            helpBox.SetHeight(calcHeight);

            EditorGUI.HelpBox(position, helpBox.message, GetConvertedMessageType(helpBox.boxType));
        }

        float GetMinBoxSize(HelpBoxType helpBoxType)
        {
            return helpBoxType switch
            {
                HelpBoxType.Info => 40f,
                HelpBoxType.Warning => 40f,
                HelpBoxType.Error => 40f,
                _ => EditorGUIUtility.singleLineHeight,
            };
        }

        MessageType GetConvertedMessageType(HelpBoxType helpBoxType)
        {
            return helpBoxType switch
            {
                HelpBoxType.Info => MessageType.Info,
                HelpBoxType.Warning => MessageType.Warning,
                HelpBoxType.Error => MessageType.Error,
                _ => MessageType.None,
            };
        }
    }
}
