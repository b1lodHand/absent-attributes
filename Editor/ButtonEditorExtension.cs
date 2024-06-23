using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class ButtonEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

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
}
