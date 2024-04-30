using System;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadonlyAttribute : PropertyAttribute
    {
        public ReadonlyAttribute() { }
    }
}
