using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly BucketNames bucket = BucketNames.Default;
        private readonly IConfiguration _configuration;

        public ImageService(IAmazonS3 amazonS3, IConfiguration configuration)
        {
            _amazonS3 = amazonS3;
            _configuration = configuration;
        }

        public async Task<(Stream DataStream, string ContentType)> GetImageAsync(string FileName, ImageFolder FolderName, CancellationToken ct)
        {
            var s3Object = await _amazonS3.GetObjectAsync(bucket.Value, $"{FolderName.Value}/{FileName}");

            return s3Object.HttpStatusCode == System.Net.HttpStatusCode.OK ?
                (s3Object.ResponseStream, s3Object.Headers.ContentType)
                : default((Stream DataStream, string ContentType));
        }

        public string GetImageURL()
        {
            return _configuration.GetSection("Image")["EndpointURL"];
        }

        public async Task<bool> RemoveImageAsync(string FileName, ImageFolder FolderName, CancellationToken ct)
        {
            var result = await _amazonS3.DeleteObjectAsync(bucket.Value, $"{FolderName.Value}/{FileName}");
            return result.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<bool> SaveImageAsync(IFormFile file, string FileName, ImageFolder FolderName, CancellationToken ct)
        {
            var request = new PutObjectRequest()
            {
                BucketName = bucket.Value,
                Key = $"{FolderName.Value}/{FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            var response = await _amazonS3.PutObjectAsync(request, ct);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}

