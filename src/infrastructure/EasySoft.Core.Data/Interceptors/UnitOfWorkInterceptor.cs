namespace EasySoft.Core.Data.Interceptors;

/// <summary>
///     工作单元拦截器
/// </summary>
public sealed class UnitOfWorkInterceptor : IInterceptor
{
    private readonly UnitOfWorkAsyncInterceptor _unitOfWorkAsyncInterceptor;

    public UnitOfWorkInterceptor(UnitOfWorkAsyncInterceptor unitOfWorkAsyncInterceptor)
    {
        _unitOfWorkAsyncInterceptor = unitOfWorkAsyncInterceptor;
    }

    public void Intercept(IInvocation invocation)
    {
        _unitOfWorkAsyncInterceptor.ToInterceptor().Intercept(invocation);
    }
}