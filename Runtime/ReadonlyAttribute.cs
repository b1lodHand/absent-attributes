using System;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadonlyAttribute : PropertyAttribute
    {
        public ReadonlyAttribute() { }
    }
}
