namespace EasySoft.Core.Data.Interceptors;

/// <summary>
///     工作单元拦截器
/// </summary>
public sealed class UnitOfWorkInterceptor : IInterceptor
{
    private readonly UnitOfWorkAsyncInterceptor _unitOfWorkAsyncInterceptor;

    /// <summary>
    /// 工作单元拦截器
    /// </summary>
    /// <param name="unitOfWorkAsyncInterceptor"></param>
    public UnitOfWorkInterceptor(UnitOfWorkAsyncInterceptor unitOfWorkAsyncInterceptor)
    {
        _unitOfWorkAsyncInterceptor = unitOfWorkAsyncInterceptor;
    }

    /// <summary>
    /// 拦截
    /// </summary>
    /// <param name="invocation"></param>
    public void Intercept(IInvocation invocation)
    {
        _unitOfWorkAsyncInterceptor.ToInterceptor().Intercept(invocation);
    }
}