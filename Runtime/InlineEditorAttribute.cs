using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InlineEditorAttribute : Attribute
    {
        public int fieldButtonId = 0;

        public InlineEditorAttribute()
        {
        }
    }
}
