using EasySoft.UtilityTools.Core.ChangeTokens;
using EasySoft.UtilityTools.Core.ConfigurationProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.ConfigurationSources;

public delegate void JsonContentEventHandler();

public class JsonContentConfigurationSource : JsonContentConfigurationSourceCore
{
    private IContentChangeToken? _changeToken;

    private string _jsonContentPrev;

    private string _jsonContent;

    public event JsonContentEventHandler? OnJsonContentChanged;

    public JsonContentConfigurationSource()
    {
        _jsonContentPrev = "";
        _jsonContent = "";
    }

    public string GetJsonContent()
    {
        return _jsonContent;
    }

    public void SetJsonContent(string jsonContent)
    {
        _jsonContent = jsonContent;

        if (OnJsonContentChanged != null && _jsonContentPrev != jsonContent)
        {
            ExecJsonContentChanged();
        }

        _jsonContentPrev = jsonContent;
    }

    private void ExecJsonContentChanged()
    {
        OnJsonContentChanged?.Invoke();
    }

    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new JsonContentConfigurationProvider(this);
    }

    public IChangeToken Watch()
    {
        _changeToken = new ContentChangeToken();

        return _changeToken;
    }

    public void PrepareRefresh()
    {
        _changeToken?.PrepareRefresh();
    }
}