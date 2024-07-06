using com.absence.attributes.internals;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a centered header.
    /// </summary>
    public sealed class Header2Attribute : BaseHeaderAttribute
    {
        public Header2Attribute(string headerText) : base(headerText, HeaderType.H2)
        {
        }
    }
}
