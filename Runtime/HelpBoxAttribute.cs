using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a helpbox.
    /// </summary>
    public sealed class HelpBoxAttribute : PropertyAttribute
    {
        public string message { get; private set; }
        public HelpBoxType boxType { get; private set; }
        public float height { get; private set; }
        public bool initialized { get; private set; }

        public HelpBoxAttribute(string message, HelpBoxType boxType)
        {
            this.message = message;
            this.boxType = boxType;
            this.initialized = false;
        }

        public void SetHeight(float newHeight)
        {
            this.height = newHeight;
            this.initialized = true;
        }
    }

    public enum HelpBoxType
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
    }
}
