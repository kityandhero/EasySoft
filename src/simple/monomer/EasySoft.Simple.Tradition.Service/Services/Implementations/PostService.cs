using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

/// <summary>
/// PostService
/// </summary>
public class PostService : IPostService
{
    private readonly IRepository<Post> _postRepository;

    /// <summary>
    /// PostService
    /// </summary>
    /// <param name="postRepository"></param>
    public PostService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }
}