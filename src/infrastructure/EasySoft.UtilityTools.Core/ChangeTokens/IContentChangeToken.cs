using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.ChangeTokens;

internal interface IContentChangeToken : IChangeToken
{
    public void PrepareRefresh();
}