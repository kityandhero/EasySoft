using Microsoft.Extensions.Primitives;

namespace EasySoft.Configuration.ChangeTokens;

internal interface IContentChangeToken : IChangeToken
{
    public void PrepareRefresh();
}