using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Services
{
    public interface IHtmlService
    {
        /// <summary>
        ///     Converts base64 sources of image to url.
        /// </summary>
        /// <param name="html">HTML text to convert</param>
        /// <param name="contentId">Parent content id</param>
        /// <param name="imageFolder">Folder of images of content</param>
        /// <param name="endpointURL">URL to image endpoint from appsettings</param>
        /// <returns>
        ///     HTML text with img src in form {endpointURL}/{imageFolder}/{contentId}/{int(unique id in folder)}{[.jpg/.png/.svg]} in first argument
        ///     and dictionary where key is file name (in form {int(unique id in folder)}{[.jpg/.png/.svg]}) and value is image in IFormFile 
        /// </returns>
        public (string html, IDictionary<string, IFormFile> images) ConvertBase64ImagesToURLImages(string html, int contentId, ImageFolder imageFolder, string endpointURL);
        public string ExtractTextFromHTML(string html);
    }
}
