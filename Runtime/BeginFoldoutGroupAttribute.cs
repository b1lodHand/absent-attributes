using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BeginFoldoutGroupAttribute : BaseBeginLayoutAttribute
    {
        public bool toggleOnLabelClick = true;

        public BeginFoldoutGroupAttribute() : base()
        {
            this.label = "Foldout Group";
        }

        public BeginFoldoutGroupAttribute(string label) : base(label, null)
        {
            this.label = label;
        }
    }
}
