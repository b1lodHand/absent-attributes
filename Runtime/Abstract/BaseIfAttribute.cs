using System;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class BaseIfAttribute : PropertyAttribute
    {
        public enum OutputMethod
        {
            ShowHide = 0,
            EnableDisable = 1,
        }

        public string controlPropertyName { get; private set; }
        public object targetValue { get; private set; }
        public bool directBool { get; private set; }
        public bool invert { get; protected set; }
        public OutputMethod outputMethod { get; protected set; }

        public BaseIfAttribute(string comparedPropertyName)
        {
            this.controlPropertyName = comparedPropertyName;
            this.targetValue = null;

            directBool = true;
        }

        public BaseIfAttribute(string comparedPropertyName, object targetValue)
        {
            this.controlPropertyName = comparedPropertyName;
            this.targetValue = targetValue;

            directBool = false;
        }
    }
}
