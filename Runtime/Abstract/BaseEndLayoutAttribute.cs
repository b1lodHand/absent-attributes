using System;

namespace com.absence.attributes.internals
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BaseEndLayoutAttribute : Attribute
    {
        public int order = 0;

        public BaseEndLayoutAttribute()
        {
        }
    }
}
