using UnityEngine;

namespace com.absence.attributes.internals
{
    public abstract class BaseHeaderAttribute : PropertyAttribute
    {
        public enum HeaderType
        {
            H1 = 0,
            H2 = 1,
            H3 = 2,
        }

        public string headerText { get; private set; }
        public HeaderType headerType { get; private set; } 

        public BaseHeaderAttribute(string headerText, HeaderType headerType)
        {
            this.headerText = headerText;
            this.headerType = headerType;
        }
    }
}
