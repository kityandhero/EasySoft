namespace EasySoft.UtilityTools.Standard.Entities.Interfaces;

/// <summary>
/// app public key
/// [应用全局校验公钥]
/// </summary>
public interface IAppPublicKey
{
    /// <summary>
    /// key, 验证公钥 
    /// </summary>
    [Description("Key")]
    string Key { get; set; }
}