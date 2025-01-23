using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class EndHorizontalAttribute : BaseEndLayoutAttribute
    {
        public EndHorizontalAttribute() 
        {
        }

        public EndHorizontalAttribute(string helper) : base(helper)
        {
        }
    }
}
