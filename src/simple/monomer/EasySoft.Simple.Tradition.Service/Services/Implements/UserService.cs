using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using EasySoft.Simple.Tradition.Service.Events;
using EasySoft.Simple.Tradition.Service.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Result.Implements;

namespace EasySoft.Simple.Tradition.Service.Services.Implements;

/// <summary>
/// UserService
/// </summary>
public class UserService : IUserService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<User> _userRepository;

    private readonly IRepository<Customer> _customerRepository;

    private readonly IRepository<Blog> _blogRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="userRepository"></param>
    /// <param name="customerRepository"></param>
    /// <param name="blogRepository"></param>
    public UserService(
        IEventPublisher eventPublisher,
        IRepository<User> userRepository,
        IRepository<Customer> customerRepository,
        IRepository<Blog> blogRepository
    )
    {
        _eventPublisher = eventPublisher;

        _userRepository = userRepository;
        _customerRepository = customerRepository;
        _blogRepository = blogRepository;
    }

    /// <summary>
    /// RegisterAsync
    /// </summary>
    /// <param name="registerDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ExecutiveResult<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        if (string.IsNullOrWhiteSpace(registerDto.LoginName))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("登陆名不能为空白"));

        if (string.IsNullOrWhiteSpace(registerDto.Password))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _userRepository.ExistAsync(o => o.LoginName == registerDto.LoginName);

        if (result.Success)
            return new ExecutiveResult<UserDto>(ReturnCode.NoChange.ToMessage("登录名已存在"));

        var user = EntityFactory.Create<User>();
        var customer = EntityFactory.Create<Customer>();
        var blog = EntityFactory.Create<Blog>();

        user.LoginName = registerDto.LoginName;
        user.Password = registerDto.Password.ToMd5();

        customer.UserId = user.Id;

        blog.UserId = user.Id;

        var resultAddUser = await _userRepository.AddAsync(user);

        if (!resultAddUser.Success)
            throw new Exception(resultAddUser.Message);

        var resultAddCustomer = await _customerRepository.AddAsync(customer);

        if (!resultAddCustomer.Success)
            throw new Exception(resultAddCustomer.Message);

        var resultAddBlog = await _blogRepository.AddAsync(blog);

        if (!resultAddBlog.Success)
            throw new Exception(resultAddBlog.Message);

        //发布注册成功事件
        var eventId = IdentifierAssist.Create();
        var eventData = new RegisterSuccessEvent.EventData()
        {
            UserId = user.Id
        };

        const string eventSource = nameof(RegisterAsync);

        await _eventPublisher.PublishAsync(new RegisterSuccessEvent(eventId, eventData, eventSource));

        return new ExecutiveResult<UserDto>(ReturnCode.Ok)
        {
            Data = resultAddUser.Data?.ToUserDto()
        };
    }

    /// <inheritdoc />
    public Task ProcessRegisterSuccessAsync(RegisterSuccessEvent registerSuccessEvent, IMessageTracker tracker)
    {
        LogAssist.Execute(nameof(ProcessRegisterSuccessAsync));

        return Task.CompletedTask;
    }

    /// <summary>
    /// RegisterMultiAsync
    /// </summary>
    /// <param name="namePassword"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ExecutiveResult> RegisterMultiAsync(Dictionary<string, string> namePassword)
    {
        if (namePassword.Count <= 0) return new ExecutiveResult(ReturnCode.NoChange.ToMessage("无可添加数据"));

        var users = new List<User>();
        var customers = new List<Customer>();
        var blogs = new List<Blog>();

        foreach (var item in namePassword)
        {
            var user = EntityFactory.Create<User>();
            var customer = EntityFactory.Create<Customer>();
            var blog = EntityFactory.Create<Blog>();

            user.LoginName = item.Key;
            user.Password = item.Value;

            customer.UserId = user.Id;

            blog.UserId = customer.Id;

            customers.Add(customer);
            users.Add(user);
            blogs.Add(blog);
        }

        var result = await _userRepository.AddRangeAsync(users);

        if (!result.Success)
            throw new Exception(result.Message);

        result = await _customerRepository.AddRangeAsync(customers);

        if (!result.Success)
            throw new Exception(result.Message);

        result = await _blogRepository.AddRangeAsync(blogs);

        if (!result.Success)
            throw new Exception(result.Message);

        return result;
    }

    /// <summary>
    /// SignInAsync
    /// </summary>
    /// <param name="signInDto"></param>
    /// <returns></returns>
    public async Task<ExecutiveResult<UserDto>> SignInAsync(SignInDto signInDto)
    {
        if (string.IsNullOrWhiteSpace(signInDto.LoginName))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("登录名不能为空白"));

        if (string.IsNullOrWhiteSpace(signInDto.Password))
            return new ExecutiveResult<UserDto>(ReturnCode.ParamError.ToMessage("密码不能为空白"));

        var result = await _userRepository.GetAsync(
            o => o.LoginName == signInDto.LoginName && o.Password == signInDto.Password.ToMd5()
        );

        if (result.Success && result.Data != null)
            return new ExecutiveResult<UserDto>(ReturnCode.Ok)
            {
                Data = result.Data.ToUserDto()
            };

        return new ExecutiveResult<UserDto>(result.Code);
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<long>> GetRoleGroupIdAsync(long userId)
    {
        if (userId <= 0) return new ExecutiveResult<long>(ReturnCode.NoData);

        var result = await _userRepository.GetAsync(userId);

        if (!result.Success || result.Data == null)
            return new ExecutiveResult<long>(ReturnCode.NoData);

        return new ExecutiveResult<long>(ReturnCode.Ok)
        {
            Data = result.Data.Id
        };
    }
}