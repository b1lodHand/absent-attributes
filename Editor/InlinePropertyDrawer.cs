using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomPropertyDrawer(typeof(InlineAttribute))]
    public class InlinePropertyDrawer : PropertyDrawer
    {
        private Dictionary<string, Vector2> m_pathScrollPairs = new();
        private Dictionary<string, float> m_pathHeightPairs = new();

        const int k_constantLineCount = 1;
        const float k_inlineEditorHeight = 500f;
        const float k_buttonWidth = 40f;
        const float k_fieldBoxPadding = 2f;
        const float k_majorSpacing = 2f;
        const float k_inlineEditorPadding = 20f;

        InlineAttribute inline => attribute as InlineAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            if (!property.isExpanded)
                return spacing + height;

            UnityEngine.Object objectReferenceValue = property.objectReferenceValue;

            float inlineHeight = 0f;

            if (objectReferenceValue == null)
                return (k_constantLineCount * (spacing + height));
            else
            {
                SerializedObject so = new SerializedObject(objectReferenceValue);
                SerializedProperty iterator = so.GetIterator();
                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren))
                {
                    using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                    {
                        inlineHeight += EditorGUI.GetPropertyHeight(iterator, true);
                    }

                    enterChildren = false;
                }
            }

            float realInlineHeight = Mathf.Min(inlineHeight, k_inlineEditorHeight);

            if (m_pathHeightPairs.ContainsKey(property.propertyPath))
                m_pathHeightPairs[property.propertyPath] = inlineHeight;
            else 
                m_pathHeightPairs.Add(property.propertyPath, inlineHeight);

            int totalLines = k_constantLineCount;

            float addition = 0f;

            if (objectReferenceValue != null && property.isExpanded) 
                addition += realInlineHeight + (k_inlineEditorPadding * 2) + spacing + k_majorSpacing;

            return (totalLines * (spacing + height)) + addition + k_fieldBoxPadding;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UnityEngine.Object objectReferenceValue = property.objectReferenceValue;

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;
            float step = spacing + height;
            float normalX = position.x;
            float normalWidth = position.width;

            Color color = Color.black;
            color.a = 0.1f;

            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;
            position.height += k_fieldBoxPadding;

            if (objectReferenceValue != null)
                EditorGUI.DrawRect(position, color);

            position.height -= k_fieldBoxPadding;

            EditorGUI.indentLevel++;

            position = EditorGUI.IndentedRect(position);

            float normalWidth2 = position.width;

            bool drawNewButton = objectReferenceValue == null && inline.newButtonId != 0;
            bool drawDelButton = objectReferenceValue != null && inline.delButtonId != 0;

            if (drawDelButton || drawNewButton)
                position.width -= k_buttonWidth;

            Rect foldoutRect = position;
            foldoutRect.width /= 2;
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, "", true, GUI.skin.label);

            bool enabledPreviously = GUI.enabled;

            using (new EditorGUI.DisabledGroupScope(inline.readonlyField))
            {
                EditorGUI.ObjectField(position, property);
            }

            position.x += normalWidth2 - k_buttonWidth + spacing;
            position.width = k_buttonWidth - spacing;

            if (objectReferenceValue == null)
            {
                if (drawNewButton)
                {
                    FieldButtonManager.ButtonGUI(position, inline.newButtonId, "New", 
                        GUI.skin.button, 
                        new object[] { property.serializedObject.targetObject, property.objectReferenceValue });
                }
            }

            else
            {
                if (drawDelButton)
                {
                    FieldButtonManager.ButtonGUI(position, inline.delButtonId, "Del",
                        GUI.skin.button,
                        new object[] { property.serializedObject.targetObject, property.objectReferenceValue });
                }
            }

            position.x = normalX;
            position.width = normalWidth;

            position.y += step;
            position.y += k_majorSpacing;
            position.y += k_inlineEditorPadding / 2;

            if (objectReferenceValue != null && property.isExpanded)
            {
                position.height = Mathf.Min(m_pathHeightPairs[property.propertyPath], k_inlineEditorHeight);

                position.y -= k_inlineEditorPadding / 2;
                position.height += k_inlineEditorPadding;

                EditorGUI.DrawRect(position, color);

                position = EditorGUI.IndentedRect(position);

                position.y += k_inlineEditorPadding / 2;
                position.height -= k_inlineEditorPadding;

                Vector2 scrollValue = Vector2.zero;
                if (!m_pathScrollPairs.TryGetValue(property.propertyPath, out scrollValue))
                    m_pathScrollPairs.Add(property.propertyPath, scrollValue);

                SerializedObject so = new SerializedObject(objectReferenceValue);

                Rect total = position;
                total.height = m_pathHeightPairs[property.propertyPath];
                using (GUI.ScrollViewScope scope = new GUI.ScrollViewScope(position, scrollValue, total))
                {
                    DoDrawDefaultInspector(position, so);
                    m_pathScrollPairs[property.propertyPath] = scope.scrollPosition;
                }

                position.y += k_inlineEditorHeight;
            }

            position.height = height;

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        private static bool DoDrawDefaultInspector(Rect position, SerializedObject obj)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.BeginChangeCheck();
            obj.UpdateIfRequiredOrScript();
            SerializedProperty iterator = obj.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                {
                    EditorGUI.PropertyField(position, iterator, true);
                    position.y += EditorGUI.GetPropertyHeight(iterator, true);
                    position.y += EditorGUIUtility.standardVerticalSpacing;
                }

                enterChildren = false;
            }

            obj.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }

        protected bool DrawHeaderFoldout(Rect position, SerializedProperty property, GUIContent label, bool foldout)
        {
            string text = property.displayName;

            GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                richText = true,
            };

            return EditorGUI.Foldout(position, foldout, text, true, foldoutStyle);
        }
    }
}
