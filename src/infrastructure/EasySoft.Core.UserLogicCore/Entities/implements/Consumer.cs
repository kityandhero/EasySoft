using EasySoft.Core.UserLogicCore.Entities.Interfaces;

namespace EasySoft.Core.UserLogicCore.Entities.implements;

/// <inheritdoc />
public class Consumer : IConsumer
{
    private Whether _whetherSuper = Whether.No;

    /// <inheritdoc />
    public string AccountName { get; set; } = "";

    /// <summary>
    /// GetWhetherSuper
    /// </summary>
    /// <returns></returns>
    public bool GetWhetherSuper()
    {
        return _whetherSuper == Whether.Yes;
    }

    /// <summary>
    /// SetWhetherSuper
    /// </summary>
    /// <param name="whether"></param>
    /// <returns></returns>
    internal Consumer SetWhetherSuper(Whether whether)
    {
        _whetherSuper = whether;

        return this;
    }
}