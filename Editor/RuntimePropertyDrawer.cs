using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomPropertyDrawer(typeof(RuntimeAttribute))]
    public class RuntimePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(!Application.isPlaying) return 0f;

            return EditorGUI.GetPropertyHeight(property,label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(!Application.isPlaying) return;

            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
