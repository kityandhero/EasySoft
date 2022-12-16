﻿using EasySoft.Core.PermissionServer.Core.Entities;

namespace EasySoft.Core.PermissionServer.Core.EntityConfigures.Items;

public class AccessWayConfig : BaseEntityTypeConfiguration<AccessWay>
{
    protected override void ConfigureColumn(EntityTypeBuilder<AccessWay> builder, Type entityType)
    {
        builder.Property(x => x.Name)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.GuidTag)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.RelativePath)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Expand)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Channel)
            .HasDefaultValue(0);

        builder.Property(x => x.Status)
            .HasDefaultValue(0);

        builder.Property(x => x.Ip)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(40)
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