using System;

namespace com.absence.attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FieldButtonIdAttribute : Attribute
    {
        public int id;

        public FieldButtonIdAttribute(int id)
        {
            this.id = id;
        }
    }
}
