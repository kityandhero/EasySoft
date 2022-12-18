using EasySoft.Core.AppSecurityServer.Core.Entities;

namespace EasySoft.Core.AppSecurityServer.Core.EntityConfigures;

/// <summary>
/// 应用校验公钥持久化配置
/// </summary>
public class AppPublicKeyConfig : BaseEntityTypeConfiguration<AppPublicKey>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<AppPublicKey> builder, Type entityType)
    {
        builder.Property(x => x.Key)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CreateTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.ModifyTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);
    }
}