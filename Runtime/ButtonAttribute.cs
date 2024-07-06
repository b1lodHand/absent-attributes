using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a button that invokes the specific method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ButtonAttribute : PropertyAttribute
    {
        public string buttonText { get; private set; }
        public object[] parameters { get; set; }

        public ButtonAttribute(string buttonText, params object[] parameters)
        {
            this.buttonText = buttonText;
            this.parameters = parameters;
        }
    }
}
