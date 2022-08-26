using Microsoft.Extensions.Primitives;

namespace EasySoft.Configuration.Providers;

public interface IContentProvider
{
    IChangeToken Watch(string filter);
}