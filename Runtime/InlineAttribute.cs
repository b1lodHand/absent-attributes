using System;
using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Use to render the editor of a <see cref="UnityEngine.Object"/> sub-type, inside 
    /// the inspector of a <see cref="UnityEngine.Object"/> sub-type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class InlineAttribute : PropertyAttribute
    {
        /// <summary>
        /// If not-set or '0', no buttons will get drawn. Otherwise a field button with this id and the label: 
        /// 'New' will get drawn at the right side of the field marked with this attribute if 
        /// its value is null otherwise.
        /// </summary>
        public int newButtonId = 0;
        /// <summary>
        /// If not-set or '0', no buttons will get drawn. Otherwise a field button with this id and the label: 
        /// 'Del' will get drawn at the right side of the field marked with this attribute if 
        /// its value is not null.
        /// </summary>
        public int delButtonId = 0;

        public bool readonlyField = false;

        public InlineAttribute()
        {
        }
    }
}
