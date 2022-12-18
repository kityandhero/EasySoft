using EasySoft.UtilityTools.Standard.Entity.Interfaces;

namespace EasySoft.UtilityTools.Standard.DataTransferObjects;

/// <inheritdoc />
public class AppPublicKeyDto : IAppPublicKey
{
    /// <inheritdoc />
    public string Key { get; set; } = "";
}