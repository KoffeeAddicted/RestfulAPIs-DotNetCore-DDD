using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Contracts.Helpers;

public static class AWSHelper
{
    public static async Task<String> UploadStreamToS3(AppSettings appSettings, Stream stream)
    {
        String? accesskey = appSettings.AwsSettings.AccessKey;
        String? secretkey = appSettings.AwsSettings.SecretKey;
        String? bucketName = appSettings.AwsSettings.BucketName;
        String? videoTitle = await YoutubeHelper.GetVideoName();
        String? key = $"Audios/{videoTitle}_{DateTime.Now.Ticks.ToString()}";
        RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast1;
        var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);
        var fileTransferUtility = new TransferUtility(s3Client);//create an object for TransferUtility
        var request = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream,
            ContentType = "audio/mpeg"
        };
        
        await fileTransferUtility.UploadAsync(request);

        return key;
    }
}