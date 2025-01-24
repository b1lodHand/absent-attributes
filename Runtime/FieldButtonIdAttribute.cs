using System;

namespace com.absence.attributes
{
    /// <summary>
    /// Use to mark a static method as 'field button' with an id. Then you can use it like this (in editor):
    /// <code>
    /// // only tries to invoke the method.
    /// bool success = FieldButtonManager.Invoke(id); 
    /// 
    /// or
    /// 
    /// // draws a button (IMGUI) that tries to invoke when pressed. 'output' is the output of your static method if its not 'void'.
    /// bool success = FieldButtonManager.ButtonGUI(id, content, style, out object output, GUILayout.Width(50f));
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FieldButtonIdAttribute : Attribute
    {
        public int id;
        public int priority = 0;

        public FieldButtonIdAttribute(int id)
        {
            this.id = id;
        }
    }
}
