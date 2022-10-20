using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Services
{
    public interface IImageService
    {
        /// <summary>
        ///     Function to Load image as Stream from S3 bucket
        /// </summary>
        /// <param name="FileName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Data stream and content type of file. Default if not found.</returns>
        public Task<(Stream DataStream, string ContentType)> GetImageAsync(string FileName, ImageFolder FolderName, CancellationToken ct);

        /// <summary>
        ///     Function to remove image from S3 bucket
        /// </summary>
        /// <param name="FileName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>true if image is deleted</returns>
        public Task<bool> RemoveImageAsync(string FileName, ImageFolder FolderName, CancellationToken ct);

        /// <summary>
        ///     Function to remove image from S3 bucket
        /// </summary>
        /// <param name="file">File from request to backend</param>
        /// <param name="FileName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>true if image was added</returns>
        public Task<bool> SaveImageAsync(IFormFile file, string FileName, ImageFolder FolderName, CancellationToken ct);
    }
}