//using com.absence.attributes.experimental;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//namespace com.absence.attributes.editor
//{
//    [CustomPropertyDrawer(typeof(InlineEditorAttribute))]
//    public class InlineEditorPropertyDrawer : PropertyDrawer
//    {
//        static Dictionary<Object, Editor> s_editorPairs = new();

//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            return base.GetPropertyHeight(property, label);
//        }

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            EditorGUI.PropertyField(position, property, label, true);

//            Object value = null;
//            try
//            {
//                value = property.objectReferenceValue;
//            }

//            catch
//            {
//                position.height = EditorGUIUtility.singleLineHeight;
//                EditorGUI.LabelField(position, "Field must derive from 'UnityEngine.Object'.");
//                return;
//            }

//            if (value == null)
//                return;

//            s_editorPairs[value].OnInspectorGUI();
//        }
//    }
//}
