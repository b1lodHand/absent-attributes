using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BeginHorizontalAttribute : BaseBeginLayoutAttribute
    {
        public BeginHorizontalAttribute() : base() 
        { 
        }

        public BeginHorizontalAttribute(string label, string style) : base(label, style) 
        { 
        }
    }
}
