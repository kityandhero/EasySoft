namespace EasySoft.Core.PermissionVerification.Attributes
{
    /// <summary>
    /// GuidTagAttribute
    /// </summary>
    public class GuidTagAttribute : Attribute
    {
        #region Properties

        public string GuidTag { get; }

        #endregion Properties

        public GuidTagAttribute()
            : this("")
        {
        }

        /// <summary>
        /// GuidTagAttribute
        /// </summary>
        /// <param name="guidTag"></param>
        public GuidTagAttribute(string guidTag)
        {
            GuidTag = guidTag;
        }
    }
}