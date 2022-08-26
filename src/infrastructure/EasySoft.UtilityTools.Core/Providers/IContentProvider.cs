using Microsoft.Extensions.Primitives;

namespace EasySoft.UtilityTools.Core.Providers;

public interface IContentProvider
{
    IChangeToken Watch(string filter);
}