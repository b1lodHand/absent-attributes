using com.absence.attributes.internals;

namespace com.absence.attributes
{
    public sealed class Header3Attribute : BaseHeaderAttribute
    {
        public Header3Attribute(string headerText) : base(headerText, HeaderType.H3)
        {
        }
    }
}
