using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class EndVerticalAttribute : BaseEndLayoutAttribute
    {
        public EndVerticalAttribute() 
        {
        }

        public EndVerticalAttribute(string helper) : base(helper)
        {
        }
    }
}
