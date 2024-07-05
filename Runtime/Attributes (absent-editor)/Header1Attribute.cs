using com.absence.attributes.internals;

namespace com.absence.attributes
{
    public sealed class Header1Attribute : BaseHeaderAttribute
    {
        public Header1Attribute(string headerText) : base(headerText, HeaderType.H1)
        {
        }
    }
}
