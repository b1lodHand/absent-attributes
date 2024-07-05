using System;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class RuntimeAttribute : PropertyAttribute
    {
        public RuntimeAttribute() { }
    }
}
