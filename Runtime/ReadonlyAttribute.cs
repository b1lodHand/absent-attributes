using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Makes the specific field readonly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadonlyAttribute : PropertyAttribute
    {
        public ReadonlyAttribute() { }
    }
}
