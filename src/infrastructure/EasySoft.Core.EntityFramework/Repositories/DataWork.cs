using System.Data.Common;
using EasySoft.Core.EntityFramework.InterFaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasySoft.Core.EntityFramework.Repositories;

public class DataWork : IDataWork
{
    private readonly DbContext _context;

    public DataWork(DbContext context)
    {
        _context = context;
    }

    public DbTransaction BeginTransaction()
    {
        _context.Database.BeginTransaction();

        if (_context.Database.CurrentTransaction == null)
        {
            throw new Exception("context disallow null");
        }

        return _context.Database.CurrentTransaction.GetDbTransaction();
    }

    public void CommitTransaction()
    {
        _context.SaveChanges();
        _context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _context.Database.RollbackTransaction();
    }
}