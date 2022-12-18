using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.UtilityTools.Standard.DataTransferObjects;

/// <inheritdoc />
public class AppPublicKeyDto : IAppPublicKey
{
    /// <inheritdoc />
    public string Key { get; set; } = "";
}