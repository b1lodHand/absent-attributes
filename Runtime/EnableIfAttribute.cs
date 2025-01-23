using com.absence.attributes.internals;

namespace com.absence.attributes
{
    /// <summary>
    /// Enables the specific field if the target property is true.
    /// </summary>
    public sealed class EnableIfAttribute : BaseIfAttribute
    {
        public EnableIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
            this.outputMethod = OutputMethod.EnableDisable;
            this.invert = true;
        }

        public EnableIfAttribute(string comparedPropertyName, object targetValue) : base(comparedPropertyName, targetValue)
        {
            this.outputMethod = OutputMethod.EnableDisable;
            this.invert = true;
        }
    }
}
