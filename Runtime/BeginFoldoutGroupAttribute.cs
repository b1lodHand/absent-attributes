using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BeginFoldoutGroupAttribute : BaseBeginLayoutAttribute
    {
        internal bool toggle = false;
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
