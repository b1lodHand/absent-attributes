using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BeginVerticalAttribute : BaseBeginLayoutAttribute
    {
        public BeginVerticalAttribute() : base()
        {
        }

        public BeginVerticalAttribute(string label, string style) : base(label, style) 
        {
        }
    }
}
