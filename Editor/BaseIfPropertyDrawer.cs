using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    /// <summary>
    /// Used to manipulate the drawing process of a property with a control. (do NOT use with custom class drawers.)
    /// </summary>
    [CustomPropertyDrawer(typeof(BaseIfAttribute), true)]
    public class BaseIfPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Check(property) && (attribute as BaseIfAttribute).outputMethod == BaseIfAttribute.OutputMethod.ShowHide)
                return 0f;

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        bool Check(SerializedProperty property)
        {
            var baseIf = attribute as BaseIfAttribute;
            string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, baseIf.controlPropertyName) :
                baseIf.controlPropertyName;

            var comparedField = property.serializedObject.FindProperty(path);

            if (comparedField == null)
            {
                Debug.LogError($"Cannot find property with name: {path}");
                return baseIf.invert ? false : true;
            }

            if (baseIf.directBool) return baseIf.invert ? comparedField.boolValue : !comparedField.boolValue;

            bool result = Process();

            return baseIf.invert ? !result : result;

            bool Process()
            {
                switch (comparedField.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        return comparedField.boolValue != (bool)baseIf.targetValue;

                    case SerializedPropertyType.Enum:
                        return !(comparedField.enumValueIndex.Equals((int)baseIf.targetValue));

                    case SerializedPropertyType.Integer:
                        return comparedField.intValue != (int)baseIf.targetValue;

                    case SerializedPropertyType.Float:
                        return comparedField.floatValue != (float)baseIf.targetValue;

                    case SerializedPropertyType.String:
                        return comparedField.stringValue != (string)baseIf.targetValue;

                    case SerializedPropertyType.ObjectReference:
                        if (baseIf.targetValue != null) return !comparedField.objectReferenceValue.Equals(baseIf.targetValue);
                        else return comparedField.objectReferenceValue != null;

                    case SerializedPropertyType.ExposedReference:
                        return !comparedField.objectReferenceValue.Equals(baseIf.targetValue);

                    case SerializedPropertyType.ManagedReference:
                        return !comparedField.managedReferenceValue.Equals(baseIf.targetValue);

                    default:
                        Debug.LogError($"This type is not supported: {comparedField.type}");
                        return false;
                }
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            switch ((attribute as BaseIfAttribute).outputMethod)
            {
                case BaseIfAttribute.OutputMethod.ShowHide:
                    if(Check(property)) EditorGUI.PropertyField(position, property, label, true);
                    break;

                case BaseIfAttribute.OutputMethod.EnableDisable:
                    if (!Check(property)) GUI.enabled = false;
                    EditorGUI.PropertyField(position, property, label, true);
                    GUI.enabled = true;
                    break;

                default:
                    break;
            }
        }

    }
}