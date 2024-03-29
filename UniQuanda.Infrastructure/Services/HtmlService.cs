﻿using HtmlAgilityPack;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Services
{
    internal class HtmlService : IHtmlService
    {
        public (string html, IDictionary<string, Stream> images) ConvertBase64ImagesToURLImages(
            string html, int contentId,
            ImageFolder imageFolder, string endpointURL)
        {
            int imageId = 1;
            var initialImagePath = $"{endpointURL}{imageFolder.Value}/";
            var imagesResult = new Dictionary<string, Stream>();
            var dom = new HtmlDocument();
            dom.LoadHtml(html);
            var imageNodes = dom.DocumentNode.SelectNodes("//img");
            if (imageNodes == null) return (html, imagesResult);
            foreach (var imageNode in imageNodes)
            {
                var imageString = imageNode.Attributes["src"].Value;
                //check if image is base64
                if (imageString.StartsWith("data:image"))
                {
                    var imageType = imageString.Substring(imageString.IndexOf('/') + 1, imageString.IndexOf(';') - imageString.IndexOf('/') - 1);
                    var base64 = imageString.Substring(imageString.IndexOf(',') + 1);
                    var byteArr = Convert.FromBase64String(base64);
                    var stream = new MemoryStream(byteArr);

                    var imageIdString = $"{contentId}/{imageId}.{imageType}";
                    imagesResult.Add(imageIdString, stream);
                    imageNode.Attributes["src"].Value = $"{initialImagePath}{imageIdString}";
                    imageId++;
                }

            }
            return (dom.DocumentNode.OuterHtml, imagesResult);

        }

        public string ExtractTextFromHTML(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var chunks = new List<string>();
            foreach (var item in doc.DocumentNode.DescendantNodesAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                    chunks.Add(item.InnerText.Trim());
            }
            return string.Join(" ", chunks);
        }
    }
}
