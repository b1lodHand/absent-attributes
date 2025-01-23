using System;
using UnityEngine;

namespace com.absence.attributes
{
    public class ObjectFieldAttribute : PropertyAttribute
    {
        public Type Type;

        public ObjectFieldAttribute(Type type)
        {
            Type = type;
        }
    }
}
