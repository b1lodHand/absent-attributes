using com.absence.attributes.experimental;
using com.absence.attributes.internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class absentEditorExtension : Editor
    {
        static Dictionary<string, FieldInfo> s_fieldInfos = new();
        static Dictionary<BeginFoldoutGroupAttribute, bool> s_togglePairs = new();

        static Stack<bool> s_activeFoldoutGroups = new();
        static bool s_foldoutBlocks = false;

        static int s_foldoutDifference = 0;
        static int s_readonlyDifference = 0;
        static int s_horizontalDifference = 0;
        static int s_verticalDifference = 0;

        static readonly Dictionary<Type, Action<BaseBeginLayoutAttribute>> s_beginActions = new()
        {
            { typeof(BeginHorizontalAttribute), OnBeginHorizontal },
            { typeof(BeginVerticalAttribute), OnBeginVertical },
            { typeof(BeginReadonlyGroupAttribute), OnBeginReadonly },
            { typeof(BeginFoldoutGroupAttribute), OnBeginFoldout },
        };

        static readonly Dictionary<Type, Action<BaseEndLayoutAttribute>> s_endActions = new()
        {
            { typeof(EndHorizontalAttribute), OnEndHorizontal },
            { typeof(EndVerticalAttribute), OnEndVertical },
            { typeof(EndReadonlyGroupAttribute), OnEndReadonly },
            { typeof(EndFoldoutGroupAttribute), OnEndFoldout },
        };

        static void OnBeginFoldout(BaseBeginLayoutAttribute beginFoldoutAttribute)
        {
            BeginFoldoutGroupAttribute foldoutAttribute = 
                beginFoldoutAttribute as BeginFoldoutGroupAttribute;

            bool isExpanded = false;
            if (!s_togglePairs.ContainsKey(foldoutAttribute))
                s_togglePairs.Add(foldoutAttribute, isExpanded);
            else 
                isExpanded = s_togglePairs[foldoutAttribute];

            GUIStyle style = new(EditorStyles.foldout)
            {
                richText = true,
            };

            if (!s_foldoutBlocks)
            {
                isExpanded = EditorGUILayout.Foldout(isExpanded, beginFoldoutAttribute.label,
                foldoutAttribute.toggleOnLabelClick, style);

                s_togglePairs[foldoutAttribute] = isExpanded;
            }

            EditorGUI.indentLevel++;
            s_foldoutDifference++;

            if (!isExpanded) s_foldoutBlocks = true;

            s_activeFoldoutGroups.Push(isExpanded);
        }

        static void OnBeginReadonly(BaseBeginLayoutAttribute beginReadonlyAttribute)
        {
            s_readonlyDifference++;

            if (s_foldoutBlocks)
                return;

            GUI.enabled = false;
        }

        static void OnBeginVertical(BaseBeginLayoutAttribute beginVertical)
        {
            s_verticalDifference++;

            if (s_foldoutBlocks)
                return;

            GUIStyle style2 = new(EditorStyles.label)
            {
                alignment = TextAnchor.LowerCenter,
                richText = true,
            };

            string vertStyle = beginVertical.style;
            string vertLabel = beginVertical.label;

            if (vertStyle != null)
            {
                if (vertLabel != null) GUILayout.BeginVertical(vertLabel, vertStyle);
                else GUILayout.BeginVertical(vertStyle);
            }

            else
            {
                EditorGUILayout.BeginVertical();
                if (vertLabel != null) EditorGUILayout.LabelField(vertLabel);
            }
        }

        static void OnBeginHorizontal(BaseBeginLayoutAttribute beginHorizontal)
        {
            s_horizontalDifference++;

            if (s_foldoutBlocks)
                return;

            GUIStyle style2 = new(EditorStyles.label)
            {
                alignment = TextAnchor.LowerCenter,
                richText = true,
            };

            string vertStyle = beginHorizontal.style;
            string vertLabel = beginHorizontal.label;

            if (vertStyle != null)
            {
                if (vertLabel != null) GUILayout.BeginHorizontal(vertLabel, vertStyle);
                else GUILayout.BeginHorizontal(vertStyle);
            }

            else
            {
                EditorGUILayout.BeginHorizontal();
                if (vertLabel != null) EditorGUILayout.LabelField(vertLabel);
            }
        }

        static void OnEndHorizontal(BaseEndLayoutAttribute endHorizontal)
        {
            s_horizontalDifference--;

            if (s_foldoutBlocks)
                return;

            EditorGUILayout.EndHorizontal();
        }

        static void OnEndVertical(BaseEndLayoutAttribute endVertical)
        {
            s_verticalDifference--;

            if (s_foldoutBlocks)
                return;

            EditorGUILayout.EndVertical();
        }

        static void OnEndReadonly(BaseEndLayoutAttribute endReadonly)
        {
            s_readonlyDifference--;

            if (s_foldoutBlocks)
                return;

            GUI.enabled = true;
        }

        static void OnEndFoldout(BaseEndLayoutAttribute endFoldout)
        {
            EditorGUI.indentLevel--;
            s_foldoutDifference--;

            if (s_foldoutDifference <= 0)
            {
                s_foldoutBlocks = false;
                return;
            }

            s_activeFoldoutGroups.Pop();

            var foldoutHistory = s_activeFoldoutGroups.ToList().GetRange(0, s_foldoutDifference).
                ToList();

            s_foldoutBlocks = foldoutHistory.Any(foldout => false);
        }

        private void OnEnable()
        {
            FieldInfo[] fieldInfos = target.GetType()
            .GetFields(BindingFlags.FlattenHierarchy |
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public);

            s_fieldInfos = new();
            for (int i = 0; i < fieldInfos.Length; i++) 
            {
                FieldInfo fieldInfo = fieldInfos[i];
                s_fieldInfos.Add(fieldInfo.Name, fieldInfo);
            }
        }

        private void OnDisable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            bool needsMark = DrawInspector();
            //DrawDefaultInspector();

            DrawButtons();

            if (needsMark)
            {
                EditorUtility.SetDirty(target);
            }

            return;

#pragma warning disable CS0219 // Variable is assigned but its value is never used
            bool DrawInspector()
            {
                bool result = false;

                EditorGUI.BeginChangeCheck();
                serializedObject.UpdateIfRequiredOrScript();
                SerializedProperty iterator = serializedObject.GetIterator();
                bool enterChildren = true;

                s_foldoutBlocks = false;

                s_activeFoldoutGroups.Clear();

                s_foldoutDifference = 0;
                s_readonlyDifference = 0;
                s_horizontalDifference = 0;
                s_verticalDifference = 0;

                while (iterator.NextVisible(enterChildren))
                {
                    #region Initialization

                    FieldInfo fieldInfo = null;
                    if (s_fieldInfos.ContainsKey(iterator.name)) 
                        fieldInfo = s_fieldInfos[iterator.name];

                    bool bypass = fieldInfo == null;
                    if (bypass)
                    {
                        using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                        {
                            EditorGUILayout.PropertyField(iterator, true);
                        }

                        continue;
                    }

                    #endregion

                    bool hasBeginHorizontal = FindAttribute(fieldInfo, out BeginHorizontalAttribute beginHorizontal);
                    bool hasBeginVertical = FindAttribute(fieldInfo, out BeginVerticalAttribute beginVertical);
                    bool hasBeginReadonly = FindAttribute(fieldInfo, out BeginReadonlyGroupAttribute beginReadonlyAttribute);
                    bool hasBeginFoldout = FindAttribute(fieldInfo, out BeginFoldoutGroupAttribute beginFoldoutAttribute);
                    bool hasEndFoldout = FindAttribute(fieldInfo, out EndFoldoutGroupAttribute endFoldoutAttribute);
                    bool hasEndReadonly = FindAttribute(fieldInfo, out EndReadonlyGroupAttribute endReadonlyAttribute);
                    bool hasEndHorizontal = FindAttribute(fieldInfo, out EndHorizontalAttribute endHorizontal);
                    bool hasEndVertical = FindAttribute(fieldInfo, out EndVerticalAttribute endVertical);

                    List<BaseBeginLayoutAttribute> beginAttributes = new();
                    if (hasBeginHorizontal) beginAttributes.Add(beginHorizontal);
                    if (hasBeginVertical) beginAttributes.Add(beginVertical);
                    if (hasBeginFoldout) beginAttributes.Add(beginFoldoutAttribute);
                    if (hasBeginReadonly) beginAttributes.Add(beginReadonlyAttribute);

                    List<BaseEndLayoutAttribute> endAttributes = new();
                    if (hasEndReadonly) endAttributes.Add(endReadonlyAttribute);
                    if (hasEndFoldout) endAttributes.Add(endFoldoutAttribute);
                    if (hasEndVertical) endAttributes.Add(endVertical);
                    if (hasEndHorizontal) endAttributes.Add(endHorizontal);

                    beginAttributes = beginAttributes.OrderBy(attr => attr.order).ToList();
                    endAttributes =  endAttributes.OrderBy(attr => attr.order).ToList();

                    result = EditorGUI.EndChangeCheck();

                    for (int i = 0; i < beginAttributes.Count; i++)
                    {
                        var attr = beginAttributes[i];
                        s_beginActions[attr.GetType()]?.Invoke(attr);
                    }

                    EditorGUI.BeginChangeCheck();

                    using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                    {
                        if (!s_foldoutBlocks)
                        {
                            EditorGUILayout.PropertyField(iterator, true);
                        }
                    }

                    for (int i = 0; i < endAttributes.Count; i++)
                    {
                        var attr = endAttributes[i];
                        s_endActions[attr.GetType()]?.Invoke(attr);
                    }

                    enterChildren = false;
                }

                if (s_readonlyDifference != 0)
                    throw new Exception("BeginReadonlyGroup and EndReadonlyGroup attribures should match!");

                if (s_foldoutDifference != 0)
                    throw new Exception("BeginFoldoutGroup and EndFoldoutGroup attribures should match!");

                if (s_horizontalDifference != 0)
                    Debug.LogWarning("BeginHorizontal and EndHorizontal attributes do not match.");

                if (s_verticalDifference != 0)
                    Debug.LogWarning("BeginVertical and EndVertical attributes do not match.");

                serializedObject.ApplyModifiedProperties();
                if (EditorGUI.EndChangeCheck()) result = true;

                return result;
            }
#pragma warning restore CS0219 // Variable is assigned but its value is never used

            void DrawButtons()
            {
                IEnumerable<MethodInfo> methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (MethodInfo method in methods)
                {
                    if (method.IsGenericMethod) continue;
                    if (method.ContainsGenericParameters) continue;

                    ButtonAttribute buttonAttribute = Attribute.GetCustomAttribute(method, typeof(ButtonAttribute)) as ButtonAttribute;

                    if (buttonAttribute == null) continue;

                    var neededParameters = method.GetParameters();
                    if (neededParameters.Length == 0)
                    {
                        if (!GUILayout.Button(buttonAttribute.buttonText)) continue;
                        method.Invoke(target, buttonAttribute.parameters);
                    }

                    //else
                    //{
                    //    if (buttonAttribute.parameters.Length == 0) buttonAttribute.parameters = new object[neededParameters.Length];

                    //    GUILayout.BeginVertical(buttonAttribute.buttonText, "window");

                    //    neededParameters.ToList().ForEach(param =>
                    //    {
                    //        if (param.IsOut) return;

                    //        EditorGUILayout.PropertyField(param);
                    //    });

                    //    GUILayout.EndVertical();
                    //}
                }
            }
        }

        bool FindAttribute<T>(FieldInfo fieldInfo, out T result) where T : Attribute
        {
            result = fieldInfo.GetCustomAttribute<T>();

            if (result == null) return false;

            return true;
        }
    }
}
