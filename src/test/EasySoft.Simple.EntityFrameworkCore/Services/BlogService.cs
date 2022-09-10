﻿using EasySoft.Simple.EntityFrameworkCore.Entities;
using EasySoft.Simple.EntityFrameworkCore.IRepositories;
using EasySoft.Simple.EntityFrameworkCore.IServices;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.EntityFrameworkCore.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public Task<ExecutiveResult<Blog>> GetBlog(int authorId)
    {
        return _blogRepository.GetBlog(authorId);
    }
}