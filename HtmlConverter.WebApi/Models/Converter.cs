using HtmlConverter.Options;
using System.Collections.Generic;

namespace HtmlConverter.WebApi.Models
{
    public class Converter
    {
        public string uri { get; set; }
        public string format { get; set; }
        public int quality { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Dictionary<string, string> extend { get; set; }

        /// <summary>
        /// Only used with pdf generation.
        /// </summary>
        public Margins pagemargins { get; set; }
        /// <summary>
        /// Only used with pdf generation.
        /// </summary>
        public Size pagesize { get; set; } = Size.A4;
    }
}
