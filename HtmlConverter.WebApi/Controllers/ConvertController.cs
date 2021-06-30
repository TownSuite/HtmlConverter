using HtmlConverter.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlConverter.WebApi.Controllers
{
    [ApiController]
    [Route("/convert")]
    [Route("v{v:apiVersion}/convert")]
    [ApiVersion("1.0")]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;

        public ConvertController(ILogger<ConvertController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public FileContentResult Post(Models.Root data)
        {

            if (string.Equals(data.to, "pdf", StringComparison.InvariantCultureIgnoreCase))
            {
                // pdf path

                if (string.Equals(data.fetcher.name, "http"))
                {
                    // read http url path
                    return PdfUrlPath(data);
                }

                // read data path
                return PdfDataPath(data);
            }

            // image path


            var theFormat = Options.ImageFormat.Jpeg;
            if (data.converter.format == "png")
            {
                theFormat = Options.ImageFormat.Png;
            }

            if (string.Equals(data.fetcher.name, "http"))
            {
                return ImageUrlPath(data, theFormat);
            }

            // read data path
            return ImageDataPath(data, theFormat);
        }

        private FileContentResult ImageDataPath(Models.Root data, Options.ImageFormat theFormat)
        {
            byte[] tobyte = Convert.FromBase64String(data.fetcher.@params.data);
            string decodedString = Encoding.UTF8.GetString(tobyte);
            var bytes = Core.HtmlConverter.ConvertHtmlToImage(new ImageConfiguration()
            {
                Content = decodedString,
                Quality = data.converter.quality,
                Format = theFormat,
                Width = data.converter.width,
                Height = data.converter.height
            });

            return new FileContentResult(bytes, $"image/{theFormat.ToString().ToLower()}");
        }

        private FileContentResult ImageUrlPath(Models.Root data, Options.ImageFormat theFormat)
        {
            var bytes = Core.HtmlConverter.ConvertUrlToImage(new ImageConfiguration()
            {
                Quality = data.converter.quality,
                Url = data.converter.uri,
                Format = theFormat,
                Width = data.converter.width,
                Height = data.converter.height
            });

            return new FileContentResult(bytes, $"image/{theFormat.ToString().ToLower()}");
        }

        private FileContentResult PdfDataPath(Models.Root data)
        {
            byte[] tobyte = Convert.FromBase64String(data.fetcher.@params.data);
            string decodedString = Encoding.UTF8.GetString(tobyte);
            var bytes = Core.HtmlConverter.ConvertHtmlToPdf(new PdfConfiguration()
            {
                IsLowQuality = false,
                Content = decodedString
            });
            return new FileContentResult(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }

        private FileContentResult PdfUrlPath(Models.Root data)
        {
            var bytes = Core.HtmlConverter.ConvertUrlToPdf(new PdfConfiguration()
            {
                IsLowQuality = false,
                Url = data.converter.uri
            });
            return new FileContentResult(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }
    }
}
