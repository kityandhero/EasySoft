using System.Threading;
using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.ChangeTokens;

internal interface IContentChangeToken : IChangeToken
{
    CancellationTokenSource CancellationTokenSource { get; }
}