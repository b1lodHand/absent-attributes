using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RequiredAttribute : PropertyAttribute
    {
        public RequiredAttribute() { }
    }
}