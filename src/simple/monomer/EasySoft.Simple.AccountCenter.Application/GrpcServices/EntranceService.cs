using EasySoft.Simple.AccountCenter.Application.Contracts.DataTransferObjects.ApiParams;
using EasySoft.Simple.AccountCenter.Application.Contracts.Services;
using EntranceService;
using Grpc.Core;

namespace EasySoft.Simple.AccountCenter.Application.GrpcServices;

public class EntranceService : Entrance.EntranceBase
{
    private readonly IUserService _userService;

    public EntranceService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<RegisterReply> Register(RegisterRequest request, ServerCallContext context)
    {
        var registerDto = new RegisterDto
        {
            LoginName = request.LoginName,
            Password = request.Password
        };

        var result = await _userService.RegisterAsync(registerDto);

        var registerReply = new RegisterReply
        {
            Success = result.Success,
            Code = result.Code.Code,
            Message = result.Message
        };

        return registerReply;
    }

    public override async Task<SignInReply> SignIn(SignInRequest request, ServerCallContext context)
    {
        var signInDto = new SignInDto
        {
            LoginName = request.LoginName,
            Password = request.Password
        };

        var result = await _userService.SignInAsync(signInDto);

        var signInReply = new SignInReply
        {
            Success = result.Success,
            Code = result.Code.Code,
            Message = result.Message
        };

        return signInReply;
    }
}