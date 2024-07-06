using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Shows a error box if the specific nullable field is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class RequiredAttribute : PropertyAttribute
    {
        public RequiredAttribute() { }
    }
}