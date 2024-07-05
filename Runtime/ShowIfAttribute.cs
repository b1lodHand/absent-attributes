namespace com.absence.attributes
{
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
