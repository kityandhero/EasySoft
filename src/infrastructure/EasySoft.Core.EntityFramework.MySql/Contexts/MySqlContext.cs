﻿using System.Drawing;
using EasySoft.Core.EntityFramework.Contexts.Basic;
using EasySoft.Core.EntityFramework.EntityConfigures.Interfaces;
using EasySoft.Core.Infrastructure.Operations.Interfaces;

namespace EasySoft.Core.EntityFramework.MySql.Contexts;

public class MySqlContext : BasicContext
{
    public MySqlContext(DbContextOptions options, IEntityConfigure entityConfigure) : base(
        options,
        entityConfigure
    )
    {
    }

    protected override void OnAdvanceModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasCharSet("utf8mb4 ");
    }
}