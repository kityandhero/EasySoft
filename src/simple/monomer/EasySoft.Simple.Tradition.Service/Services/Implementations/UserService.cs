using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.DataTransferObjects.ApiParams;
using EasySoft.Simple.Tradition.Service.ExtensionMethods;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

/// <summary>
/// UserService
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRepository<User> _userRepository;

    private readonly IRepository<Customer> _customerRepository;

    private readonly IRepository<Blog> _blogRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="userRepository"></param>
    /// <param name="customerRepository"></param>
    /// <param name="blogRepository"></param>
    public UserService(
        IUnitOfWork unitOfWork,
        IRepository<User> userRepository,
        IRepository<Customer> customerRepository,
        IRepository<Blog> blogRepository
    )
    {
        _unitOfWork = unitOfWork;
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

        try
        {
            var user = EntityFactory.Create<User>();
            var customer = EntityFactory.Create<Customer>();
            var blog = EntityFactory.Create<Blog>();

            user.LoginName = registerDto.LoginName;
            user.Password = registerDto.Password.ToMd5();

            customer.UserId = user.Id;

            blog.UserId = user.Id;

            var resultAddUser = await _userRepository.CreateAsync(user);

            if (!resultAddUser.Success)
                throw new Exception(resultAddUser.Message);

            var resultAddCustomer = await _customerRepository.CreateAsync(customer);

            if (!resultAddCustomer.Success)
                throw new Exception(resultAddCustomer.Message);

            var resultAddBlog = await _blogRepository.CreateAsync(blog);

            if (!resultAddBlog.Success)
                throw new Exception(resultAddBlog.Message);

            await _unitOfWork.CommitAsync();

            return new ExecutiveResult<UserDto>(ReturnCode.Ok)
            {
                Data = resultAddUser.Data?.ToUserDto()
            };
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return new ExecutiveResult<UserDto>(ReturnMessage.Exception.ToMessage(ex.Message));
        }
        finally
        {
            _unitOfWork.Dispose();
        }
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

        try
        {
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

            var result = await _userRepository.CreateRangeAsync(users);

            if (!result.Success)
                throw new Exception(result.Message);

            result = await _customerRepository.CreateRangeAsync(customers);

            if (!result.Success)
                throw new Exception(result.Message);

            result = await _blogRepository.CreateRangeAsync(blogs);

            if (!result.Success)
                throw new Exception(result.Message);

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return new ExecutiveResult(ReturnMessage.Exception.ToMessage(ex.Message));
        }
        finally
        {
            _unitOfWork.Dispose();
        }
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
}