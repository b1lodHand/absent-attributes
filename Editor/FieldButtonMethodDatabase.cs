using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace com.absence.attributes.editor
{
    public static class FieldButtonMethodDatabase
    {
        public const BindingFlags FLAGS = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
        static Dictionary<int, MethodInfo> s_pairs;

        [InitializeOnLoadMethod]
        public static void Refresh()
        {
            s_pairs = new();

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            List<Type> allTypes = new();
            List<MethodInfo> temp = new();

            assemblies.ForEach(asm =>
            {
                List<Type> localTypes = asm.GetTypes().Where(t => t.IsClass).ToList();
                localTypes.ForEach(type => allTypes.Add(type));
            });

            allTypes.ForEach(type =>
            {
                List<MethodInfo> localMethods = type.GetMethods(FLAGS).Where(method =>
                {
                    return (!method.IsGenericMethod) && (method.GetCustomAttributes(typeof(FieldButtonIdAttribute), false).Length > 0);
                }).ToList();

                localMethods.ForEach(method => temp.Add(method));
            });

            if (temp.Count == 0) return;

            temp.ForEach(method =>
            {
                FieldButtonIdAttribute attribute = method.GetCustomAttribute<FieldButtonIdAttribute>();
                s_pairs.Add(attribute.id, method);
            });
        }

        public static bool Invoke(int id)
        {
            if (!s_pairs.ContainsKey(id)) return false;

            s_pairs[id].Invoke(null, null);
            return true;
        }

        [FieldButtonId(0)]
        static void NoMethodId()
        {
            Debug.Log("This is the default field button method (id = 0).");
        }
    }
}
