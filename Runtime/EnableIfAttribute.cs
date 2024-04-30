

namespace com.absence.attributes
{
    public class EnableIfAttribute : BaseIfAttribute
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
