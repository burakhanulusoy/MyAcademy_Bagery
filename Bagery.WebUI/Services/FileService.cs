
using Amazon.S3;
using Amazon.S3.Model;

namespace Bagery.WebUI.Services
{
    public class FileService(IConfiguration _configuration,
                             IAmazonS3 _amazonS3) : IFileService
    {
        private readonly string _bucketName = _configuration["AWS:BucketName"];


        public async Task<string> UploadFile(IFormFile file)
        {

            await _amazonS3.EnsureBucketExistsAsync(_bucketName);

            var key = $"{Guid.NewGuid()}-{file.FileName}";

            var awsRequest = new PutObjectRequest
            {

                BucketName = _bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                CannedACL = S3CannedACL.PublicRead

            };


            awsRequest.Metadata.Add("Content-Type", file.ContentType);

            var response = await _amazonS3.PutObjectAsync(awsRequest);

            string fileUrl = $"https://{_bucketName}.s3.{_amazonS3.Config.RegionEndpoint.SystemName}.amazonaws.com/{key}";

            return fileUrl;
        }
    }
}
