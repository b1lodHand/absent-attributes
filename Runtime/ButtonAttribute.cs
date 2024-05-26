using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ButtonAttribute : PropertyAttribute
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
