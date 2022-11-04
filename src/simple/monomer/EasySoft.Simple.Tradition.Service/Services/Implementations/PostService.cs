using EasySoft.Core.Data.Repositories;
using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

public class PostService : IPostService
{
    private readonly IRepository<Post> _postRepository;

    public PostService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }
}