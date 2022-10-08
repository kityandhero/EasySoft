using EasySoft.Core.EntityFramework.Contexts.Basic;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Domain.Infrastructure.Core.Contexts;

public class BaseContext : BasicContext
{
    public BaseContext(
        DbContextOptions options
    ) : base(options)
    {
    }
}