using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class EndReadonlyGroupAttribute : BaseEndLayoutAttribute
    {
        public EndReadonlyGroupAttribute() 
        { 
        }

        public EndReadonlyGroupAttribute(string helper) : base(helper)
        {
        }
    }
}
