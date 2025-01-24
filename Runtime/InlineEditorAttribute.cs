using System;

namespace com.absence.attributes.experimental
{
    /// <summary>
    /// Use to render the editor of a <see cref="UnityEngine.Object"/> sub-type, inside 
    /// the inspector of a <see cref="UnityEngine.Object"/> sub-type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InlineEditorAttribute : Attribute
    {
        /// <summary>
        /// If not-set or '0', no buttons will get drawn. A field button with this id and the label: 
        /// 'New' will get drawn at the right side of the field marked with this attribute otherwise.
        /// </summary>
        public int fieldButtonId = 0;

        public InlineEditorAttribute()
        {
        }
    }
}
