using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class EndFoldoutGroupAttribute : BaseEndLayoutAttribute
    {
        public EndFoldoutGroupAttribute() 
        { 
        }

        public EndFoldoutGroupAttribute(string helper) : base(helper)
        {
        }
    }
}
