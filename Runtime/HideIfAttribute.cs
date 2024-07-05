namespace com.absence.attributes
{
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