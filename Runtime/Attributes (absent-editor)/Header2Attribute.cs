using com.absence.attributes.internals;

namespace com.absence.attributes
{
    public sealed class Header2Attribute : BaseHeaderAttribute
    {
        public Header2Attribute(string headerText) : base(headerText, HeaderType.H2)
        {
        }
    }
}
