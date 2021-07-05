# HtmlConverter

## What is HtmlConvert

HtmlConvert is a simple .NET Core wrapper around the [WkHtmlToPdf](http://wkhtmltopdf.org) tool. Most options are exposed via a PdfConfiguration object for Pdf Conversion and ImageConfiguration object for Image Conversion , others can be specified by using Custom overrides for the configuration area you want.

Conversion setting assume you have the WkHTMLToPDF/WkHTMLToImage (x64) tool installed if not an error will be provided. You can override the Path to the tool by overridding **PdfConfiguration . WkhtmlPath** (PDF) | **ImageConfiguration . WkhtmlPath** (Image).

**You will need to install/download [WkHtmlToPdf](http://wkhtmltopdf.org), it is not embedded in the NuGet Package**

- [HtmlConverter](#htmlconverter)
  - [What is HtmlConvert](#what-is-htmlconvert)
  - [Convert HTML to PDF](#convert-html-to-pdf)
  - [Sample 1: Static HTML Content](#sample-1-static-html-content)
  - [Sample 2: Get Content from a URL](#sample-2-get-content-from-a-url)
  - [Sample 3: Quality and Page Size](#sample-3-quality-and-page-size)
  - [Convert HTML to Image](#convert-html-to-image)
  - [Sample 1: Static HTML Content (Image)](#sample-1-static-html-content-image)
  - [Sample 2: Get Content from a URL (Image)](#sample-2-get-content-from-a-url-image)
  - [Sample 3: Crop, Zoom and Page Size](#sample-3-crop-zoom-and-page-size)
  - [Configuration](#configuration)
    - [Basic Configuration (Image and Pdf)](#basic-configuration-image-and-pdf)
    - [Custom Configuration (Pdf)](#custom-configuration-pdf)
    - [Custom Configuration (Image)](#custom-configuration-image)
  - [Contributions](#contributions)
  - [Credits](#credits)

## Convert HTML to PDF

## Sample 1: Static HTML Content

```c#
    HtmlConverter.ConvertHtmlTo(new PdfConfiguration
    {
        Content = @"<h1>Lorem ipsum dolor sit amet consectetuer adipiscing elit I SHOULD BE RED BY JAVASCRIPT</h1>
        <script>
            document.querySelector('h1').style.color = 'rgb(128,0,0)';
        </script>",
        OutputPath = @"C:\temp\temp.pdf"
    });
```

## Sample 2: Get Content from a URL

```c#
    HtmlConverter.ConvertUrlToPdf(new PdfConfiguration
    {
        Url = "http://www.lipsum.com/",
        OutputPath = @"C:\temp\temp-url.pdf"
    });
```

## Sample 3: Quality and Page Size

```c#
    HtmlConverter.ConvertUrlToPdf(new PdfConfiguration
    {
        IsLowQuality = false,
        PageMargins = new Margins() { Bottom = 10, Left = 10, Right = 10, Top = 10 },
        PageSize = Size.A3,
        Content = @"<h1>Lorem ipsum dolor sit amet consectetuer adipiscing elit I SHOULD BE RED BY JAVASCRIPT</h1><script>document.querySelector('h1').style.color = 'rgb(128,0,0)';</script>",
        OutputPath = @"C:\temp\sample3.pdf"
    });
```

## Convert HTML to Image

## Sample 1: Static HTML Content (Image)
```c#
    HtmlConverter.ConvertHtmlToImage(new ImageConfiguration
    {
        Content = @"<h1>Lorem ipsum dolor sit amet consectetuer adipiscing elit I SHOULD BE RED BY JAVASCRIPT</h1>
        <script>
            document.querySelector('h1').style.color = 'rgb(128,0,0)';
        </script>",
        Quality = 100,
        Format = ImageFormat.Png,
        OutputPath = @"C:\temp\temp.pdf"
    });
```

## Sample 2: Get Content from a URL (Image)

```c#
    HtmlConverter.ConvertUrlToImage(new ImageConfiguration
    {
        Url = "http://www.lipsum.com/",
        OutputPath = @"C:\temp\temp-url.png",
        Quality = 100,
        Format = ImageFormat.Png
    });
```

## Sample 3: Crop, Zoom and Page Size

```c#
    HtmlConverter.ConvertHtmlToImage(new ImageConfiguration
    {
        Crop = new Cropping() { Height = 10, Width = 10, CropX = 10, CropY = 10 },
        Content = @"<h1>Lorem ipsum dolor sit amet consectetuer adipiscing elit I SHOULD BE RED BY JAVASCRIPT</h1><script>document.querySelector('h1').style.color = 'rgb(128,0,0)';</script>",
        OutputPath = @"C:\temp\sample3.pdf",
        Quality = 100,
        Format = ImageFormat.Png,
        Width = 1024,
        Height = 800
    });
```

## Configuration

### Basic Configuration (Image and Pdf)

```c#
        /// <summary>
        /// This will be send to the browser as a name of the generated PDF file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Path to wkhtmltopdf\wkhtmltoimage binary.
        /// </summary>
        public string WkhtmlPath { get; set; }

        /// <summary>
        /// Sets custom headers.
        /// </summary>
        [OptionFlag("--custom-header")]
        public Dictionary<string, string> CustomHeaders { get; set; }

        /// <summary>
        /// Sets cookies.
        /// </summary>
        [OptionFlag("--cookie")]
        public Dictionary<string, string> Cookies { get; set; }

        /// <summary>
        /// Sets post values.
        /// </summary>
        [OptionFlag("--post")]
        public Dictionary<string, string> Post { get; set; }

        /// <summary>
        /// Indicates whether the page can run JavaScript.
        /// </summary>
        [OptionFlag("-n")]
        public bool IsJavaScriptDisabled { get; set; }

        /// <summary>
        /// Minimum font size.
        /// </summary>
        [OptionFlag("--minimum-font-size")]
        public int? MinimumFontSize { get; set; }

        /// <summary>
        /// Sets proxy server.
        /// </summary>
        [OptionFlag("-p")]
        public string Proxy { get; set; }

        /// <summary>
        /// HTTP Authentication username.
        /// </summary>
        [OptionFlag("--username")]
        public string UserName { get; set; }

        /// <summary>
        /// HTTP Authentication password.
        /// </summary>
        [OptionFlag("--password")]
        public string Password { get; set; }

        /// <summary>
        /// Set the default text encoding, for input
        /// </summary>
        [OptionFlag("--encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// Use this if you need another switches that are not currently supported by that module.
        /// </summary>
        [OptionFlag("")]
        public string CustomSwitches { get; set; }

        public string Content {get;set;}
        public string Url {get;set;}
        public string OutputPath {get;set;}
```

### Custom Configuration (Pdf)

```c#
        /// <summary>
        /// Sets the page size.
        /// </summary>
        [OptionFlag("-s")]
        public Size? PageSize { get; set; }

        /// <summary>
        /// Sets the page width in mm.
        /// </summary>
        /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageHeight"/> has to be also specified.</remarks>
        [OptionFlag("--page-width")]
        public double? PageWidth { get; set; }

        /// <summary>
        /// Sets the page height in mm.
        /// </summary>
        /// <remarks>Has priority over <see cref="PageSize"/> but <see cref="PageWidth"/> has to be also specified.</remarks>
        [OptionFlag("--page-height")]
        public double? PageHeight { get; set; }

        /// <summary>
        /// Sets the page orientation.
        /// </summary>
        [OptionFlag("-O")]
        public Orientation? PageOrientation { get; set; }

        /// <summary>
        /// Sets the page margins.
        /// </summary>
        public Margins PageMargins { get; set; }

        /// <summary>
        /// Indicates whether the PDF should be generated in lower quality.
        /// </summary>
        [OptionFlag("-l")]
        public bool IsLowQuality { get; set; }

        /// <summary>
        /// Number of copies to print into the PDF file.
        /// </summary>
        [OptionFlag("--copies")]
        public int? Copies { get; set; }

        /// <summary>
        /// Indicates whether the PDF should be generated in grayscale.
        /// </summary>
        [OptionFlag("-g")]
        public bool IsGrayScale { get; set; }
```

### Custom Configuration (Image)

```c#
/// <summary>
        /// Sets the page margins.
        /// </summary>
        public Cropping Crop { get; set; }

        /// <summary>
        /// Output image quality (between 0 and 100)
        /// </summary>
        [OptionFlag("--quality")]
        public int? Quality { get; set; }

        /// <summary>
        /// Set screen width, note that this is used only as a guide line. Use --disable-smart-width to make it strict.
        /// </summary>
        [OptionFlag("--width")]
        public int? Width { get; set; }

        /// <summary>
        /// Set screen height (default is calculated from page content)
        /// </summary>
        [OptionFlag("--height")]
        public int? Height { get; set; }

        /// <summary>
        /// Output file format
        /// </summary>
        [OptionFlag("--format")]
        public ImageFormat Format { get; set; }

        /// <summary>
        /// Use this zoom factor
        /// </summary>
        [OptionFlag("--zoom")]
        public int? Zoom { get; set; }
```


## Web API 

HtmlConverter.WebApi is a tiny webapi hosted on top of the HtmlConverter library.  It is meant to be, more or less, a drop in replacement of https://github.com/gogap/go-wkhtmltox.


### Docker build and host site

Build a docker image like this:

```bash
sudo ./build_docker.sh the.path.your.private.docker.registry.com/feed_name/company_name
```


Run the web api docker image.

```bash
 sudo docker run htmlconverterwebapi -p 80:8080
 ```


### Web api curl example 

Call the webapi project with curl.

```bash
curl --location --request POST 'https://[your site address here]/v1/convert' \
--header 'cache-control: no-cache' \
--header 'content-type: application/json' \
--header 'Authorization: Basic [Credentials go Here]' \
--data-raw '{
        "to" : "image",
        "fetcher" : {
        	"name": "data",
        	"params": {
            "data":"PGJyIGNsYXNzPSdrLWJyJz48dGFibGUgcm9sZT0nZ3JpZCcgc3R5bGUgPSdib3JkZXItY29sbGFwc2U6c2VwYXJhdGU7IG1pbi13aWR0aDo0ODBweDsgbWFyZ2luOiAwcHg7IG1heC13aWR0aDpub25lOyBlbXB0eS1jZWxsczpzaG93OyBib3JkZXItd2lkdGg6MXB4OyBvdXRsaW5lOiAwcHg7IGJvcmRlcjogMXB4IHNvbGlkOyBwYWRkaW5nOiAxMHB4OyBib3JkZXItcmFkaXVzOjI1cHg7IGJhY2tncm91bmQtY29sb3I6IzIxMjg1MTtjb2xvcjojZmZmO21hcmdpbi1sZWZ0OmF1dG87IG1hcmdpbi1yaWdodDphdXRvJyBjbGFzcz0nay10YWJsZScgPjxjb2xncm91cD48Y29sPjxjb2w+PGNvbD48L2NvbGdyb3VwPjx0Ym9keSByb2xlPSdyb3dncm91cCc+PHRyIHJvbGU9J3JvdycgYWxpZ249J2NlbnRlcic+PHRkIHJvbGU9J2dyaWRjZWxsJyBjb2xzcGFuPSczJz48aDQgc3R5bGU9J3RleHQtYWxpZ246bGVmdDttYXJnaW4tbGVmdDoyMHB4ICFpbXBvcnRhbnQnPjxzcGFuPjxpbWcgc3JjPSJodHRwczovL21pbmlvLmRlbW8udG93bnN1aXRlLmNvbS83OTA0MWNkYi5zaXRlLWltYWdlcy9tYWlubG9nby5wbmc/MjAyMS0wNi0yOC0xMDo1MDowMSIgc3R5bGU9ImhlaWdodDogNDBweCFpbXBvcnRhbnQ7ICI+PC9zcGFuPjwvaDQ+IDwvdGQ+IDwvdHI+IDx0ciByb2xlPSdyb3cnIGFsaWduPSdjZW50ZXInPjx0ZCByb2xlPSdncmlkY2VsbCcgY29sc3Bhbj0nMyc+PGg0IHN0eWxlPSd0ZXh0LWFsaWduOi13ZWJraXQtY2VudGVyOyc+U2lsdmVyPC9oND4gPC90ZD4gPC90cj4gPHRyIHJvbGU9J3JvdycgYWxpZ249J2NlbnRlcic+PHRkIHJvbGU9J2dyaWRjZWxsJz48cCBzdHlsZT0ndGV4dC1hbGlnbjpyaWdodDsnPjxzbWFsbD5OYW1lIC0gPC9zbWFsbD4gICAgICA8L3A+IDxwIHN0eWxlPSd0ZXh0LWFsaWduOnJpZ2h0Oyc+PHNtYWxsPlB1cmNoYXNlIERhdGUgLTwvc21hbGw+PC9wPiA8cCBzdHlsZT0ndGV4dC1hbGlnbjpyaWdodDsnPjxzbWFsbD5WYWxpZCBUaWxsIC0gPC9zbWFsbD48L3A+IDwvdGQ+IDx0ZCByb2xlPSdncmlkY2VsbCcgc3R5bGU9J2JvcmRlci1yaWdodDoxcHggc29saWQ7JyA+PHAgc3R5bGU9J3RleHQtYWxpZ246bGVmdDsnID48c3BhbiBzdHlsZT0ndGV4dC1hbGlnbjotd2Via2l0LWNlbnRlcjsnPiAmbmJzcDtUZXN0IE5hbWU8L3NwYW4+ICAgICAgPC9wPiA8cCBzdHlsZT0ndGV4dC1hbGlnbjpsZWZ0OycgPjxzbWFsbD4mbmJzcDtBcHJpbCAyOSwgMjAyMDwvc21hbGw+PC9wPiA8cCBzdHlsZT0ndGV4dC1hbGlnbjpsZWZ0OycgPjxzbWFsbCBzdHlsZT0nZm9udC1zaXplOjExLjlweDt0ZXh0LWFsaWduOi13ZWJraXQtY2VudGVyOycgPiAmbmJzcDtBcHJpbCAyOSwgMjAyMDwvc21hbGw+PC9wPiA8L3RkPiA8dGQgcm9sZT0nZ3JpZGNlbGwnID48ZGl2IGlkPSJiYXJjb2RlX3ZpZXciIGNsYXNzPSJwdWxsLWxlZnQiPjxpbWcgc3JjPSdkYXRhOmltYWdlL3BuZztiYXNlNjQsaVZCT1J3MEtHZ29BQUFBTlNVaEVVZ0FBQUdRQUFBQmtDQUlBQUFEL2dBSURBQUFBQVhOU1IwSUFyczRjNlFBQUFBUm5RVTFCQUFDeGp3djhZUVVBQUFBSmNFaFpjd0FBRHNNQUFBN0RBY2R2cUdRQUFBWlNTVVJCVkhoZTdkRFJqaDAzRWdSUi8vOVBld1ZHUWdnNFJiSW9yd0EvOUhrcVJGWDNuZW0vL3Y2TWZSL3J3ZmV4SG53ZjY4SDNzUjU4SCt2Qjk3RWVmQi9yd2ZleEhud2Y2OEgzc1I1OEgrdkI5N0VlVEQvV1gyTjVZT25TdUVIUzR1TFpkaDFzSi9MQXpmaHVMQThzWFJvM1NGcGNQTnV1ZysxRUhyZ1ozdzFlNmh0bTdFcnJMY1YySFd6UnBVMXVmaHJmRFY3cUcyYnNTdXN0eFhZZGJOR2xUVzUrR3QvcHBjem16Z3lLWmJHNDlJd3UxbHNLWEpqTm5mbHFmS2VYTXBzN015aVd4ZUxTTTdwWWJ5bHdZVFozNXF2eG5WN0tiTzZlNGNMYzJGb1dHem5hOEkxbmMyZStHdC9wcGN6bTdoa3V6STJ0WmJHUm93M2ZlRFozNXF2eG5WN0tiTzdNNXM3Y3ZHVkdGK3V0UzgvbXpudzF2dE5MbWMyZDJkeVptN2ZNNkdLOWRlblozSm12eG5lRGwvcG1ONFBTdlBWczlKYjE0c0s4TTduNWFYdzNlS2x2ZGpNb3pWdlBSbTlaTHk3TU81T2JuOFozWTc3L0w4d1QzRitONzhaOC8xK1lKN2kvbXQ3OXYrU3ZXMXlZNGNKczdwNUIrVVArN050Yi9xZkZoUmt1ek9idUdaUS9aUHIyL0MwYnJ6ZWVRVEYzWnV0T2djdGtIcHBlOCtxZDF4dlBvSmc3czNXbndHVXlEMDJ2L1dyUGpTMlNqbks2bkl0bm95TnBTU3JuN2NIMEdmK0E1OFlXU1VjNVhjN0ZzOUdSdENTVjgvYmc3UmwreG5ZZGJKRzBKQTNrQWRsMXNJWExaTDZhM29GWDI2NkRMWktXcElFOElMc090bkNaekZmVE8vTVBlRzdlZW9hTDUrYXRaK3MrS1U5KzUwbi9wT2ZtcldlNGVHN2VlcmJ1ay9KaytxUi9oaGtVSkMxSnN1dGdhMWxzNUVpeWtDdzJjdlJpK294L2dCa1VKQzFKc3V0Z2ExbHM1RWl5a0N3MmN2VGl0NTZwSDNQcEdWMk1iY3Rhc3RqSTBaSlVzbDZTWnQ2dTBUL2owak82R051V3RXU3hrYU1scVdTOUpNMDhYbTkrZ042eUxsa2Y1VlM2VTVxMzgvbHFlb2ZkcStrdDY1TDFVVTZsTzZWNU81K3ZwbmZnMVhCaHRuT0hDL1BPNUtiNUtjKzI2Ny8wOWhmd2FyZ3cyN25EaFhsbmN0UDhsR2ZiOVY5Ni9ndCs0QWZNM2JPNWUwYVhuY25sNnczejFmVE8rQUZ6OTJ6dW50RmxaM0w1ZXNOOE5iNnJsM2JaT1YreU5mZWVyVHNGU1NYckpXbG1ldDJ2N3JKenZtUnI3ajFiZHdxU1N0Wkwwc3owT3U4Vzk1Nk5ibGxzVEc3QXBXV3huQXN6S0ZmanUrTGVzOUV0aTQzSkRiaTBMSlp6WVFibGFuclhkajlEUjlKeUx1Y1psTGJiMHBGVXp0dC9tTjYxM2MvUWtiU2N5M2tHcGUyMmRDU1Y4L1lmcG5mZzFVaGFrc1M5NTNiZU51NHhLV2ZjWHozOGZUL2szVXZTa2lUdVBiZnp0bkdQU1Ruai9tcDhKN3VDcERMWndtVTNnd0lYWnV3S2ttYW0xM24zc2l0SUtwTXRYSFl6S0hCaHhxNGdhZWJ0R3Y2WjF4bVVzNXd1WFVEZjZac3VvRjlONzh3LzhEcURjcGJUcFF2b08zM1RCZlNyNlYzeno1eG5uQXV6dVh1R0MzTmplemEvL0dGNjEvd3o1eG5ud216dW51SEMzTmllelM5L21ONmhYKzNDYkhSelo4YXVuT1ZVZHYyTXA2Nm1kK2hYdXpBYjNkeVpzU3RuT1pWZFArT3BxL0hkUnRiaTdubUhHeVJKZDRwbHNaekxiaDZhWHZQcWxyVzRlOTdoQmtuU25XSlpMT2V5bTRlbTEvMXFGK2FkdnFHMDNsSXNpK0p0eitnQyt0WDRybDdxd3J6VE41VFdXNHBsVWJ6dEdWMUF2eHJmNmFVOW8wdmpCaTZlalc1WlNIZEt5MXAyL1pmR2QzcHB6K2pTdUlHTFo2TmJGdEtkMHJLV1hmK2w4VjI5bEFJWDVuYmVnaHZMb21UOUwrUkZMNmJQOUE5UTRNTGN6bHR3WTFtVXJQK0Z2T2pGN3p3emtiOW9TVnFTU3RiaXpneEt5M3BKa2l5V3BIZS8vK1JaL3E0bGFVa3FXWXM3TXlndDZ5VkpzbGlTM2syZnpPOE03TzdwU0NyZU1vT0NKSEdmektBZzZXWjhON2E3cHlPcGVNc01DcExFZlRLRGdxU2I4ZDNncGZNYkpDMHVQV05YYk42N1hFMnZKNitlM3lCcGNla1p1Mkx6M3VWcWV1MVhNNXY3Njd6akcyYWpvOHNjenc1TnIvMXFabk4vblhkOHcyeDBkSm5qMmFIcHRWL05iTzZlalg3bXk1N1B1RVRTNG5LZXI4WjNlaW16dVhzMitwa3ZlejdqRWttTHkzbStHdC9wcGN6bTdubUhHeVF0THA1QmFkNTZoa3ZQb0Z5TjcvUlNablAzdk1NTmtoWVh6NkEwYnozRHBXZFFyc1ozZzVmNmhoa3V6S0FnYWRrVkpFa1dHem5hbU56OE5MNGJ2TlEzekhCaEJnVkp5NjRnU2JMWXlOSEc1T2FuOGQyWTc1blJCZWVPcExMYjB0R2xjWE0xdmh2elBUTzY0TnlSVkhaYk9ybzBicTZtZDU4ZnZvLzE0UHRZRDc2UDllRDdXQSsrai9YZysxZ1B2by8xNFB0WUQ3NlA5ZUQ3V0ErK2ovWGcrMWhqZi8vOVB5aDVGNVNjVzk5cUFBQUFBRWxGVGtTdVFtQ0MnPjwvZGl2PjwvdGQ+IDwvdHI+IDx0ciByb2xlPSdyb3cnIGFsaWduPSdjZW50ZXInPjx0ZCByb2xlPSdncmlkY2VsbCcgY29sc3Bhbj0nMic+PHA+PHNwYW4+PC9zcGFuPjwvcD4gPC90ZD4gPC90cj4gPC90Ym9keT48L3RhYmxlPjxiciBjbGFzcz0nay1icic+IA=="
        	}
        },
        "converter":{
            "format": "jpg",
            "quality": 94,
            "width": 390
        },
        "template": "binary"
}'
```


### Sample c# client library.

The most basic bare bones client example to use call the web api.

```cs
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter
    public class HtmlToImageServiceConverter
    {
        readonly HttpClient client;
        readonly string username;
        readonly string password;
        readonly string url;

        public HtmlToImageServiceConverter(HttpClient client, string username, string password, string url)
        {
            this.client = client;
            this.username = username;
            this.password = password;
            this.url = url;
        }

        public async Task<byte[]> GenerateAsync(string data, ConverterOptions options)
        {
            using (var request_ = new System.Net.Http.HttpRequestMessage())
            {
                request_.Method = new System.Net.Http.HttpMethod("POST");


                if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(username))
                {
                    var byteArray = Encoding.UTF8.GetBytes($"{username}:{password}");
                    request_.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }

                request_.RequestUri = new Uri(url);

                var jsonObject = new Root()
                {
                    to = "image",
                    fetcher = new Fetcher()
                    {
                        name = "data",
                        @params = new Params()
                        {
                            data = Convert.ToBase64String(Encoding.UTF8.GetBytes(data))
                        }
                    },
                    converter = new Converter()
                    {
                        format = options.Format.ToString().ToLower(),
                        quality = options.Quality,
                        width = options.Width,
                        height = options.Height

                    },
                    template = "binary"
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);
                using (var content = new System.Net.Http.StringContent(json))
                {
                    content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content;

                    var response_ = await client.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    var status_ = (int)response_.StatusCode;

                    if (status_ < 200 || status_ > 299)
                    {
                        var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new HttpRequestException("The HTTP status code of the response was not expected (" + status_ + "). " + responseData_);
                    }

                    return await response_.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
            }
        }

        class Params
        {
            public string data { get; set; }
        }

        class Fetcher
        {
            public string name { get; set; }
            public Params @params { get; set; }
        }

        class Converter
        {
            public string format { get; set; }
            public int quality { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        class Root
        {
            public string to { get; set; }
            public Fetcher fetcher { get; set; }
            public Converter converter { get; set; }
            public string template { get; set; }
        }


    }

    public class ConverterOptions
    {
        public ConverterFormats Format { get; set; } = ConverterFormats.JPG;
        public int Quality { get; set; } = 94;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }

    public enum ConverterFormats
    {
        JPG,
        PNG,
        PDF
    }
}
```

## Contributions

- King Kemsty  [@kemsty2](https://twitter.com/kemsty2/)

## Credits

This project is base on

- Rotativa
- CoreHtmlToImage
