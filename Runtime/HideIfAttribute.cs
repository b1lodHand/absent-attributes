namespace com.absence.attributes
{
    /// <summary>
    /// Hides the specific field if the target property is true.
    /// </summary>
    public sealed class HideIfAttribute : BaseIfAttribute
    {
        public HideIfAttribute(string comparedPropertyName) : base(comparedPropertyName)
        {
            this.outputMethod = OutputMethod.ShowHide;
            this.invert = false;
        }

        public HideIfAttribute(string comparedPropertyName, object targetValue) : base(comparedPropertyName, targetValue)
        {
            this.outputMethod = OutputMethod.ShowHide;
            this.invert = false;
        }
    }
}