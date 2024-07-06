using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Makes the specific field to be visible only in play-mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class RuntimeAttribute : PropertyAttribute
    {
        public RuntimeAttribute() { }
    }
}
