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
        ///     HTML text with img src in form {endpointURL}/{imageFolder}/{contentId}/{int(unique id in folder)}in first argument
        ///     and dictionary where key is file name (in form {ContentId}/{int(unique id in folder)}.{image type(jpg/png/etc.)}) and value is image in stream 
        /// </returns>
        public (string html, IDictionary<string, Stream> images) ConvertBase64ImagesToURLImages(string html, int contentId, ImageFolder imageFolder, string endpointURL);
        /// <summary>
        ///    Extracts text from HTML
        /// </summary>
        /// <param name="html">Html text</param>
        /// <returns>all text data</returns>
        public string ExtractTextFromHTML(string html);
    }
}
