namespace EasySoft.Core.AppSecurityServer.Core.DataTransferObjects;

/// <inheritdoc />
public class AppPublicKeyDto : IAppPublicKey
{
    /// <inheritdoc />
    public string Key { get; set; } = "";
}