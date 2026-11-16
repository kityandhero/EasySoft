namespace EasySoft.Core.Refit.Assists;

/// <summary>
/// RefitConfigureAssist
/// </summary>
public class RefitConfigureAssist
{
    #region Fields

    /// <summary>
    /// RefitConfigureNode
    /// </summary>
    private const string RefitConfigureNode = "Refit";

    #endregion

    #region Properties

    /// <summary>
    /// 配置集
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// 基础配置
    /// </summary>
    /// <param name="configuration"></param>
    public RefitConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #endregion
}