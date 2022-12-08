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

        public async Task<(Stream DataStream, string ContentType)> GetImageAsync(string ImageName, ImageFolder FolderName, CancellationToken ct)
        {
            try
            {
                var s3Object = await _amazonS3.GetObjectAsync(bucket.Value, $"{FolderName.Value}/{ImageName}", ct);
                return s3Object.HttpStatusCode == System.Net.HttpStatusCode.OK ?
                    (s3Object.ResponseStream, s3Object.Headers.ContentType)
                    : default((Stream DataStream, string ContentType));
            }
            catch (AmazonS3Exception)
            {
                return default;
            }
        }

        public string GetImageURL()
        {
            return _configuration.GetSection("Image")["EndpointURL"];
        }

        public async Task<bool> RemoveImageAsync(string ImageName, ImageFolder FolderName, CancellationToken ct)
        {
            var result = await _amazonS3.DeleteObjectAsync(bucket.Value, $"{FolderName.Value}/{ImageName}", ct);
            return result.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public async Task<bool> SaveImageAsync(IFormFile image, string ImageName, ImageFolder FolderName, CancellationToken ct)
        {
            var request = new PutObjectRequest()
            {
                BucketName = bucket.Value,
                Key = $"{FolderName.Value}/{ImageName}",
                InputStream = image.OpenReadStream(),
                ContentType = image.ContentType
            };
            request.Metadata.Add("Content-Type", image.ContentType);
            var response = await _amazonS3.PutObjectAsync(request, ct);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> UploadMultipleImagesAsStreamAsync(IDictionary<string, Stream> images, ImageFolder FolderName, CancellationToken ct)
        {
            var result = true;
            foreach (var image in images)
            {
                var request = new PutObjectRequest()
                {
                    BucketName = bucket.Value,
                    Key = $"{FolderName.Value}/{image.Key}",
                    InputStream = image.Value,
                    ContentType = $"image/{image.Key.Split('.').Last()}"
                };
                request.Metadata.Add("Content-Type", request.ContentType);
                var response = await _amazonS3.PutObjectAsync(request, ct);
                result = response.HttpStatusCode == System.Net.HttpStatusCode.OK && result;
            }
            return result;
        }
    }
}

