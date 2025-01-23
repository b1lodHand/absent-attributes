using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public class BeginReadonlyGroupAttribute : BaseBeginLayoutAttribute
    {
        public BeginReadonlyGroupAttribute() : base()
        {
            this.label = "Foldout Group";
        }

        public BeginReadonlyGroupAttribute(string label) : base(label, null)
        {
            this.label = label;
        }
    }
}
