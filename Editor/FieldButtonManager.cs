using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace com.absence.attributes.editor
{
    /// <summary>
    /// The static class responsible for managing anything about field buttons.
    /// </summary>
    public static class FieldButtonManager
    {
        public const bool DEBUG_MODE = false;
        public const BindingFlags FLAGS = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

        static Dictionary<int, MethodInfo> s_pairs;

        #region Public API

        /// <summary>
        /// Use to refresh 'n get all methods marked with the <see cref="FieldButtonIdAttribute"/>.
        /// Automatically gets called when editor initializes.
        /// </summary>
        public static void Refresh(bool debugMode = DEBUG_MODE)
        {
            s_pairs = new();

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> allTypes = new();
            List<(FieldButtonIdAttribute attr, MethodInfo method)> temp = new();

            assemblies.ForEach(asm =>
            {
                List<Type> localTypes = asm.GetTypes().Where(t => t.IsClass).ToList();
                localTypes.ForEach(type => allTypes.Add(type));
            });

            allTypes.ForEach(type =>
            {
                List<MethodInfo> localMethods = type.GetMethods(FLAGS).ToList();

                localMethods.ForEach(method =>
                {
                    if (method.IsGenericMethod) return;
                    
                    List<FieldButtonIdAttribute> attributes = 
                        method.GetCustomAttributes(typeof(FieldButtonIdAttribute), true).ToList().
                        ConvertAll(obj => obj as FieldButtonIdAttribute);

                    if (attributes == null) return;

                    attributes.ForEach(attr =>
                    {
                        temp.Add((attr, method));
                    });
                });
            });

            if (temp.Count == 0) return;

            List<(FieldButtonIdAttribute attr, MethodInfo method)> orderedAttributes = 
                temp.OrderByDescending(tuple => tuple.attr.priority).ToList();

            for (int i = 0; i < orderedAttributes.Count; i++)
            {
                FieldButtonIdAttribute attr = orderedAttributes[i].attr;
                MethodInfo method = orderedAttributes[i].method;

                if (!s_pairs.ContainsKey(attr.id))
                    s_pairs.Add(attr.id, method);
            }

            if (!debugMode) return;

            StringBuilder sb = new("<b>[ATTRIBUTES] Found FieldButtonId Methods:</b>");

            foreach (KeyValuePair<int, MethodInfo> kvp in s_pairs) 
            {
                sb.Append($"\n\t{kvp.Key} => {kvp.Value.Name}");
            }

            Debug.Log(sb.ToString());
        }

        /// <summary>
        /// Use to invoke a method marked with <see cref="FieldButtonIdAttribute"/> with the corresponding id, if exists.
        /// </summary>
        /// <param name="id">The target id.</param>
        /// <returns>Return false if there are no methods with the corresponding id, true otherwise.</returns>
        public static bool Invoke(int id)
        {
            return Invoke(id, out _);
        }

        /// <summary>
        /// Use to invoke a method marked with <see cref="FieldButtonIdAttribute"/> with the corresponding id, if exists.
        /// </summary>
        /// <param name="id">The target id.</param>
        /// <param name="output">Outputs null if this method returns false. Output of the called static method otherwise.</param>
        /// <returns>Returns false if anything goes wrong, true otherwise.</returns>
        public static bool Invoke(int id, out object output)
        {
            output = null;
            if (!s_pairs.ContainsKey(id)) return false;

            output = s_pairs[id].Invoke(null, null);
            return true;
        }

        /// <summary>
        /// Use to draw a button with IMGUI in editor that invokes a field button method.
        /// </summary>
        /// <param name="id">The target id.</param>
        /// <param name="content">The GUIContent of button. Required.</param>
        /// <param name="style">The GUIStyle of button. Optional, leave null if you will use the default style.</param>
        /// <param name="options">The GUILayout options.</param>
        /// <returns>Returns false if anything goes wrong or if simply the button does not get pressed, true otherwise.</returns>
        public static bool ButtonGUI(int id, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            return ButtonGUI(id, content, style, out _, options);
        }

        /// <summary>
        /// Use to draw a button with IMGUI in editor that invokes a field button method.
        /// </summary>
        /// <param name="id">The target id.</param>
        /// <param name="content">The GUIContent of button. Required.</param>
        /// <param name="style">The GUIStyle of button. Optional, leave null if you will use the default style.</param>
        /// <param name="output">The GUIStyle of button. Optional, leave null if you will use the default style.</param>
        /// <param name="output">Outputs null if this method returns false. Output of the called static method otherwise.</param>
        /// <returns>Returns false if anything goes wrong or if simply the button does not get pressed, true otherwise.</returns>
        public static bool ButtonGUI(int id, GUIContent content, GUIStyle style, out object output, params GUILayoutOption[] options)
        {
            output = null;

            bool pressed = DrawButton(content, style, options);

            if (!pressed) 
                return false;

            bool result = Invoke(id, out output);

            if (!result)
                Debug.Log($"There are no actions associated with this button at the moment (id = {id}). Create one with 'FieldButtonId' attribute.");

            return result;
        }

        #endregion

        #region Internal

        static bool DrawButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            bool hasContent = content != null;

            if (!hasContent)
                throw new Exception("Button can not get drawn without content!");

            bool pressed;
            bool hasStyle = style != null;

            if (hasStyle) pressed = GUILayout.Button(content, style, options);
            else pressed = GUILayout.Button(content, options);

            return pressed;
        }

        #endregion

        #region Built-in Methods

        [FieldButtonId(0, priority = int.MaxValue)]
        static void NullId()
        {
            Debug.Log("This is the default field button method (id = 0).");
        }

        #endregion
    }
}
