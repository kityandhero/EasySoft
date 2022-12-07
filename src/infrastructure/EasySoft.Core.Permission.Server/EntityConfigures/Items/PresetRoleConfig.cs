﻿using EasySoft.Core.Permission.Server.Entities;

namespace EasySoft.Core.Permission.Server.EntityConfigures.Items;

public class PresetRoleConfig : BaseEntityTypeConfiguration<PresetRole>
{
    protected override void ConfigureColumn(EntityTypeBuilder<PresetRole> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Description)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Content)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.ModuleCount)
            .HasDefaultValue(0);

        builder.Property(x => x.Competence)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.WhetherSuper)
            .HasDefaultValue(0);

        builder.Property(x => x.Status)
            .HasDefaultValue(0);

        builder.Property(x => x.Ip)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CreateUserId)
            .HasDefaultValue(0L);

        builder.Property(x => x.CreateTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.UpdateUserId)
            .HasDefaultValue(0L);

        builder.Property(x => x.UpdateTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);
    }
}