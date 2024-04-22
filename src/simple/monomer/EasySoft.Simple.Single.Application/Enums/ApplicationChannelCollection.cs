using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Simple.Single.Application.Enums;

/// <summary>
///     ApplicationChannelCollection
/// </summary>
public abstract class ApplicationChannelCollection
{
    /// <summary>
    /// TestApplication
    /// </summary>
    public static readonly Channel TestApplication = new(
        "32a7030c689c42a097513e653a83c1c2",
        "TestApplication",
        "TestApplication"
    );
}