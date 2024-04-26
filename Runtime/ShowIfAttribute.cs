using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.attributes
{
    public class ShowIfAttribute : HideIfAttribute
    {
        public ShowIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
            Invert = true;
        }

        public ShowIfAttribute(string comparedPropertyName, object targetValue) : base(comparedPropertyName, targetValue)
        {
            Invert = true;
        }
    }
}
