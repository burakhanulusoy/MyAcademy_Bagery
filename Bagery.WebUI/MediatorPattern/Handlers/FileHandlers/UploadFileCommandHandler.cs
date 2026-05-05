using Amazon.S3;
using Amazon.S3.Model;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Comments.FileComments;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.FileHandlers
{
    public class UploadFileCommandHandler(IAmazonS3 _amazonS3, IConfiguration _configuration) : IRequestHandler<UploadFileCommand, string>
    {
        private readonly string _bucketName = _configuration["AWS:BucketName"];

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
                if (request.file is null || request.file.Length == 0)
            
                {
                    throw new ValidationException("Dosya boş bırakılamaz");
                }


                await _amazonS3.EnsureBucketExistsAsync(_bucketName);

                var key = $"{Guid.NewGuid()}-{request.file.FileName}";

                var awsRequest = new PutObjectRequest
                {

                    BucketName = _bucketName,
                    Key = key,
                    InputStream = request.file.OpenReadStream(),
                    CannedACL = S3CannedACL.PublicRead

                };


                awsRequest.Metadata.Add("Content-Type", request.file.ContentType);

                var response = await _amazonS3.PutObjectAsync(awsRequest);

                string fileUrl = $"https://{_bucketName}.s3.{_amazonS3.Config.RegionEndpoint.SystemName}.amazonaws.com/{key}";

                return fileUrl;

            }
        }
    }
