using EasySoft.Core.LogServer.Core.Entities;

namespace EasySoft.Core.LogServer.Core.EntityConfigures;

/// <summary>
/// ErrorLogConfig
/// </summary>
public class ErrorLogConfig : BaseEntityTypeConfiguration<ErrorLog>
{
    /// <inheritdoc />
    protected override void ConfigureColumn(EntityTypeBuilder<ErrorLog> builder, Type entityType)
    {
        builder.Property(x => x.UserId)
            .HasDefaultValue(0);

        builder.Property(x => x.Url)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(500)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Message)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.StackTrace)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Source)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Scene)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Type)
            .HasDefaultValue(0);

        builder.Property(x => x.Degree)
            .HasDefaultValue(0);

        builder.Property(x => x.Header)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.UrlParams)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.PayloadParams)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.FormParams)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Host)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Port)
            .HasDefaultValue(0);

        builder.Property(x => x.CustomLog)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(400)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CustomData)
            .HasColumnType(DatabaseConstant.NvarcharMax)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CustomDataType)
            .HasDefaultValue(0);

        builder.Property(x => x.ExceptionTypeName)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.ExceptionTypeFullName)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(200)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.Channel)
            .HasDefaultValue(0);

        builder.Property(x => x.Status)
            .HasDefaultValue(0);

        builder.Property(x => x.Ip)
            .HasColumnType(DatabaseConstant.Nvarchar)
            .HasMaxLength(40)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.CreateBy)
            .HasDefaultValue(0L);

        builder.Property(x => x.CreateTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);

        builder.Property(x => x.ModifyBy)
            .HasDefaultValue(0L);

        builder.Property(x => x.ModifyTime)
            .HasDefaultValue(ConstCollection.DbDefaultDateTime);
    }
}