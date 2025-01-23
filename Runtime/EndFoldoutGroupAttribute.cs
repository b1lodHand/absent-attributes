using com.absence.attributes.internals;
using System;

namespace com.absence.attributes.experimental
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EndFoldoutGroupAttribute : BaseEndLayoutAttribute
    {
        public EndFoldoutGroupAttribute() 
        { 
        }
    }
}
