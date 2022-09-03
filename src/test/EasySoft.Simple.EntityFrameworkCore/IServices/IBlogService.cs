using EasySoft.Core.Web.Framework.Services;
using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.IServices;

public interface IBlogService : IService
{
    Task<ExecutiveResult<Blog>> GetBlog(int authorId);
}