using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsNull(property)) return 3 * EditorGUIUtility.singleLineHeight;
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight;

            var dynamicRect = new Rect(position.x, position.y, position.width, 2 * lineHeight);
            if (IsNull(property))
            {
                EditorGUI.HelpBox(dynamicRect, "This field is required.", MessageType.Error);
                dynamicRect.y += (2 * lineHeight);
            }

            dynamicRect.height = lineHeight;

            EditorGUI.PropertyField(dynamicRect, property);
        }

        bool IsNull(SerializedProperty property)
        {
            return property.objectReferenceValue == null;
        }
    }
}
