using com.absence.attributes.internals;

namespace com.absence.attributes
{
    /// <summary>
    /// Shows the specific field if the target property is true.
    /// </summary>
    public sealed class ShowIfAttribute : BaseIfAttribute
    {
        public ShowIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
            this.outputMethod = OutputMethod.ShowHide;
            this.invert = true;
        }

        public ShowIfAttribute(string comparedPropertyName, object targetValue) : base(comparedPropertyName, targetValue)
        {
            this.outputMethod = OutputMethod.ShowHide;
            this.invert = true;
        }
    }
}
