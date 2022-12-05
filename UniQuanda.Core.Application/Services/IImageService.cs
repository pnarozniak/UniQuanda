using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Services
{
    public interface IImageService
    {
        /// <summary>
        ///     Function to Load image as Stream from S3 bucket
        /// </summary>
        /// <param name="ImageName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Data stream and content type of file. Default if not found.</returns>
        public Task<(Stream DataStream, string ContentType)> GetImageAsync(string ImageName, ImageFolder FolderName, CancellationToken ct);

        /// <summary>
        ///     Function to remove image from S3 bucket
        /// </summary>
        /// <param name="ImageName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>true if image is deleted</returns>
        public Task<bool> RemoveImageAsync(string ImageName, ImageFolder FolderName, CancellationToken ct);

        /// <summary>
        ///     Function to save image from S3 bucket
        /// </summary>
        /// <param name="image">File from request to backend</param>
        /// <param name="ImageName">Name of file (with type). Ex. File.png</param>
        /// <param name="FolderName">AdvancedEnum for folder name</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>true if image was added</returns>
        public Task<bool> SaveImageAsync(IFormFile image, string ImageName, ImageFolder FolderName, CancellationToken ct);

        /// <summary>
        ///     Returns image endpoint adress. 
        /// </summary>
        /// <returns>string with image endpoint adress</returns>
        public string GetImageURL();

        /// <summary>
        ///    Function to Upload multiple images to S3 bucket
        /// </summary>
        /// <param name="images">List of all images to add. Key is file name and value is file </param>
        /// <param name="images">Folder name enum</param>
        /// <param name="ct"></param>
        /// <returns>IEnumerable with IDs of added images</returns>
        public Task<IEnumerable<int>> UploadMultipleImagesAsync(IDictionary<string, IFormFile> images, ImageFolder FolderName, CancellationToken ct);
    }
}