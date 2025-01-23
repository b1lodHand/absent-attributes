using System;

namespace com.absence.attributes.internals
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class BaseBeginLayoutAttribute : Attribute
    {
        public string label;
        public string style;
        public int order = 0;

        public BaseBeginLayoutAttribute()
        {
        }

        public BaseBeginLayoutAttribute(string label, string style)
        {
            this.label = label;
            this.style = style;
        }
    }
}
