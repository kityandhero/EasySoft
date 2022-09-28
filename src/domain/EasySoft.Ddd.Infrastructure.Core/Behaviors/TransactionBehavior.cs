using EasySoft.Domain.Infrastructure.Core.Contexts;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EasySoft.Domain.Infrastructure.Core.Behaviors
{
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TDbContext : AdvanceContext
        where TRequest : IRequest<TResponse>
    {
        ILogger _logger;
        TDbContext _dbContext;
        ICapPublisher _capBus;

        public TransactionBehavior(TDbContext dbContext, ILogger logger, ICapPublisher capBus)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _capBus = capBus ?? throw new ArgumentNullException(nameof(capBus));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                // 定义数据路操作执行的策略
                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.BeginTransactionAsync(_capBus))
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId,
                            typeName, request);

                        response = await next();

                        _logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId,
                            typeName);

                        await _dbContext.CommitAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}