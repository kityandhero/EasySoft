using EasySoft.UtilityTools.Core.ChangeTokens;
using EasySoft.UtilityTools.Core.ConfigurationProviders;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

/// <summary>
/// JsonContentEventHandler
/// </summary>
public delegate void JsonContentEventHandler();

/// <summary>
/// JsonContentConfigurationSource
/// </summary>
public class JsonContentConfigurationSource : JsonContentConfigurationSourceCore
{
    private IContentChangeToken? _changeToken;

    private string _jsonContentPrev;

    private string _jsonContent;

    /// <summary>
    /// OnJsonContentChanged
    /// </summary>
    public event JsonContentEventHandler? OnJsonContentChanged;

    /// <summary>
    /// JsonContentConfigurationSource
    /// </summary>
    public JsonContentConfigurationSource()
    {
        _jsonContentPrev = "";
        _jsonContent = "";
    }

    /// <summary>
    /// GetJsonContent
    /// </summary>
    /// <returns></returns>
    public string GetJsonContent()
    {
        return _jsonContent;
    }

    /// <summary>
    /// SetJsonContent
    /// </summary>
    /// <param name="jsonContent"></param>
    public void SetJsonContent(string jsonContent)
    {
        _jsonContent = jsonContent;

        if (OnJsonContentChanged != null && _jsonContentPrev != jsonContent) ExecJsonContentChanged();

        _jsonContentPrev = jsonContent;
    }

    private void ExecJsonContentChanged()
    {
        OnJsonContentChanged?.Invoke();
    }

    /// <summary>
    /// Build
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new JsonContentConfigurationProvider(this);
    }

    /// <summary>
    /// Watch
    /// </summary>
    /// <returns></returns>
    public IChangeToken Watch()
    {
        _changeToken = new ContentChangeToken();

        return _changeToken;
    }

    /// <summary>
    /// PrepareRefresh
    /// </summary>
    public void PrepareRefresh()
    {
        _changeToken?.PrepareRefresh();
    }
}