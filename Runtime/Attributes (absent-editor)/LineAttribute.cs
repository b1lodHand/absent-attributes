using UnityEngine;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a horizontal line.
    /// </summary>
    public sealed class LineAttribute : PropertyAttribute
    {
        public bool colorSet {  get; set; }
        public Color color { get; set; }

        public LineAttribute()
        {
            colorSet = false;
        }

        public LineAttribute(float r, float g, float b, float a = 1f)
        {
            colorSet = true;
            color = new Color(r, g, b, a);
        }
    }
}
