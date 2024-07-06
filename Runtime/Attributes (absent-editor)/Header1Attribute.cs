using com.absence.attributes.internals;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a centered and underlined header.
    /// </summary>
    public sealed class Header1Attribute : BaseHeaderAttribute
    {
        public Header1Attribute(string headerText) : base(headerText, HeaderType.H1)
        {
        }
    }
}
