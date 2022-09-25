﻿using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.InterFaces;

public interface IAdvanceTransaction
{
    IDbContextTransaction GetCurrentTransaction();

    bool HasActiveTransaction { get; }

    Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus);

    Task CommitTransactionAsync(IDbContextTransaction transaction);

    void RollbackTransaction();
}