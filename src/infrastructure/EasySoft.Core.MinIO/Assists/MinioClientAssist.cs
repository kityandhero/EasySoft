using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;
using Minio;
using Minio.Exceptions;

namespace EasySoft.Core.MinIO.Assists;

public static class MinioClientAssist
{
    public static MinioClient GetMinioClient()
    {
        return AutofacAssist.Instance.Resolve<MinioClient>();
    }

    public static async Task<ExecutiveResult> PutObjectAsync(string bucketName, string objectName, string contentType)
    {
        try
        {
            var minioClient = AutofacAssist.Instance.Resolve<MinioClient>();

            // Make a bucket on the server, if not already present.
            var found = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

            if (!found) await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithFileName(objectName)
                .WithContentType(contentType);

            // Upload a file to bucket.
            await minioClient.PutObjectAsync(putObjectArgs);

            return ExecutiveResultAssist.CreateOk();
        }
        catch (MinioException e)
        {
            return new ExecutiveResult(ReturnCode.Exception.ToMessage(e.Message));
        }
    }
}