using System;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RuntimeAttribute : PropertyAttribute
    {

    }
}
