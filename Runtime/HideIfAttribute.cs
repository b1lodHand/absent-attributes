using UnityEngine;
using System;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class HideIfAttribute : PropertyAttribute
    {
        public string PropertyName { get; private set; }
        public object TargetValue { get; private set; }
        public bool DirectBool { get; private set; }
        public bool Invert { get; protected set; }

        public HideIfAttribute(string comparedPropertyName)
        {
            this.PropertyName = comparedPropertyName;
            this.TargetValue = null;

            Invert = false;
            DirectBool = true;
        }

        public HideIfAttribute(string comparedPropertyName, object targetValue)
        {
            this.PropertyName = comparedPropertyName;
            this.TargetValue = targetValue;

            Invert = false;
            DirectBool = false;
        }
    }
}