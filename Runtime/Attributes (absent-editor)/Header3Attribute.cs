using com.absence.attributes.internals;

namespace com.absence.attributes
{
    /// <summary>
    /// Creates a centered, small header.
    /// </summary>
    public sealed class Header3Attribute : BaseHeaderAttribute
    {
        public Header3Attribute(string headerText) : base(headerText, HeaderType.H3)
        {
        }
    }
}
