namespace com.absence.attributes
{
    /// <summary>
    /// Disables the specific field if the target property is true.
    /// </summary>
    public sealed class DisableIfAttribute : BaseIfAttribute
    {
        public DisableIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
            this.outputMethod = OutputMethod.EnableDisable;
            this.invert = false;
        }

        public DisableIfAttribute(string comparedPropertyName, object targetValue) : base(comparedPropertyName, targetValue)
        {
            this.outputMethod = OutputMethod.EnableDisable;
            this.invert = false;
        }
    }
}
